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
        public Tuple<double, double> longlat { get; set; }
       

        public Point(int pointID, string name, double pLong, double pLat)
        {
            this.pointID = pointID;
            this.name = name;
            this.longlat = new Tuple<double, double>(pLong, pLat);
        }

        public Point()
        {

        }

    }
}
      