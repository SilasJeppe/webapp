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
        public int ID { get; set; }
        [DataMember]
        public Longlat Coords { get; set; }
        [DataMember]
        public int RouteID { get; set; }

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



        public Point(int ID, double pLong, double pLat, int RouteID)
        {
            this.ID = ID;
            Coords = new Longlat(pLong, pLat);
            this.RouteID = RouteID;
        }

        public Point()
        {

        }

    }
}
