using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRLM.Models.GameModels
{
    public class GameCreate
    {
        [Required(ErrorMessage ="Game must have a title.")]
        public string Title { get; set; }
        public string UserId { get; set; }

        [Required (ErrorMessage ="Game must have cars.")]
        public List<int> CarIds { get; set; }

        [Required(ErrorMessage = "Game must have tracks.")]
        public List<int> TrackIds { get; set; }

        [Required(ErrorMessage = "Game must have a platform.")]
        public List<int> PlatformIds { get; set; }
        public IEnumerable<SelectListItem> Cars { get; set; }
        public IEnumerable<SelectListItem> Tracks { get; set; }
        public IEnumerable<SelectListItem> Platforms { get; set; }

    }
}
