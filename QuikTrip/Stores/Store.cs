using QuikTrip.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuikTrip.Stores
{
    class Store
    {
        private List<EmployeeBase> _employees = new List<EmployeeBase>();
        public string Name { get; set; } //QuikTripW500
        public long RetailCurrentQuarter { get; private set; }
        public long RetailYearly { get; set; }
        public long GasCurrentQuarter { get; set; }
        public long GasYearly { get; set; }
        public long EmployeeSales { get; set; }

        public Store(string name, long retailCurrentQuarter, long retailYearly, long gasCurrentQuarter, long gasYearly)
        {
            Name = name;
            RetailCurrentQuarter = retailCurrentQuarter;
            RetailYearly = retailYearly;
            GasCurrentQuarter = gasCurrentQuarter;
            GasYearly = gasYearly;
        }
        public void AddEmployee(EmployeeBase employee)
        {
            _employees.Add(employee);
        }
        public List<EmployeeBase> GetEmployees()
        {
            return _employees;
        }
    }
}
