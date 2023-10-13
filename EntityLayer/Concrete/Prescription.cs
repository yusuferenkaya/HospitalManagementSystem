using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Prescription
    {
        [Key]
        public int prescriptionID { get; set; }

        [StringLength(50)]
        public string medicineName { get; set; }

        [StringLength(200)]
        public string useIntr { get; set; }


        public int duration { get; set; }

        public int appointmentID { get; set; }//foreign key
        public virtual Appointment Appointment { get; set; }


    }
}
