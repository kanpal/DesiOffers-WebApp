using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLogic.Models;

namespace WebLogic.DataModels
{
    public class ProductModel
    {
        public long ID { get; set; }
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        public long? CategoryID { get; set; }

        public long? CustomerID { get; set; }

        [DisplayName("Price")]
        public decimal MinPrice { get; set; }

        [DisplayName("Current Offer")]
        public decimal OfferPrice { get; set; }

        public long? ImageStoreID { get; set; }

        public ProductModel()
        {
        }

        public ProductModel(Product product)
        {
            ID = product.ID;
            Name = product.Name;
            CategoryID = product.CategoryID;
            MinPrice = product.MinPrice ?? 0;
            OfferPrice = product.OfferPrice ?? 0;
            ImageStoreID = product.PrimaryImageStoreID;
            CustomerID = product.CustomerID;
        }
        public Product ToEntity()
        {
            Product product = new Product();
            Sync(product);
            return product;
        }

        public virtual void Sync(Product product)
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
