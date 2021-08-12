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
                        Console.WriteLine("Get Store Report"); // Ask user for a specific store name
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
                            Console.WriteLine("Enter store or district to add");
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

                                    Console.WriteLine("Please enter store name.");
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

                                    Console.WriteLine("Store saved");
                                    
                                    
                                    break;
                                case "district":
                                    Console.WriteLine("You selected add district");
                                    var userDistrictInfo = Console.ReadLine();
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
