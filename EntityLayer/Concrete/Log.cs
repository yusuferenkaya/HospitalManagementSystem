using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Log
    {
        [Key]
        public int LogID { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
