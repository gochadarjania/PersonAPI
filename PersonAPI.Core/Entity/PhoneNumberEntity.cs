using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonAPI.Core.Enum;

namespace PersonAPI.Core.Entity
{
  [Table("PhoneNumbers")]
  public class PhoneNumberEntity : EntityBase
  {

    [Required]
    public NumberTypeEnum NumberType { get; set; }

    [Required]
    [MaxLength(50)]
    public string Number { get; set; } 

    [Required]
    public int PersonId { get; set; }
    public PersonEntity Person { get; set; }
  }
}
