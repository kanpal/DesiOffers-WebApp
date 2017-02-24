using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLogic.Models
{
    public static class Constants
    {
    }

    public static class TransactionTypes
    {
        public const string Offer = "OF";
        public const string Credit = "CR";
        public const string Debit = "DB";
        public const string Void = "VD";

        public static string GetDescription(string type)
        {
            switch (type)
            {
                case Offer: return "Offer";
                case Credit: return "Credit";
                case Debit: return "Debit";
                case Void: return "Void";
            }
            return string.Empty;
        }
    }
}
