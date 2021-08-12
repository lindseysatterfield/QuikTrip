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
