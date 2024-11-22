using PersonAPI.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAPI.Core.Models
{
    public class RelatedPersonModel
    {
        public int Id { get; set; }
        public RelationshipTypeEnum RelationshipType { get; set; }
        public int RelatedPersonId { get; set; }
        public string RelatedPersonFullName { get; set; }
    }
}
