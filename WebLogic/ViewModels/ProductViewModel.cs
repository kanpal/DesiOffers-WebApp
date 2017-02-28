using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using WebLogic.DataModels;
using WebLogic.Models;

namespace WebLogic.ViewModels
{
    public class ProductViewModel : ProductModel
    {
        [DisplayName("Category")]
        public string CategoryName { get; set; }

        [DisplayName("Offered by")]
        public string CustomerName { get; set; }

        public string ImageUrl { get; set; }

        public ProductViewModel()
        {
        }

        public ProductViewModel(Product product)
            : base(product)
        {
            if (product.Category != null)
                CategoryName = product.Category.Name;
            if (product.Customer != null)
                CustomerName = product.Customer.FirstName + " " + product.Customer.LastName;
            //ImageUrl = UrlHelper.GenerateUrl("", "Get", "ImageStore", new { id = product.PrimaryImageStoreID.Value }, null, null, false); // GeneralService.GetImageService().GetImagePath(product.PrimaryImageStoreID);
        }

        public override void Sync(Product product)
        {
            base.Sync(product);
        }

    }
}