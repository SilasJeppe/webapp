using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Fornavn")]
        public string Firstname { get; set; }
        [DataMember]
        [Display(Name = "Efternavn")]
        public string Surname { get; set; }
        [DataMember]
        [Display(Name = "Adresse")]
        public string Address { get; set; }
        [DataMember]
        [Display(Name = "By")]
        public string City { get; set; }
        [DataMember]
        [Display(Name = "Postnummer")]
        public int ZipCode { get; set; }
        [DataMember]
        [Display(Name = "Telefon")]
        public int PhoneNumber { get; set; }
        [DataMember]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [DataMember]
        [Display(Name = "Password")]
        public string password { get; set; }
        [DataMember]
        public List<Activity> ActivityList { get; set; }

        public User()
        {

        }
    }
}