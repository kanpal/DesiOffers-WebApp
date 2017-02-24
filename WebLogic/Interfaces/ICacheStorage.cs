using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLogic.Interfaces
{
    public interface ICacheStorage
    {
        object RetrieveData(string key);
        void SaveData(string key, object data);
        void RemoveData(string key);
        void Clear();
    }
}
