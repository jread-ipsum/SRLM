using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRLM.Models.LeagueModels
{
    public class LeagueEdit
    {
        [Required]
        public int LeagueId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Country { get; set; }
        public string LobbySettings { get; set; }
        [Required]
        public DateTimeOffset StartDate { get; set; }
        [Required]
        public DateTimeOffset EndDate { get; set; }
        [Required]
        public int GameId { get; set; }
        [Required]
        public int RaceClassId { get; set; }
        [Required]
        public int PlatformId { get; set; } 
        [Required]
        public int MaxDriverCount { get; set; }
        public string UserId { get; set; }

        public IEnumerable<SelectListItem> Games { get; set; }
        public IEnumerable<SelectListItem> RaceClasses { get; set; }
        public IEnumerable<SelectListItem> Platforms { get; set; }
    }
}
