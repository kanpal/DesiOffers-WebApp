using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLogic.DataModels;
using WebLogic.Models;

namespace WebLogic.ViewModels
{
    public class TransactionViewModel : TransactionModel
    {
        public string PriceFormatted { get; set; }
        public TransactionViewModel(TransactionLine transaction)
            : base(transaction)
        {
            PriceFormatted = string.Format("{0:C}", transaction.OfferPrice ?? 0);
        }
    }
}
