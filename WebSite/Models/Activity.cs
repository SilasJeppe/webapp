//Class for the model Activity used by the DB and API 
using System;
using System.Runtime.Serialization;

namespace Models
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
        public TimeSpan Time { get; set; }
        [DataMember]
        public string StartAddress { get; set; }
        [DataMember]
        public string EndAddress { get; set; }
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public Route Route { get; set; }

        public Activity()
        {

        }
    }
}