using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebLogic.Services;
using WebLogic.ViewModels;

namespace WebApp.Controllers
{
    public class ProductApiController : ApiController
    {
        [HttpGet]
        [HttpPost]
        [ResponseType(typeof(ProductViewModel))]
        public IHttpActionResult Get(long id)
        {
            try
            {
                ProductService productService = GeneralService.GetProductService();
                return Ok(productService.Get(id));
            }
            catch (Exception exception)
            {
                return Json(new { Error = exception.Message });
            }
        }

        [HttpGet]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<ProductViewModel>))]
        public IHttpActionResult ListRecent()
        {
            ProductService productService = GeneralService.GetProductService();
            return Ok(productService.ListRecent().AsEnumerable());
        }

        [HttpGet]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<ProductViewModel>))]
        public IHttpActionResult List(string searchText)
        {
            ProductService productService = GeneralService.GetProductService();
            return Ok(productService.ListMatching(searchText).AsEnumerable());
        }

        [HttpGet]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<ProductViewModel>))]
        public IHttpActionResult ListByZipCode(string zipCode)
        {
            ProductService productService = GeneralService.GetProductService();
            if (!string.IsNullOrWhiteSpace(zipCode))
            {
                return Ok(productService.ListByZipCode(zipCode).AsEnumerable());
            }
            return Ok();
        }

        [HttpGet]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<ProductViewModel>))]
        public IHttpActionResult ListByPosition(float? latitude, float? longitude)
        {
            ProductService productService = GeneralService.GetProductService();
            if (latitude.HasValue && longitude.HasValue)
            {
                LocationService service = new LocationService();
                var zipCodes = service.GetZipCodesInRange(latitude.Value, longitude.Value).Select(x => x.Zipcode).ToList();
                return Ok(productService.ListByZipCodes(zipCodes).ToList());
            }
            return Ok();
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<CategoryViewModel>))]
        // List all the categories
        public IHttpActionResult ListCategories()
        {
            ProductService productService = GeneralService.GetProductService();
            return Ok(productService.ListCategories());
        }

        [HttpPost]
        [ResponseType(typeof(ProductViewModel))]
        // Add new Product
        public IHttpActionResult Post(ProductViewModel viewModel)
        {
            ProductService productService = GeneralService.GetProductService();
            return Ok(productService.Add(viewModel));
        }

        [HttpPost]
        [ResponseType(typeof(ProductViewModel))]
        // Update Existing Product
        public IHttpActionResult Put(ProductViewModel viewModel)
        {
            ProductService productService = GeneralService.GetProductService();
            return Ok(productService.Update(viewModel));
        }

        [HttpGet]
        [HttpPost]
        // Delete a Product
        public bool Delete(int id)
        {
            ProductService productService = GeneralService.GetProductService();
            return productService.Delete(id);
        }

        [HttpPost]
        [ResponseType(typeof(IEnumerable<TransactionViewModel>))]
        public IHttpActionResult ListTransactions(long id)
        {
            ProductService productService = GeneralService.GetProductService();
            return Ok(productService.ListTransactions(id));
        }
    }
}
