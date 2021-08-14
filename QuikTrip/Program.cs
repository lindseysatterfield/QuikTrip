using QuikTrip.Districts;
using QuikTrip.Employees;
using QuikTrip.Repositories;
using QuikTrip.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using Spectre.Console;

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
                            List<Store> stores = StoreRepository.GetStores().ToList();
                            AnsiConsole.MarkupLine("[greenyellow]Please enter store name[/]");
                            Console.WriteLine();
                            Console.WriteLine("---Available Stores---");
                            foreach (var store in stores)
                            {
                                AnsiConsole.MarkupLine($"[blue]{store.Name}[/]");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            var storeName = Console.ReadLine();
                            if (StoreRepository.FindStore(storeName))
                            {
                                Console.Clear();
                                AnsiConsole.MarkupLine("[yellow]You found a matching store[/]");
                                Console.WriteLine();
                                // Display store employees
                                var index = StoreRepository.FindIndex(storeName);
                                var employees = mockStores[index].GetEmployees();
                                Console.WriteLine($"----Your favorite QuikTrip Store----");
                                AnsiConsole.MarkupLine($"[deeppink4_2]--List of {storeName}'s Awesome Employees--[/]");
                                var i = 1;
                                foreach (var employee in employees)
                                {
                                    Console.WriteLine("------------------------");
                                    Console.WriteLine($"{i++}. {employee.Title}");
                                    Console.WriteLine($"Name: {employee.Name}");
                                    Console.WriteLine($"Retail Sales: ${String.Format("{0:#,##0.##}", employee.EmployeeSales)}");
                                }
                                Console.WriteLine("------------------------");
                                // Store sales report
                                StoreRepository.StoreReport(storeName);
                                AnsiConsole.MarkupLine("[red3]---End of Report---[/]");
                                storeReportLoop = false;
                            }
                            else
                            {
                                Console.Clear();
                                AnsiConsole.MarkupLine("[red3_1]Invalid[/]");
                            }
                        }
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Add New Employee");
                        var newEmployeeLoop = true;
                        while (newEmployeeLoop)
                        {
                            newEmployeeLoop = false;
                            Console.WriteLine("Please enter store name to add employee:");
                            Console.WriteLine();
                            Console.WriteLine("---Available Stores---");
                            foreach (var store in mockStores)
                            {
                                Console.WriteLine(store.Name);
                            }
                            var storeName = Console.ReadLine();

                            Console.WriteLine($"You entered Store {storeName}. Is this the store you want to add an employee to? Yes or No.");
                            var correctStoreNameQuestion = Console.ReadLine();
                            switch (correctStoreNameQuestion.ToLower())
                            {
                                case "no":
                                    newEmployeeLoop = true;
                                    break;
                                case "yes":
                                    Console.WriteLine("Great. Lets add an employee.");
                                    Console.WriteLine();
                                    Console.WriteLine($"Please enter the new employee's name:");
                                    var newEmployeeName = Console.ReadLine();

                                    Console.WriteLine($"Enter {newEmployeeName}'s Title:");
                                    var newEmployeeTitle = Console.ReadLine();

                                    long newEmployeeRetailSales = 0;
                                    var userQuestionLoop = true;
                                    while (userQuestionLoop)
                                    {
                                        Console.WriteLine("Enter Employee Retail Sales total as 0");
                                        newEmployeeRetailSales = long.Parse(Console.ReadLine());

                                        if (newEmployeeRetailSales.GetType() == typeof(long))
                                        {
                                            userQuestionLoop = false;
                                        }
                                    }

                                    if (StoreRepository.FindStore(storeName))
                                    {
                                        Console.Clear();
                                        var index = StoreRepository.FindIndex(storeName);
                                        mockStores[index].AddEmployee(new EmployeeBase(newEmployeeName, newEmployeeTitle, newEmployeeRetailSales));
                                        Console.WriteLine($"{newEmployeeName} has been added to {storeName} as {newEmployeeTitle}.");
                                        Console.WriteLine();
                                        var employeeList = mockStores[index].GetEmployees();
                                        Console.WriteLine($"--List of {storeName}'s Employees--");
                                        foreach (var employee in employeeList)
                                        {
                                            Console.WriteLine("------------------------");
                                            Console.WriteLine($"Title: {employee.Title}");
                                            Console.WriteLine($"Name: {employee.Name}");
                                            Console.WriteLine($"Retail Sales: ${String.Format("{0:#,##0.##}", employee.EmployeeSales)}");
                                        }
                                        Console.WriteLine("------------------------");
                                    }
                                    break;
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

                                    Console.WriteLine("Please enter district name");
                                    var userDistrictName = Console.ReadLine();

                                    Console.WriteLine("Enter district manager name");
                                    var userDistrictManager = Console.ReadLine();

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
