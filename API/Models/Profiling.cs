﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Profiling
    {
        [Key]
        [ForeignKey("NIK")]
        [Required]
        public string NIK { get; set; }
        public Account Account { get; set; }
        public Education Education { get; set; }
    }
}
