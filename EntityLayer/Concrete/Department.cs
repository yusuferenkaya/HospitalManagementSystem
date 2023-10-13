using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Department
    {
        [Key]
        public int departmentID { get; set; }
        [StringLength(100)]
        public string departmentName { get; set; }
        [StringLength(100)]
        public string locationAdress { get; set; }


    }
}
