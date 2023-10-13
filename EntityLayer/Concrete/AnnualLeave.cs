using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class AnnualLeave
    {
        [Key]
        public int annualLeaveID { get; set; }

        public DateTime startDate { get; set; }

        public DateTime finishDate { get; set; }
        public int doctorID { get; set; } //foreign key
        public virtual Doctor Doctor { get; set; }

    }
}
