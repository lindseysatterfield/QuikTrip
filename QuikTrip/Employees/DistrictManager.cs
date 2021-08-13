using QuikTrip.Districts;
using QuikTrip.Repositories;
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
        private string _DistrictManagerID;
        private static List<string> _UniqueIds = new List<string> { "0529" };
        public District ManagedDistrict { get; set; } // Maybe won't work, not neccessary

        public DistrictManager(string name, District managedDistrict)
        {
            Name = name;
            ManagedDistrict = managedDistrict;
            CreateID();
        }
        // Needs methods for doing things (maybe have to get creative later)
        private void CreateID()
        {
            string newId = null;
            var uniqueValue = true;
            while (uniqueValue)
            {
                uniqueValue = false;
                var rnd = new Random();
                newId = String.Format("{0: 0000}", rnd.Next(0, 9999));
                foreach (var id in _UniqueIds)
                {
                    if (newId == id)
                    {
                        uniqueValue = true;
                    }

                }
                
                
            }
            _UniqueIds.Add(newId);
            _DistrictManagerID = newId;
        }
        public static bool CheckId(string userId)
        {
            bool validId = false;
            foreach (var id in _UniqueIds)
            {
                if (userId == id)
                {
                    validId = true; 
                }
            }
            return validId;
        }
        public static void RequestDistrictReport()
        {
            var checkId = true;
            while (checkId)
            {
                Console.WriteLine("Please enter your District Manager ID");
                var userId = Console.ReadLine();
                if (DistrictManager.CheckId(userId))
                {
                    checkId = false;
                } else
                {
                    Console.WriteLine("Invalid ID. Please try again.");
                }
            }
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
        }
    }
}
