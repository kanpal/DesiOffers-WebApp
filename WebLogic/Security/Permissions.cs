using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLogic.Security
{
    public class Permissions
    {
        // Products
        public const string CreateProducts = "products.create";
        public const string EditProducts = "products.edit";
        public const string DeleteProducts = "products.delete";

        // Customers
        public const string CreateCustomers = "customers.create";
        public const string EditCustomers = "customers.edit";
        public const string DeleteCustomers = "customers.delete";

        // General
        public const string SystemAdministration = "system.administration";
    }
}
