using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAPI.Core.Entity
{
  [Table("Cities")]
  public class CityEntity : EntityBase
  {
    [Required]
    [MaxLength(100)]
    public string NameKa { get; set; }
    [Required]
    [MaxLength(100)]
    public string NameEn { get; set; }
  }
}
