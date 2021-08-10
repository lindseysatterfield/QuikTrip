﻿using System;

namespace QuikTrip
{
    class Program
    {
        static void Main(string[] args)
        {
            var menuLoop = true;
            while (menuLoop)
            {
                
                Console.WriteLine(@"
QuikTrip Management Systems

1. Enter District Sales
2. Generate District Report
3. Add New Employee
4. Add a Store/District
5. Exit");
                var userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        Console.WriteLine("You chose to enter district sales");
                        break;
                    case "2":
                        Console.WriteLine("You chose to enter district sales");
                        break;
                    case "3":
                        Console.WriteLine("Add New Employee");
                        break;
                    case "4":
                        Console.WriteLine("You chose to Add a Store/District");
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
