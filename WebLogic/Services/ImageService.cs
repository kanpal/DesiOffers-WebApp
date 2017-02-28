using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebLogic.Framework;
using WebLogic.Interfaces;
using WebLogic.Models;
using WebLogic.ViewModels;

namespace WebLogic.Services
{
    public class ImageService : BaseEntityService<ImageStore, ImageStoreViewModel>
    {
        const string BaseUrl = "~/ImageStore";
        const int DefaultFilePathRootId = 1;
        public ImageService()
        {
        }

        public override string GetName()
        {
            return "ImageService";
        }

        public ImageStoreViewModel Get(long id)
        {
            var imageStore = GeneralService.GetDbEntities().ImageStores.Where(i => i.ID == id).FirstOrDefault();
            if (imageStore == null)
                return null;
            return new ImageStoreViewModel(imageStore);
        }

        public string GetImagePath(long? imageId)
        {
            if (imageId.HasValue && imageId.Value != 0)
            {
                ImageStore imageStore = GeneralService.GetDbEntities().ImageStores.Where(img => img.ID == imageId.Value).FirstOrDefault();
                if (imageStore != null)
                {
                    return PathingService.MapPath(imageStore.FilePathRootID, imageStore.FilePath, PathTypes.DiskPath);
/*
                    string filePath = imageStore.FilePath;
                    if (!string.IsNullOrWhiteSpace(filePath))
                    {
                        filePath = filePath.Replace('\\', '/');
                        return string.Format("{0}{1}", BaseUrl, filePath);
                    }
*/
                }
            }
            return string.Empty;
        }

        public string GetCustomerStockImagePath()
        {
            return GetStockImagePath(DefaultFilePathRootId, "\\Customers\\Default-Male.jfif");
        }

        public string GetProductStockImagePath()
        {
            return GetStockImagePath(DefaultFilePathRootId, "\\Products\\Default-No-Image.png");
        }

        public string GetStockImagePath(long pathRootId, string filePath)
        {
            return PathingService.MapPath(pathRootId, filePath, PathTypes.DiskPath);
        }

        public string GetContentType(string filePath)
        {
            if (filePath == null)
                return string.Empty;
            string extension = Path.GetExtension(filePath);
            if (extension == null)
                return null;            
            switch (extension.ToLower())
            {
                case ".jpeg":
                case ".jpg":
                case ".jfif":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
            }
            return null;
        }

        public ImageStoreViewModel AddNew(string fileName)
        {
            string fileType = !string.IsNullOrWhiteSpace(fileName) ? Path.GetExtension(fileName) : ".jpg";
            ImageStore imageStore = new ImageStore();
            long nextImageId = GeneralService.CounterNextValue(CounterNames.ImageId);
            imageStore.FilePath = string.Format("\\Products\\{0}{1}", nextImageId, fileType);
            imageStore.FilePathRootID = 1;  // TODO - set it from config
            imageStore.CreatedDate = DateTime.Now;
            var entities = GeneralService.GetDbEntities();
            entities.ImageStores.Add(imageStore);
            entities.SaveChanges();
            return new ImageStoreViewModel(imageStore);
        }

        public bool Delete(long id)
        {
            var imageStore = GeneralService.GetDbEntities().ImageStores.Where(i => i.ID == id).FirstOrDefault();
            if (imageStore == null)
                return false;
            string fullPath = PathingService.MapPath(imageStore.FilePathRootID, imageStore.FilePath, PathTypes.DiskPath);
            try
            {
                GeneralService.GetDbEntities().ImageStores.Remove(imageStore);
                File.Delete(fullPath);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public ImageStoreViewModel AddProductImage(long productId, HttpPostedFileBase imageFile)
        {
            string fileName = Path.GetFileName(imageFile.FileName);
            ImageStoreViewModel imageModel = AddNew(fileName);
            imageFile.SaveAs(imageModel.FullPath);
            var productService = GeneralService.GetProductService();
            productService.AddImage(productId, imageModel.ID);
            return imageModel;
        }
    }
}
