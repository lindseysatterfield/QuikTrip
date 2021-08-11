using QuikTrip.Districts;
using QuikTrip.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuikTrip.Repositories
{
    static class DistrictRepository
    {
        static List<District> _districts = new List<District>
        {
            new District("d1", "jesse"),
        }
        ;
        public static List<District> GetDistricts()
        {
            return _districts;
        }
        public static void SaveNewDistrict(District District)
        {
            _districts.Add(District);
        }
    }
}
