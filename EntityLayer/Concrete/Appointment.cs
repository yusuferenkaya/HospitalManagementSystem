using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Appointment
    {


        [Key]
        public int appointmentID { get; set; }

        public DateTime appDate { get; set; }

        public int doctorID { get; set; } //foreign key
        public virtual Doctor Doctor { get; set; }

        public int AppointmentHourID { get; set; } //foreign key
        public virtual AppointmentHour AppointmentHour { get; set; }

        public int patientID { get; set; } //foreign key
        public virtual Patient Patient { get; set; }
    }
}
