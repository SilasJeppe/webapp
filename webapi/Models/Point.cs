using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace webapi.Models
{
    [DataContractAttribute]
    public class Point
    {
        [DataMember]
        public int pointID { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public Longlat Coords { get; set; }

        public struct Longlat
        {
            public double pLong { get; set; }
            public double pLat { get; set; }
            public Longlat(double x, double y) : this()
            {
                pLong = x;
                pLat = y;
            }
        }



        public Point(int pointID, string name, double pLong, double pLat)
        {
            this.pointID = pointID;
            this.name = name;
            Coords = new Longlat(pLong, pLat);
        }

        public Point()
        {

        }

    }
}
