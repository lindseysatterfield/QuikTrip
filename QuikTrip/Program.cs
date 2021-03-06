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
                var highlightStyle = new Style().Foreground(Color.Lime);

                var userChoice = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("[green]QuikTrip[/] Management Systems")
        .HighlightStyle(highlightStyle)
        .PageSize(10)
        .AddChoices(new[] {
            "1. Get District Report", "2. Get Store Report", "3. Add New Employee", "4. Add A New Store or District", "5. Exit Application"
        }));
                switch (userChoice)
                {
                    case "1. Get District Report":
                        DistrictManager.RequestDistrictReport();
                        break;
                    case "2. Get Store Report":
                        Console.Clear();
                        var storeReportLoop = true;
                        while (storeReportLoop)
                        {
                            List<Store> stores = StoreRepository.GetStores().ToList();
                            AnsiConsole.MarkupLine("[greenyellow]Please choose a store[/]");
                            var storeNames = new List<string>();
                            stores.ForEach(store => storeNames.Add(store.Name));
                            var storeName = AnsiConsole.Prompt(
                                  new SelectionPrompt<string>()
                                      .Title("---Available Stores---")
                                      .HighlightStyle(highlightStyle)
                                      .PageSize(10)
                                      .AddChoices(storeNames));
                            if (StoreRepository.FindStore(storeName))
                            {
                                Console.Clear();
                                AnsiConsole.MarkupLine("[yellow]You found a matching store[/]");
                                Console.WriteLine();
                                // Display store employees
                                var index = StoreRepository.FindIndex(storeName);
                                var employees = stores[index].GetEmployees();
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
                    case "3. Add New Employee":
                        Console.WriteLine("Add New Employee");
                        var newEmployeeLoop = true;
                        while (newEmployeeLoop)
                        {
                            var storeNames = new List<string>();
                            var stores = StoreRepository.GetStores().ToList();
                            stores.ForEach(store => storeNames.Add(store.Name));
                            newEmployeeLoop = false;
                            var storeName = AnsiConsole.Prompt(
                                  new SelectionPrompt<string>()
                                      .Title("Please choose a store to add employee to")
                                      .HighlightStyle(highlightStyle)
                                      .PageSize(10)
                                      .AddChoices(storeNames));
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
                                        stores[index].AddEmployee(new EmployeeBase(newEmployeeName, newEmployeeTitle, newEmployeeRetailSales));
                                        Console.WriteLine($"{newEmployeeName} has been added to {storeName} as {newEmployeeTitle}.");
                                        Console.WriteLine();
                                        var employeeList = stores[index].GetEmployees();
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
                    case "4. Add A New Store or District":
                        var newStoreDistrictLoop = true;
                        while (newStoreDistrictLoop)
                        {
                            newStoreDistrictLoop = false;
                 
                            var storeOrDistrict = AnsiConsole.Prompt(
                              new SelectionPrompt<string>()
                                  .Title("Choose what to add.")
                                  .PageSize(10)
                                  .HighlightStyle(highlightStyle)
                                  .AddChoices(new[] {
                                     "store","district"
                                  }));
                            switch (storeOrDistrict)
                            {
                                case "store":
                                    var userQuestionLoop = true;
                                    District district = null;
                                    while (userQuestionLoop)
                                    {
                                        var highlightStyle1 = new Style().Foreground(Color.Lime);
                                        var districtNames = new List<string>();
                                        DistrictRepository.GetDistricts().ToList().ForEach(district => districtNames.Add(district.Name));
                                        var userDistrictInput = AnsiConsole.Prompt(
                                            new SelectionPrompt<string>()
                                                .Title("Districts")
                                                .PageSize(10)
                                                .HighlightStyle(highlightStyle1)
                                                .AddChoices(districtNames));

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
                    case "5. Exit Application":
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
