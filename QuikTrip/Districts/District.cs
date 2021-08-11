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

            RetailCurrentQuarter = 0;
            RetailYearly = 0;
            GasCurrentQuarter = 0;
            GasYearly = 0;
            Console.WriteLine($@"
                    {Name} Sales Report");

            foreach (var store in Stores)
            {
                
                    // Calculates the most up to date sales for all the districts stores
                    
                    RetailCurrentQuarter += store.RetailCurrentQuarter;
                    RetailYearly += store.RetailYearly;
                    GasCurrentQuarter += store.GasCurrentQuarter;
                    GasYearly += store.GasYearly;

                    Console.WriteLine(@$"
                    ------------------------------
                    {store.Name}
                    ------------------------------
");
                    var employees = store.GetEmployees();
                    var i = 1;
                    foreach (var employee in employees)
                    {
                        Console.WriteLine($@"
                    {i++}. {employee.Title}, {employee.Name}:
                           Retail Sales: ${employee.EmployeeSales}

");
                    };
                Console.WriteLine($@"
                    Retail Current Quarter: ${store.RetailCurrentQuarter}
                    Retail Yearly:  ${store.RetailYearly}
                    Gas Current Quarter: ${store.GasCurrentQuarter}
                    Gas Yearly: ${store.GasYearly}
");

            }
                Console.WriteLine($@"
                    =======================
                    District Totals

                    Retail Current Quarter: ${RetailCurrentQuarter}
                    Retail Yearly:  ${RetailYearly}
                    Gas Current Quarter: ${GasCurrentQuarter}
                    Gas Yearly: ${GasYearly}
");
        }
    }
}
