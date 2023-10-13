using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Admin
    {
        [Key]
        public int adminID { get; set; }
        [StringLength(50)]
        public string adminEmail { get; set; }
        [StringLength(50)]
        public string adminPassword { get; set; }

        [StringLength(1)]
        public string userRole { get; set; }
    }
}
