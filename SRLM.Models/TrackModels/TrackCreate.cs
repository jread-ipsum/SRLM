using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Models.TrackModels
{
    public class TrackCreate
    {
        [Required]
        [MinLength(1, ErrorMessage ="Must be at least 1 character.")]
        [MaxLength(50, ErrorMessage ="Must be less than 50 characters.")]
        [Display(Name = "Track Name")]
        public string Name { get; set; }
        public string Country { get; set; }
        public string UserId { get; set; }
    }
}
