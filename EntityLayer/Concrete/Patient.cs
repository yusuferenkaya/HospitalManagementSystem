using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Patient
    {
        [Key]
        public int patientID { get; set; }

        [StringLength(20)]
        public string patientPreName { get; set; }

        [StringLength(20)]
        public string patientLastName { get; set; }


        public DateTime patientBirthDate { get; set; }

        [StringLength(11)]
        public string patientSocSecNo { get; set; }

        [StringLength(50)]
        public string patientEmail { get; set; }
        public int userID { get; set; }//foreign key
        public virtual User User { get; set; }



    }
}
