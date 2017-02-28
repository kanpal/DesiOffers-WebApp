using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLogic.Models;

namespace WebLogic.ViewModels
{
    public class CustomerViewModel
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        [DisplayName("Email")]
        public string EmailId { get; set; }
        [DisplayName("Phone")]
        public string PhoneNumber { get; set; }
        public bool Verified { get; set; }
        public bool Active { get; set; }
        public long? ProfileImageID { get; set; }

        public string FullName
        {
            get { return (LastName + ", " + FirstName); }
        }

        public CustomerViewModel()
        {
        }

        public CustomerViewModel(Customer customer)
        {
            ID = customer.ID;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            City = customer.City;
            ZipCode = customer.ZipCode;
            EmailId = customer.EmailId;
            PhoneNumber = customer.PhoneNumber;
            Verified = customer.Verified ?? false;
            Active = customer.Active ?? false;
            ProfileImageID = customer.ProfileImageID;
        }
    }
}
