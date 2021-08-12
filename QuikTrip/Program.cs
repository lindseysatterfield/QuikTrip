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
            mockStores[0].AddEmployee(new EmployeeBase("Jdog", "Chicken Eating Chump", 169985774));
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
                        break;
                    case "3":
                        Console.WriteLine("Add New Employee");
                        var newEmployeeLoop = true;
                        while (newEmployeeLoop)
                        {
                            newEmployeeLoop = false;
                            Console.WriteLine("Enter Store # to add Employee:");
                            var storeNumber = Console.ReadLine();

                            Console.WriteLine($"You entered Store # {storeNumber}. Is this the store you want to add an employee to? Yes or No.");
                            var correctStoreNumberQuestion = Console.ReadLine();
                            switch (correctStoreNumberQuestion.ToLower())
                            {
                                case "no":
                                    newEmployeeLoop = true;
                                    break;
                                case "yes":
                                    Console.WriteLine("Great. Lets add an employee.");
                                    Console.WriteLine($"")
                                default:
                                    Console.WriteLine("Invalid Store #");
                                    break;
                            }
                        }
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
