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

        public List<int> CarIds { get; set; }
        public List<int> TrackIds { get; set; }
        public List<int> PlatformIds { get; set; }
        public IEnumerable<SelectListItem> Cars { get; set; }
        public IEnumerable<SelectListItem> Tracks { get; set; }
        public IEnumerable<SelectListItem> Platforms { get; set; }

    }
}
