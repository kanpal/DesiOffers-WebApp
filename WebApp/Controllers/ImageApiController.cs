using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebLogic.Services;
using WebLogic.ViewModels;

namespace WebApp.Controllers
{
    public class ImageApiController : ApiController
    {
        static string[] _allowedExtensions = { ".jpg", ".jpeg", ".gif", ".bmp", ".png", ".jfif" };

        [HttpGet]
        [HttpPost]
        public HttpResponseMessage Get(long id)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            ImageService imageService = GeneralService.GetImageService();
            var imageViewModel = imageService.Get(id);

            using (FileStream fileStream = new FileStream(imageViewModel.FullPath, FileMode.Open))
            {
                Image image = Image.FromStream(fileStream);
                MemoryStream memoryStream = new MemoryStream();
                image.Save(memoryStream, ImageFormat.Jpeg);
                result.Content = new ByteArrayContent(memoryStream.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                fileStream.Close();
            }

            return result;
        }

        [HttpPost]
        public HttpResponseMessage Save(HttpPostedFile imageFile)
        {
            if (!_allowedExtensions.Any(x => x.Equals(Path.GetExtension(imageFile.FileName.ToLower()), StringComparison.OrdinalIgnoreCase)))
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "Invalid file type."));
            }
            ImageService imageService = GeneralService.GetImageService();
            string fileName = imageFile.FileName;
            if (fileName != null)
                fileName = fileName.Replace("\"", "");
            ImageStoreViewModel viewModel = imageService.AddNew(fileName);
            imageFile.SaveAs(viewModel.FullPath);

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(string.Format("ImageId={0}", viewModel.ID));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                ImageService imageService = GeneralService.GetImageService();

                string result = string.Empty;
                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    string fileName = file.Headers.ContentDisposition.FileName;
                    if (fileName != null)
                        fileName = fileName.Replace("\"", "");
                    ImageStoreViewModel viewModel = imageService.AddNew(fileName);

                    File.Move(file.LocalFileName, viewModel.FullPath);

                    if (string.IsNullOrWhiteSpace(result))
                        result = string.Format("ImageId={0}", viewModel.ID);
                    else
                        result += "," + viewModel.ID.ToString();
                    //Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    //Trace.WriteLine("Server file path: " + file.LocalFileName);
                }
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(result);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                return response;
                //return Request.CreateResponse(HttpStatusCode.OK, new StringContent(result));
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPost]
        public HttpResponseMessage PostAsync()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            if (Request.Content.IsMimeMultipartContent())
            {
                ImageService imageService = GeneralService.GetImageService();
                long imageId = 0;
                //For larger files, this might need to be added:
                //Request.Content.LoadIntoBufferAsync().Wait();
                                
                Request.Content.ReadAsMultipartAsync<MultipartMemoryStreamProvider>(
                        new MultipartMemoryStreamProvider()).ContinueWith((task) =>
                        {
                            MultipartMemoryStreamProvider provider = task.Result;
                            foreach (HttpContent content in provider.Contents)
                            {
                                Stream stream = content.ReadAsStreamAsync().Result;
                                using (Image image = Image.FromStream(stream))
                                {
                                    string name = content.Headers.ContentDisposition.Name;
                                    string fileName = content.Headers.ContentDisposition.FileName;
                                    if (fileName != null)
                                        fileName = fileName.Replace("\"", "");
                                    ImageStoreViewModel viewModel = imageService.AddNew(fileName);
                                    string filePath = viewModel.FullPath;
                                    imageId = viewModel.ID;
                                    //Note that the ID is pushed to the request header,
                                    //not the content header:
                                    //String[] headerValues = (String[])Request.Headers.GetValues("UniqueId");
                                    //String fileName = headerValues[0] + ".jpg";
                                    //String fullPath = Path.Combine(filePath, fileName);
                                    image.Save(filePath);
                                }
                            }
                        });  
                response.Content = new StringContent(string.Format("ImageId={0}", imageId));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                return response;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }
        public void Delete(int id)
        {
            ImageService imageService = GeneralService.GetImageService();
            imageService.Delete(id);
        }
    }
}
