using QuikTrip.Employees;
using QuikTrip.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
       // Needs Methods for stuff
    }
}
