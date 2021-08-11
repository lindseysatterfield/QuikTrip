using QuikTrip.Employees;
using QuikTrip.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuikTrip.Districts
{
    class District
    {
        public string Name { get; }
        public DistrictManager DistManager { get; set; }
        public List<Store> Stores = new List<Store>();
        public long RetailCurrentQuarter { get; set; }
        public long RetailYearly { get; set; }
        public long GasCurrentQuarter { get; set; }
        public long GasYearly { get; set; }

        public District(string name, string ManagerName)
        {
            Name = name;
            DistManager = new DistrictManager(ManagerName, this);
            RetailCurrentQuarter = 0;
            RetailYearly = 0;
            GasCurrentQuarter = 0;
            GasYearly = 0;

            

        }
        public void GetDistrictReport()
        {
            Console.WriteLine($@"
            {Name} Sales Report");
            foreach (var store in Stores)
            {
                Stores.ForEach(store =>
                {
                    // Calculates the most up to date sales for all the districts stores
                    RetailCurrentQuarter = 0;
                    RetailYearly = 0;
                    GasCurrentQuarter = 0;
                    GasYearly = 0;
                    RetailCurrentQuarter += store.RetailCurrentQuarter;
                    RetailYearly += store.RetailYearly;
                    GasCurrentQuarter += store.GasCurrentQuarter;
                    GasYearly += store.GasYearly;

                    Console.WriteLine(@$"
                    -----------------------------------------
                    Store {store.Name}
                    -----------------------------------------
");
                    var employees = store.GetEmployees();
                    var i = 0;
                    foreach (var employee in employees)
                    {
                        Console.WriteLine($@"
                        {i++}. {employee.Title}, {employee.Name}:
                               {employee.}
");
                    }
                });

            }
        }
    }
}
