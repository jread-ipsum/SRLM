using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Data
{
    public class RaceClass
    {
        [Key]
        public int RaceClassId { get; set; }
        [Required]
        public string OwnerId { get; set; }
        [Required]
        [Display(Name ="Class")]
        public string Name { get; set; }
    }
}
