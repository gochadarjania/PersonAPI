using Microsoft.EntityFrameworkCore;
using PersonAPI.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAPI.Core.Entity
{
    [Table("RelatedPersons")]
    [Index(nameof(RelationshipType))]
    public class RelatedPersonEntity : EntityBase
    {
        [Required]
        public RelationshipTypeEnum RelationshipType { get; set; }

        [Required]
        public int PersonId { get; set; }
        [ForeignKey(nameof(PersonId))]
        public virtual PersonEntity Person { get; set; }

        [Required]
        public int RelatedPersonId { get; set; }
        [ForeignKey(nameof(RelatedPersonId))]
        public virtual PersonEntity RelatedPerson { get; set; }
    }
}
