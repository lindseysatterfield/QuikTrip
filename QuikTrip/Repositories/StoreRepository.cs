using QuikTrip.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuikTrip.Repositories
{
    static class StoreRepository
    {

        static List<Store> _stores = new List<Store>
        {
            new Store("QuikTrip5000", 1233, 1515151, 3241, 366000),
            new Store("QuikTrip22222", 333, 215151, 1666, 1415150),
            new Store("QuikTrip11111", 444, 444451, 3441, 34444000),
        };

        public static List<Store> GetStores()
        {
            return _stores;
        }

        public static void SaveNewStore(Store Store)
        {
            _stores.Add(Store);
        }

        public static Store GetSingleStore(string name)
        {
            return StoreRepository.GetStores().FirstOrDefault(store => store.Name == name);
        }
    }
}
