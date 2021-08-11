using QuikTrip.Districts;
using QuikTrip.Employees;
using QuikTrip.Repositories;
using QuikTrip.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuikTrip
{
    class Program
    {
        static void Main(string[] args)
        {
            // Mock Data Stuff

            District mockDistrict = DistrictRepository.GetDistricts().Where(district => district.Name == "d1").First();
            List<Store> mockStores = StoreRepository.GetStores().ToList();
            mockStores[0].AddEmployee(new EmployeeBase("Jdog", "Chicken Eating Chump", 300000));
            mockDistrict.Stores.AddRange(mockStores);

            // Keeps Menu Looping
            var menuLoop = true;

            while (menuLoop)
            {
                //Console.Clear();
                Console.WriteLine(@"
QuikTrip Management Systems

1. Get District Report
2. Get Store Report
3. Add New Employee
4. Add a New Store/District
5. Exit
");
                var userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        Console.WriteLine("Get District Report"); // Ask user for a district name
                        break;
                    case "2":
                        Console.WriteLine("Get Store Report"); // Ask user for a specific store name
                        Console.WriteLine();
                        var storeReportLoop = true;

                        while (storeReportLoop)
                        {
                            Console.WriteLine("Please enter store name");
                            Console.WriteLine();
                            Console.WriteLine("---Available Stores---");
                            foreach (var store in mockStores)
                            {
                                Console.WriteLine(store.Name);
                            }

                            var storeName = Console.ReadLine();
                            if (StoreRepository.FindStore(storeName))
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("You found a matching store");
                                Console.ForegroundColor = ConsoleColor.White;
                                
                                // Display store employees
                                var index = StoreRepository.FindIndex(storeName);
                                var employees = mockStores[index].GetEmployees();
                                Console.WriteLine($"Information about {storeName}");
                                Console.WriteLine("--List of Employees--");
                                foreach (var employee in employees)
                                {
                                    for (var i = 0; i < employees.Count; i++)
                                    {
                                        Console.WriteLine("-------------------");
                                        Console.WriteLine($"{i + 1}. {employee.Title}");
                                        Console.WriteLine($"Name: {employee.Name}");
                                        Console.WriteLine($"Retail Sales: ${String.Format("{0:#,##0.##}", employee.EmployeeSales)}");
                                    }
                                }
                                // Store sales report
                                Console.WriteLine("--Store report--");
                                StoreRepository.StoreReport(storeName);
                                storeReportLoop = false;
                            } else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid");
                                Console.ForegroundColor = ConsoleColor.White;
                            }       
                        }
                        break;
                    case "3":
                        Console.WriteLine("Add New Employee");
                        break;
                    case "4":
                        Console.WriteLine("Add a new store or new district");
                        var newStoreDistrictLoop = true;
                        while (newStoreDistrictLoop)
                        {
                            newStoreDistrictLoop = false;
                        }
                        break;
                    case "5":
                        Console.WriteLine("Exiting");
                        menuLoop = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
    }
}
