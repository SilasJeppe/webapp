using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace webapi.Models
{
    [DataContractAttribute]
    public class User
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Firstname { get; set; }
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public int ZipCode { get; set; }
        [DataMember]
        public int PhoneNumber { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public List<Activity> ActivityList { get; set; }

        public User()
        {

        }
    }
}