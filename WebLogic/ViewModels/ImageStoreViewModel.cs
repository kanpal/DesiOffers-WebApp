using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLogic.Models;
using WebLogic.Services;

namespace WebLogic.ViewModels
{
    public class ImageStoreViewModel
    {
        public long ID { get; set; }
        public string FullPath { get; set; }

        public ImageStoreViewModel()
        {
        }

        public ImageStoreViewModel(ImageStore imageStore)
        {
            ID = imageStore.ID;
            FullPath = PathingService.MapPath(imageStore.FilePathRootID, imageStore.FilePath, PathTypes.DiskPath);
        }
    }
}
