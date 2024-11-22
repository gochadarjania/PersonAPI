using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAPI.Core.Entity
{
    [Table("Logs")]
    public class LogEntity : EntityBase
    {
        public DateTime CreatedOn { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Exception { get; set; }
        public string Logger { get; set; }
        public string Url { get; set; }
    }
}
