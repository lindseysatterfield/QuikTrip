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


        // Find if store exists to generate report
        public static bool FindStore(string name)
        {
            var result = _stores.Any(x => x.Name == name);
            return result;
        }


        // Generate Store Report
        public static void StoreReport(string name)
        {
            var storeReport = _stores.Where(x => x.Name == name);
            
            foreach (var store in storeReport)
            {
                Console.WriteLine($"Gas Yearly: ${String.Format("{0:#,##0.##}",store.GasYearly)}");
                Console.WriteLine($"Gas Current Quarter: ${String.Format("{0:#,##0.##}", store.GasCurrentQuarter)}");
                Console.WriteLine($"Retail Yearly: ${String.Format("{0:#,##0.##}", store.RetailYearly)}");
                Console.WriteLine($"Retail Current Quarter: ${String.Format("{0:#,##0.##}", store.RetailCurrentQuarter)}");
            }
            
        }

        // Find index of the store from the _stores
        public static int FindIndex(string name)
        {
            int index = _stores.FindIndex(x => x.Name == name);
            return index;
        }

        public static Store GetSingleStore(string name)
        {
            return StoreRepository.GetStores().FirstOrDefault(store => store.Name == name);
        }
    }
}
