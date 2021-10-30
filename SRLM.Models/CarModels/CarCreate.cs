using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRLM.Models.CarModels
{
    public class CarCreate
    {
        [Required]
        [MinLength(1, ErrorMessage = "Name must be at least 1 character")]
        [MaxLength(50, ErrorMessage = "Name must be less than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Must choose a race class")]
        [Display(Name="Race Class")]
        public int RaceClassId { get; set; }
        public IEnumerable<SelectListItem> RaceClasses { get; set; }
    }
}
