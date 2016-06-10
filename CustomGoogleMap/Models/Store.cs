using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleMaps.LocationServices;
using Microsoft.Owin.BuilderProperties;

namespace CustomGoogleMap.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Content { get; set; }
    }
}
