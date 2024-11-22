using PersonAPI.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAPI.Core.Models
{
    public class PhoneNumberModel
    {
        public int Id { get; set; }
        public NumberTypeEnum NumberType { get; set; }
        public string Number { get; set; }
    }
}
