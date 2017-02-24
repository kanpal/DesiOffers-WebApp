using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;
using WebLogic.Interfaces;
using WebLogic.Models;

namespace WebLogic.Services
{
    public class GeneralService : IService
    {
        private static DesiOfferEntities _entities = new DesiOfferEntities();
        public GeneralService()
        {
        }

        public string GetName()
        {
            return "General";
        }

        public static DesiOfferEntities GetDbEntities()
        {
            // TODO: Use Unity/DI
            return new DesiOfferEntities();// _entities;
        }

        public static SearchService GetSearchService()
        {
            return new SearchService();
        }

        public static ProductService GetProductService()
        {
            return new ProductService();
        }

        public static CustomerService GetCustomerService()
        {
            return new CustomerService();
        }

        public static ImageService GetImageService()
        {
            return new ImageService();
        }

        public static LocationService GetLocationService()
        {
            return new LocationService();
        }

        public static PathingService GetPathingService()
        {
            return new PathingService();
        }

        #region Select Lists

        public static List<SelectListItem> GetCategories()
        {
            List<SelectListItem> categories = new List<SelectListItem>();

            foreach (Category category in _entities.Categories)
            {
                categories.Add(new SelectListItem { Text = category.Name, Value = category.ID.ToString() });
            }

            return categories;
        }

        #endregion

        #region Counter Service

        private static object _counterLock = new object();
        public static Int64 CounterNextValue(string key)
        {
            lock (_counterLock)      // KP: 1/7/14 - Get into thread exclusive section
            {
                int attempts = 5;
                while (attempts-- > 0)
                {
                    try
                    {
                        using (var transactionScope = new TransactionScope(TransactionScopeOption.Suppress))
                        {
                            var outParameter = new SqlParameter("@value", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
                            GetDbEntities().Database.ExecuteSqlCommand("CounterNextValue @key, @value OUT", new SqlParameter("@key", key), outParameter);
                            transactionScope.Complete();
                            return (Int64)outParameter.Value;
                        }
                    }
                    catch (Exception exception)
                    {
                        //LogMessageHelper.LogError(exception, SystemSettings.ElmahExceptionPolicy());
                    }
                    // Wait before the next retry
                    System.Threading.Thread.Sleep(300);
                }
            }
            throw new Exception("Unable to obtain the next counter value for " + key);
        }

        public static void CounterReturnValue(string key, Int64 value)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Suppress))
            {
                GetDbEntities().Database.ExecuteSqlCommand("CounterReturnValue @key, @value", new SqlParameter("@key", key), new SqlParameter("@value", value));
                transactionScope.Complete();
            }
        }

        public static void CounterResetValue(string key, Int64 value)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Suppress))
            {
                GetDbEntities().Database.ExecuteSqlCommand("CounterResetValue @key, @value", new SqlParameter("@key", key), new SqlParameter("@value", value));
                transactionScope.Complete();
            }
        }

        #endregion
    }

    public static class CounterNames
    {
        public const string ImageId = "ImageId";
    }
}
