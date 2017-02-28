using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLogic.Models;

namespace WebLogic.DataModels
{
    public class TransactionModel
    {
        public long ID { get; set; }
        public long? ProductID { get; set; }
        public string ProductName { get; set; }
        public long? CustomerID { get; set; }
        public string CustomerName { get; set; }
        public decimal? OfferPrice { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }

        public TransactionModel(TransactionLine transaction)
        {
            ID = transaction.ID;
            ProductID = transaction.ProductID;
            CustomerID = transaction.CustomerID;
            OfferPrice = transaction.OfferPrice;
            Type = TransactionTypes.GetDescription(transaction.TransactionType);
            Date = transaction.CreatedDate.ToString("g");
            if (transaction.Customer != null)
                CustomerName = transaction.Customer.FirstName + " " + transaction.Customer.LastName;
            if (transaction.Product != null)
                ProductName = transaction.Product.Name;
        }

    }
}
