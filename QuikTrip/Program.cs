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
            mockStores[0].AddEmployee(new EmployeeBase("Jesse", "Manager", 2010));
            mockStores[0].AddEmployee(new EmployeeBase("Jody", "Associate", 3150));
            mockStores[0].AddEmployee(new EmployeeBase("Tim", "Assistant Manager", 1316));
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
                        DistrictManager.RequestDistrictReport();
                        break;
                    case "2":
                        Console.Clear();
                        var storeReportLoop = true;
                        while (storeReportLoop)
                        {
                            Console.WriteLine("Please enter store name");
                            Console.WriteLine();
                            Console.WriteLine("---Available Stores---");
                            foreach (var store in mockStores)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine(store.Name);
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            var storeName = Console.ReadLine();
                            if (StoreRepository.FindStore(storeName))
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("You found a matching store");
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.White;
                                // Display store employees
                                var index = StoreRepository.FindIndex(storeName);
                                var employees = mockStores[index].GetEmployees();
                                Console.WriteLine($"----Your favorite QuikTrip Store----");
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine($"--List of {storeName}'s Awesome Employees--");
                                Console.ForegroundColor = ConsoleColor.White;
                                foreach (var employee in employees)
                                {
                                    Console.WriteLine("------------------------");
                                    Console.WriteLine($"Title: {employee.Title}");
                                    Console.WriteLine($"Name: {employee.Name}");
                                    Console.WriteLine($"Retail Sales: ${String.Format("{0:#,##0.##}", employee.EmployeeSales)}");
                                }
                                Console.WriteLine("------------------------");
                                // Store sales report
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine($"----Store Sales Report for {storeName}-----");
                                Console.ForegroundColor = ConsoleColor.White;
                                StoreRepository.StoreReport(storeName);
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("---End of Report---");
                                Console.ForegroundColor = ConsoleColor.White;
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
