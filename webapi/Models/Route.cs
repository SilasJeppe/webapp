using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace webapi.Models
{
    [DataContractAttribute]
    public class Route
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int ActivityID { get; set; }
        [DataMember]
        public List<Point> PointList { get; set; }

        public Route()
        {

        }
    }
}