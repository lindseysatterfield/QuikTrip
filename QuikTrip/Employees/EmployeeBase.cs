using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuikTrip.Employees
{
    class EmployeeBase
    {
        public string Name { get; set; }
        public string Title { get; set; } = "Chump";
        public long RetailCurrentQuarter { get; private set; } = 10200;
        public long RetailYearly { get; set; } = 10015150;
        public long GasCurrentQuarter { get; set; } = 10455100;
        public long GasYearly { get; set; } = 102300;
        public long EmployeeSales { get; set; } = 100220;
        public EmployeeBase(string name, string title)
        {
            Name = name;
            Title = title;
        }
        // Methods should go in the inherited classes
    }
}
