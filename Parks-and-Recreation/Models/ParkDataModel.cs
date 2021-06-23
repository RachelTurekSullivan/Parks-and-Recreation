using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parks_and_Recreation.Models
{
    public class ParkDataModel
    {
        public string ParkID { get; set; }
        public string Parkname { get; set; }
        public string SanctuaryName { get; set; }
        public string Borough { get; set; }
        public string Acres { get; set; }
        public string Directions { get; set; }
        public string Description { get; set; }
        public string HabitatType { get; set; }

    }
}