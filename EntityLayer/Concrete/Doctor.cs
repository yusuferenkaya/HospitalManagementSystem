using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Doctor
    {
        [Key]
        public int doctorID { get; set; }
        [StringLength(20)]
        public string doctorPreName{ get; set; }
        [StringLength(20)]
        public string doctorLastName { get; set; }
        public DateTime birthDate { get; set; }
        public int salary { get; set; }
        [StringLength(11)]
        public string socSecNo { get; set; }
        public string doctorPhoneNo { get; set; }

        [StringLength(50)]
        public string doctorEmail { get; set; }
        public int departmentID { get; set; }//foreign key
        public virtual Department Department { get; set; }




    }
}
