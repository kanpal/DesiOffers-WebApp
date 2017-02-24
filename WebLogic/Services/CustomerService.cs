using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLogic.Framework;
using WebLogic.Interfaces;
using WebLogic.Models;
using WebLogic.ViewModels;

namespace WebLogic.Services
{
    public class CustomerService : BaseEntityService<Customer, CustomerViewModel>
    {
        public CustomerService()
        {
        }

        public override string GetName()
        {
            return "CustomerService";
        }

        public CustomerViewModel Get(long id)
        {
            var customer = GeneralService.GetDbEntities().Customers.Where(c => c.ID == id).FirstOrDefault();
            if (customer == null)
                return null;
            return new CustomerViewModel(customer);
        }

        public IEnumerable<CustomerViewModel> ListMatching(string searchText)
        {
            searchText = searchText ?? string.Empty;
            searchText = searchText.Trim().ToLower();
            DesiOfferEntities entities = GeneralService.GetDbEntities();
            var query = entities.Customers.Where(c => (c.FirstName != null && c.FirstName.ToLower().Contains(searchText)) || (c.LastName != null && c.LastName.ToLower().Contains(searchText)));
            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            foreach (Customer customer in query)
            {
                customers.Add(new CustomerViewModel(customer));
            }
            return customers;
        }

        public IEnumerable<CustomerViewModel> ListInZipCode(string zipCode)
        {
            DesiOfferEntities entities = GeneralService.GetDbEntities();
            var query = entities.Customers.Where(c => c.ZipCode == zipCode);
            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            foreach (Customer customer in query)
            {
                customers.Add(new CustomerViewModel(customer));
            }
            return customers;
        }

        public IEnumerable<CustomerViewModel> ListInZipCodes(List<string> zipCodes)
        {
            DesiOfferEntities entities = GeneralService.GetDbEntities();
            var query = entities.Customers.Where(c => zipCodes.Contains(c.ZipCode));
            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            foreach (Customer customer in query)
            {
                customers.Add(new CustomerViewModel(customer));
            }
            return customers;
        }

        public IEnumerable<TransactionViewModel> ListTransactions(long customerId)
        {
            var entities = GeneralService.GetDbEntities();
            List<TransactionViewModel> transactionViewModels = new List<TransactionViewModel>();
            var transactions = entities.TransactionLines.Where(t => t.CustomerID == customerId).OrderByDescending(t => t.ID);
            foreach (var transaction in transactions)
            {
                transactionViewModels.Add(new TransactionViewModel(transaction));
            }
            return transactionViewModels;
        }
    }
}
