using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Data
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        [Required]
        public string OwnerId { get; set; }
        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(RaceClass))]
        [Display(Name= "Race Class")]
        public int RaceClassId { get; set; }
        public virtual RaceClass RaceClass { get; set; }

        public virtual List<Game> Games { get; set; }
    }
}
