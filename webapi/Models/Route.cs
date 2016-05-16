//Class for the model Route used by the DB and API 
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

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