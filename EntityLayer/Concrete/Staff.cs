using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Staff
    {
        [Key]
        public int staffID { get; set; }

        [StringLength(20)]
        public string staffPreName { get; set; }

        [StringLength(20)]
        public string staffLastName { get; set; }
        public int staffSalary { get; set; }

        [StringLength(50)]
        public string staffTask { get; set; }
        [StringLength(11)]
        public string stafSocSecNo { get; set; }


    }
}
