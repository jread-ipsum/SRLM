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
        public string Title { get; set; }

        [Display(Name ="Cars")]
        public List<int> CarIds { get; set; }

        [Display(Name ="Tracks")]
        public List<int> TrackIds { get; set; }

        [Display(Name ="Platforms")]
        public List<int> PlatformIds { get; set; }
    }
}
