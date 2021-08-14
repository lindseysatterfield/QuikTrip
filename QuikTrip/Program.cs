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
                            AnsiConsole.MarkupLine("[greenyellow]Please enter store name[/]");
                            Console.WriteLine();
                            Console.WriteLine("---Available Stores---");
                            foreach (var store in mockStores)
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
