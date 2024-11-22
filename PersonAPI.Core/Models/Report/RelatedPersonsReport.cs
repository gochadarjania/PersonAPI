using PersonAPI.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAPI.Core.Models.Report
{
    public class RelatedPersonsReport
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public RelationshipTypeEnum RelationshipType { get; set; }
        public int RelatedPersonCount { get; set; }
    }
}
