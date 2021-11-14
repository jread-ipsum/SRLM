using SRLM.Models.CarModels;
using SRLM.Models.GameModels;
using SRLM.Models.LeagueModels;
using SRLM.Models.PlatformModels;
using SRLM.Models.RaceClassModels;
using SRLM.Models.TrackModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Models.AdminDashboardModels
{
    public class AdminDashboard
    {
        public IEnumerable<PlatformListItem> PlatformListItems { get; set; }
        public IEnumerable<GameListItem> GameListItems { get; set; }
        public IEnumerable<TrackListItem> TrackListItems { get; set; }
        public IEnumerable<CarListItem> CarListItems { get; set; }
        public IEnumerable<RaceClassListItem> RaceClassListItems { get; set; }
        public IEnumerable<LeagueListItem> LeagueListItems { get; set; }
    }
}
