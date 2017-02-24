using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLogic.Models;

namespace WebLogic.ViewModels
{
    public class TransactionViewModel
    {
        public long ID { get; set; }
        public long? ProductID { get; set; }
        public long? CustomerID { get; set; }
        public string CustomerName { get; set; }
        public decimal? OfferPrice { get; set; }
        public string PriceFormatted { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }

        public TransactionViewModel(TransactionLine transaction)
        {
            ID = transaction.ID;
            ProductID = transaction.ProductID;
            CustomerID = transaction.CustomerID;
            if (transaction.Customer != null)
                CustomerName = transaction.Customer.FirstName + " " + transaction.Customer.LastName;
            OfferPrice = transaction.OfferPrice;
            PriceFormatted = string.Format("{0:C}", transaction.OfferPrice ?? 0);
            Type = TransactionTypes.GetDescription(transaction.TransactionType);
            Date = transaction.CreatedDate.ToString("g");
        }
    }
}
