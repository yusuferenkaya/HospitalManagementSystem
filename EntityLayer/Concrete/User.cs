using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class User
    {
        [Key]
        public int userID { get; set; }


        [StringLength(20)]
        public string userPreName { get; set; }


        [StringLength(20)]
        public string userLastName { get; set; }


        [StringLength(50)]
        public string userEmail { get; set; }


        [StringLength(20)]
        public string userPassword { get; set; }

    }
}
