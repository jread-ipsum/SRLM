﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Data
{
    public class Platform
    {
        [Key]
        public int PlatformId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual List<Game> Games { get; set; }
    }
}
