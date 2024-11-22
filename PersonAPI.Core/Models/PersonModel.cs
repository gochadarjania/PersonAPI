using PersonAPI.Core.Enum;
using PersonAPI.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PersonAPI.Core.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string PhotoPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<PhoneNumberModel> PhoneNumbers { get; set; }
        public virtual ICollection<RelatedPersonModel> RelatedPersons { get; set; }
    }
}
