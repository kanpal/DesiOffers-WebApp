using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using WebLogic.Models;

namespace WebLogic.ViewModels
{
    public class ProductViewModel
    {
        public long ID { get; set; }
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        public long? CategoryID { get; set; }

        [DisplayName("Category")]
        public string CategoryName { get; set; }

        public long? CustomerID { get; set; }

        [DisplayName("Offered by")]
        public string CustomerName { get; set; }

        [DisplayName("Price")]
        public decimal OfferPrice { get; set; }

        public long? ImageStoreID { get; set; }
        public string ImageUrl { get; set; }

        public ProductViewModel()
        {
        }

        public ProductViewModel(Product product)
        {
            ID = product.ID;
            Name = product.Name;
            CategoryID = product.CategoryID;
            if (product.Category != null)
                CategoryName = product.Category.Name;
            OfferPrice = product.OfferPrice ?? 0;
            ImageStoreID = product.PrimaryImageStoreID;
            CustomerID = product.CustomerID;
            if (product.Customer != null)
                CustomerName = product.Customer.FirstName + " " + product.Customer.LastName;
            //ImageUrl = UrlHelper.GenerateUrl("", "Get", "ImageStore", new { id = product.PrimaryImageStoreID.Value }, null, null, false); // GeneralService.GetImageService().GetImagePath(product.PrimaryImageStoreID);
        }

        public Product ToEntity()
        {
            Product product = new Product();
            Sync(product);
            return product;
        }

        public void Sync(Product product)
        {
            product.ID = ID;
            product.Name = Name;
            product.CategoryID = CategoryID;
            product.CustomerID = CustomerID;
            product.OfferPrice = OfferPrice;
            product.PrimaryImageStoreID = ImageStoreID;
        }

    }
}