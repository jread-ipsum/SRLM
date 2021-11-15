using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Models.LeagueModels
{
    public class LeagueDetail
    {
        public int LeagueId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string LobbySettings { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int GameId { get; set; }
        public string Game { get; set; }
        public int RaceClassId { get; set; }
        public string RaceClass { get; set; }
        public int PlatformId { get; set; }
        public string Platform { get; set; }
        public List<int> DriverIds { get; set; }
        public List<string> DriverNames { get; set; }
        public int MaxDriverCount { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }

    }
}
