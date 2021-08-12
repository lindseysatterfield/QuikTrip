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
        public string Title { get; set; }
        public long EmployeeSales { get; set; } //arbitrary number
        public EmployeeBase(string name, string title, long employeeSales)
        {
            Name = name;
            Title = title;
            EmployeeSales = employeeSales;
        }
        // Methods should go in the inherited classes
    }
}
