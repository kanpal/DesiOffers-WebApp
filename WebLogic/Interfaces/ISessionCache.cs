using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLogic.Interfaces
{
    public interface ISessionCache : ICacheStorage
    {
        object this[string key] { get; set; }
    }
}
