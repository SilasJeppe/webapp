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
        public int ID { get; set; }
        [DataMember]
        public NpgsqlPoint Coords { get; set; }
        [DataMember]
        public int RouteID { get; set; }

        public Point(int ID, NpgsqlPoint p, int RouteID)
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
