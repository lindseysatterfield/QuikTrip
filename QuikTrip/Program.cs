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
                        var districtNameQuestionLoop = true;
                        while (districtNameQuestionLoop)
                        {
                            Console.Clear();
                            Console.WriteLine("Enter district name");
                            var userInput = Console.ReadLine();
                            if (DistrictRepository.GetDistricts().FirstOrDefault(district => district.Name == userInput) != null)
                            {
                                districtNameQuestionLoop = false;
                                DistrictRepository.GetDistricts().FirstOrDefault(district => district.Name == userInput).GetDistrictReport();
                            }
                        }
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
                        var newStoreDistrictLoop = true;
                        while (newStoreDistrictLoop)
                        {
                            newStoreDistrictLoop = false;
                            Console.WriteLine("Enter 'store' or 'district' to add");
                            var storeOrDistrict = Console.ReadLine();
                            switch (storeOrDistrict)
                            {
                                case "store":
                                    var userQuestionLoop = true;
                                    District district = null;
                                    while (userQuestionLoop)
                                    {
                                        Console.WriteLine("What district would you like to add this store to?");
                                        var userDistrictInput = Console.ReadLine();
                                        if (DistrictRepository.GetDistricts().FirstOrDefault(district => district.Name == userDistrictInput) != null)
                                        {
                                            district = DistrictRepository.GetDistricts().FirstOrDefault(district => district.Name == userDistrictInput);
                                            userQuestionLoop = false;
                                        }
                                       
                                    }

                                    Console.WriteLine("Please enter store name");
                                    var userStoreName = Console.ReadLine();

                                    long userStoreRetailQuarter = 0;
                                    userQuestionLoop = true;
                                    while (userQuestionLoop)
                                    {
                                        Console.WriteLine("Please enter retail sales for current quarter");
                                        userStoreRetailQuarter = long.Parse(Console.ReadLine());

                                        if (userStoreRetailQuarter.GetType() == typeof(long))
                                        {
                                            userQuestionLoop = false;
                                        }
                                    }

                                    long userStoreRetailYearly = 0;
                                    userQuestionLoop = true;
                                    while (userQuestionLoop)
                                    {
                                        Console.WriteLine("Please enter retail yearly sales");
                                        userStoreRetailYearly = long.Parse(Console.ReadLine());

                                        if (userStoreRetailYearly.GetType() == typeof(long))
                                        {
                                            userQuestionLoop = false;
                                        }
                                    }

                                    long userStoreGasQuarter = 0;
                                    userQuestionLoop = true;
                                    while (userQuestionLoop)
                                    {
                                        Console.WriteLine("Please enter gas sales for current quarter");
                                        userStoreGasQuarter = long.Parse(Console.ReadLine());

                                        if (userStoreGasQuarter.GetType() == typeof(long))
                                        {
                                            userQuestionLoop = false;
                                        }
                                    }

                                    long userStoreGasYearly = 0;
                                    userQuestionLoop = true;
                                    while (userQuestionLoop)
                                    {
                                        Console.WriteLine("Please enter gas yearly sales");
                                        userStoreGasYearly = long.Parse(Console.ReadLine());

                                        if (userStoreGasYearly.GetType() == typeof(long))
                                        {
                                            userQuestionLoop = false;
                                        }
                                    }

                                    StoreRepository.SaveNewStore(new Store(
                                        userStoreName,
                                        userStoreRetailQuarter,
                                        userStoreRetailYearly,
                                        userStoreGasQuarter,
                                        userStoreGasYearly
                                     ));

                                    district.Stores.Add(StoreRepository.GetSingleStore(userStoreName));

                                    Console.WriteLine("Store added");
                                    break;
                                
                                case "district":
                                    Console.WriteLine("You selected add district");
                                    userQuestionLoop = true;
                                    string userDistrictName = null;
                                    string userDistrictManager = null;
                                    while (userQuestionLoop)
                                    {
                                        Console.WriteLine("Please enter district name");
                                        userDistrictName = Console.ReadLine();
                                        Console.WriteLine("Enter district manager name");
                                        userDistrictManager = Console.ReadLine();

                                        if (userDistrictName.GetType() == typeof(string))
                                        {
                                            userQuestionLoop = false;
                                        }
                                    }
                                    DistrictRepository.SaveNewDistrict(new District(userDistrictName, userDistrictManager));
                                    Console.WriteLine("District added");
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice");
                                    break;
                            }
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
