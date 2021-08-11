using QuikTrip.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuikTrip.Employees
{
    class DistrictManager
    {
        public string Name { get; set; }
        public District ManagedDistrict { get; set; } // Maybe won't work, not neccessary

        public DistrictManager(string name, District managedDistrict)
        {
            Name = name;
            ManagedDistrict = managedDistrict;
        }
        // Needs methods for doing things (maybe have to get creative later)
    }
}
