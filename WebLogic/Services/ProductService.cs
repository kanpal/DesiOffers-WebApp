using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebLogic.DataModels;
using WebLogic.Framework;
using WebLogic.Interfaces;
using WebLogic.Models;
using WebLogic.ViewModels;

namespace WebLogic.Services
{
    public class ProductService : BaseEntityService<Product, ProductViewModel>
    {
        public ProductService()
        {
        }

        public string GetName()
        {
            return "ProductService";
        }

        public ProductViewModel Get(long id)
        {
            var product = GeneralService.GetDbEntities().Products.Where(p => p.ID == id).FirstOrDefault();
            if (product == null)
                return null;
            return new ProductViewModel(product);
        }

        public IEnumerable<ProductViewModel> ListRecent(int limit = 50)
        {
            DesiOfferEntities entities = GeneralService.GetDbEntities();
            List<ProductViewModel> viewModels = new List<ProductViewModel>();
            foreach (Product product in entities.Products.Take(limit))
            {
                viewModels.Add(new ProductViewModel(product));
            }
            return viewModels;
        }

        public IEnumerable<ProductViewModel> ListByZipCode(string zipCode)
        {
            DesiOfferEntities entities = GeneralService.GetDbEntities();
            var query = entities.Products.Where(c => c.Customer.ZipCode == zipCode);
            List<ProductViewModel> viewModels = new List<ProductViewModel>();
            foreach (Product product in query)
            {
                viewModels.Add(new ProductViewModel(product));
            }
            return viewModels;
        }

        public IEnumerable<ProductViewModel> ListByZipCodes(List<string> zipCodes)
        {
            DesiOfferEntities entities = GeneralService.GetDbEntities();
            var query = entities.Products.Where(c => zipCodes.Contains(c.Customer.ZipCode));
            List<ProductViewModel> viewModels = new List<ProductViewModel>();
            foreach (Product product in query)
            {
                viewModels.Add(new ProductViewModel(product));
            }
            return viewModels;
        }

        public IEnumerable<ProductViewModel> ListMatching(string searchText, int limit = 50)
        {
            var entities = GeneralService.GetDbEntities();
            var products = entities.Products.Where(p => (p.Name != null && p.Name.Contains(searchText) || (p.Tags != null && p.Tags.Contains(searchText))));
            List<ProductViewModel> viewModels = new List<ProductViewModel>();
            foreach (Product product in products.Take(limit))
            {
                ProductViewModel viewModel = new ProductViewModel(product);
                viewModels.Add(viewModel);
            }
            return viewModels.AsEnumerable();
        }

        public IEnumerable<CategoryViewModel> ListCategories()
        {
            var entities = GeneralService.GetDbEntities();
            List<CategoryViewModel> viewModels = new List<CategoryViewModel>();
            foreach (Category category in entities.Categories)
            {
                viewModels.Add(new CategoryViewModel(category));
            }
            return viewModels;
        }

        public ProductViewModel Add(ProductViewModel viewModel)
        {
            var entities = GeneralService.GetDbEntities();
            Product product = entities.Products.Add(viewModel.ToEntity());
            entities.SaveChanges();
            viewModel.ID = product.ID;
            return viewModel;
        }

        public ProductViewModel Update(ProductViewModel viewModel)
        {
            var entities = GeneralService.GetDbEntities();
            Product product = entities.Products.FirstOrDefault(p => p.ID == viewModel.ID);
            if (product == null)
                return null;
            viewModel.Sync(product);
            entities.SaveChanges();
            return viewModel;
        }

        public bool Delete(long id)
        {
            var entities = GeneralService.GetDbEntities();
            Product product = entities.Products.FirstOrDefault(p => p.ID == id);
            if (product == null)
                return false;
            entities.Products.Remove(product);
#if DEBUG == false
            entities.SaveChanges();     // Dont delete records in debug mode
#endif
            return true;
        }

        public long? CreateOffer(long id, decimal price, long customerId)
        {
            var entities = GeneralService.GetDbEntities();
            Product product = entities.Products.FirstOrDefault(p => p.ID == id);
            if (product == null)
                return null;
            product.OfferPrice = price;
            TransactionLine transaction = new TransactionLine
            {
                ProductID = id,
                CategoryID = product.CategoryID,
                CustomerID = customerId,
                SellerID = product.CustomerID,
                OfferPrice = price,
                TransactionType = TransactionTypes.Offer,
                CreatedDate = DateTime.Now,
            };
            entities.TransactionLines.Add(transaction);
            entities.SaveChanges();
            return transaction.ID;
        }

        public TransactionModel GetLeadingOffer(long productId)
        {
            var entities = GeneralService.GetDbEntities();
            TransactionLine transaction = entities.TransactionLines.Where(t => t.ProductID == productId).OrderByDescending(p => p.OfferPrice).FirstOrDefault();
            if (transaction == null)
                return null;
            return new TransactionModel(transaction);
        }

        public IEnumerable<TransactionViewModel> ListTransactions(long productId)
        {
            var entities = GeneralService.GetDbEntities();
            List<TransactionViewModel> transactionViewModels = new List<TransactionViewModel>();
            var transactions = entities.TransactionLines.Where(t => t.ProductID == productId).OrderByDescending(t => t.ID);
            foreach (var transaction in transactions)
            {
                transactionViewModels.Add(new TransactionViewModel(transaction));
            }
            return transactionViewModels;
        }

        public long? AddImage(long productId, long imageStoreId)
        {
            var entities = GeneralService.GetDbEntities();
            var product = GeneralService.GetDbEntities().Products.Where(p => p.ID == productId).FirstOrDefault();
            if (product == null)
                return null;
            if (!product.PrimaryImageStoreID.HasValue || product.PrimaryImageStoreID.Value == 0)
                product.PrimaryImageStoreID = imageStoreId;
            var productImage = entities.ProductImages.Add(new ProductImage
            {
                ProductID = productId,
                ImageStoreID = imageStoreId
            });
            entities.SaveChanges();
            if (productImage != null) 
                return productImage.ID;
            return null;
        }

    }
}
