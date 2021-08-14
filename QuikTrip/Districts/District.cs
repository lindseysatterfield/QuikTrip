using QuikTrip.Employees;
using QuikTrip.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace QuikTrip.Districts
{
    class District
    {
        public string Name { get; }
        public DistrictManager DistManager { get; set; }
        public List<Store> Stores = new List<Store>();
        public long RetailCurrentQuarter { get; set; }
        public long RetailYearly { get; set; }
        public long GasCurrentQuarter { get; set; }
        public long GasYearly { get; set; }

        public District(string name, string ManagerName)
        {
            Name = name;
            DistManager = new DistrictManager(ManagerName, this);
            RetailCurrentQuarter = 0;
            RetailYearly = 0;
            GasCurrentQuarter = 0;
            GasYearly = 0;
        }
        public void GetDistrictReport()
        {

            RetailCurrentQuarter = 0;
            RetailYearly = 0;
            GasCurrentQuarter = 0;
            GasYearly = 0;

            var outerTable = new Table();
            outerTable.Expand();
            outerTable.Centered();
            outerTable.AddColumn($"[green]{Name} Sales Report | District Manager, {DistManager.Name}[/]");
            
            foreach (var store in Stores)
            {
                
                    // Calculates the most up to date sales for all the districts stores
                    
                RetailCurrentQuarter += store.RetailCurrentQuarter;
                RetailYearly += store.RetailYearly;
                GasCurrentQuarter += store.GasCurrentQuarter;
                GasYearly += store.GasYearly;

                var employees = store.GetEmployees();

                // Constructing Employee table
                var employeeTable = new Table();
                employeeTable.Width(75);
                employeeTable.Centered();
                employeeTable.AddColumn("Name");
                employeeTable.AddColumn("Title");
                employeeTable.AddColumn("Retail Sales");

                foreach (var employee in employees)
                {
                    employeeTable.AddRow(employee.Name, employee.Title, $"[lime]${employee.EmployeeSales}[/]");
                };

                var table = new Table();
                var salesTable = new Table();
                salesTable.Width(75);
                salesTable.Centered();
                salesTable.AddColumns("Fiscal Time Frames", "Amount");
                salesTable.AddRow("Retail Current Quarter", $"[lime]${store.RetailCurrentQuarter}[/]");
                salesTable.AddRow("Retail Yearly", $"[lime]${store.RetailYearly}[/]");
                salesTable.AddRow("Gas Current Quarter", $"[lime]${store.GasCurrentQuarter}[/]");
                salesTable.AddRow("Gas Yearly", $"[lime]${store.GasYearly}[/]");
                table.Width(100);
                table.Centered();
                table.AddColumn($"[green]{store.Name}[/]");
                

                

                table.AddEmptyRow();
                table.AddRow("Employees");
                table.AddRow(employeeTable);
                table.AddRow("Sales");
                table.AddRow(salesTable);

                outerTable.AddRow(table);

                

            }
            var totalsTable = new Table();
            totalsTable.Centered();
            totalsTable.AddColumn("[green]District Totals[/]");
            totalsTable.AddRow($"Retail Current Quarter: [lime]${ RetailCurrentQuarter}[/]");
            totalsTable.AddRow($"Retail Yearly:  [lime]${RetailYearly}[/]");
            totalsTable.AddRow($"Gas Current Quarter: [lime]${GasCurrentQuarter}[/]");
            totalsTable.AddRow($"Gas Yearly: [lime]${GasYearly}[/]");
            outerTable.AddRow(totalsTable);
            AnsiConsole.Render(outerTable);
            
        }
    }
}
