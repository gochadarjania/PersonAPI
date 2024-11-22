using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PersonAPI.Core.Enum;
using Microsoft.EntityFrameworkCore;

namespace PersonAPI.Core.Entity
{
    [Table("Persons")]
    [Index(nameof(PersonalNumber), IsUnique = true)]
    [Index(nameof(FirstName))]
    [Index(nameof(LastName))]
    [Index(nameof(Gender))]
    [Index(nameof(CityId))]
    public class PersonEntity : EntityBase
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public GenderEnum Gender { get; set; }

        [Required]
        [MaxLength(11)]
        public string PersonalNumber { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public int CityId { get; set; }
        public string PhotoPath { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        public virtual ICollection<PhoneNumberEntity> PhoneNumbers { get; set; }

        [InverseProperty(nameof(RelatedPersonEntity.RelatedPerson))]
        public virtual ICollection<RelatedPersonEntity> RelatedPersons { get; set; }
    }
}
