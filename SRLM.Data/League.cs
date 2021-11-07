using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Data
{
    public class League
    {
        [Key]
        public int LeagueId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Country { get; set; }
        public string LobbySettings { get; set; }
        [Required]
        public DateTimeOffset StartDate { get; set; }
        [Required]
        public DateTimeOffset EndDate { get; set; }

        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        [ForeignKey(nameof(RaceClass))]
        public int RaceClassId { get; set; }
        public virtual RaceClass RaceClass { get; set; }

        [ForeignKey(nameof(Platform))]
        public int PlatformId { get; set; }
        public virtual Platform Platform { get; set; }

        public virtual List<ApplicationUser> Drivers { get; set; }
        public int MaxDriverCount { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }

    }
}
