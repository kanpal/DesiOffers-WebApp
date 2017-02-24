using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLogic.Interfaces;
using WebLogic.Services;

namespace WebLogic.Framework
{
    public class BaseEntityService<T, TModel> : IService
    {
        public virtual string GetName()
        {
            return "Base";
        }
        /*
        public TModel Get(long id)
        {
            var customer = GeneralService.GetDbEntities().Set<T>().Where(c => c.ID == id).FirstOrDefault();
            if (customer == null)
                return null;
            return new CustomerViewModel(customer);
        }
        */
    }
}
