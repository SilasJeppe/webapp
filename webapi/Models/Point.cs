using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using NpgsqlTypes;

namespace webapi.Models
{
    [DataContractAttribute]
    public class Point
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public NpgsqlPoint Coords { get; set; }
        [DataMember]
        public int RouteID { get; set; }

        //This field(Point) contains the information from the map marker.
        public Point(long ID, NpgsqlPoint p, int RouteID)
        {
            this.ID = ID;
            this.Coords = p;
            this.RouteID = RouteID;
        }

        public Point()
        {

        }

    }
}
