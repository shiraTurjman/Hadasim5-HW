﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Entities
{
    public class StatusEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
