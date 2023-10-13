﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Stock
    {
        [Key]
        public int stockID { get; set; }

        [StringLength(50)]
        public string stockName { get; set; }

        public int stockAmount { get; set; }
    }
}
