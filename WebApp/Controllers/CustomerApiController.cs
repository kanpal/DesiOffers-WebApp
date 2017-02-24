using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using WebLogic.Framework;
using WebLogic.Models;
using WebLogic.Services;
using WebLogic.ViewModels;

namespace WebApp.Controllers
{
    public class CustomerApiController : ApiController
    {
        [HttpGet]
        [HttpPost]
        [ResponseType(typeof(CustomerViewModel))]
        public IHttpActionResult Get(long id)
        {
            CustomerService customerService = GeneralService.GetCustomerService();
            return Ok(customerService.Get(id));
        }

        [HttpGet]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<CustomerViewModel>))]
        public IHttpActionResult List(string searchText)
        {
            CustomerService customerService = GeneralService.GetCustomerService();
            return Ok(customerService.ListMatching(searchText).AsEnumerable());
        }

        [HttpGet]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<CustomerViewModel>))]
        public IHttpActionResult ListByZipCode(string zipCode)
        {
            CustomerService customerService = GeneralService.GetCustomerService();
            if (!string.IsNullOrWhiteSpace(zipCode))
            {
                return Ok(customerService.ListInZipCode(zipCode).AsEnumerable());
            }
            return Ok();
        }

        [HttpGet]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<CustomerViewModel>))]
        public IHttpActionResult ListByPosition(float? latitude, float? longitude)
        {
            CustomerService customerService = GeneralService.GetCustomerService();
            if (latitude.HasValue && longitude.HasValue)
            {
                LocationService service = new LocationService();
                var zipCodes = service.GetZipCodesInRange(latitude.Value, longitude.Value).Select(x => x.Zipcode).ToList();
                return Ok(customerService.ListInZipCodes(zipCodes).AsEnumerable());
            }
            return Ok();
        }

        [HttpPost]
        [ResponseType(typeof(IEnumerable<TransactionViewModel>))]
        public IHttpActionResult ListTransactions(long id)
        {
            CustomerService customerService = GeneralService.GetCustomerService();
            return Ok(customerService.ListTransactions(id));
        }
    }
}
