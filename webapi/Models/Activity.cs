using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace webapi.Models
{
    [DataContractAttribute]
    public class Activity
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public double Distance { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public DateTime Time { get; set; }
        [DataMember]
        public string StartAddress { get; set; }
        [DataMember]
        public string EndAddress { get; set; }
        [DataMember]
        public Route ActivityRoute { get; set; }

        public Activity()
        {

        }
    }
}