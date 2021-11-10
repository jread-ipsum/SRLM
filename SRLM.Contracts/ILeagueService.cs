using SRLM.Models.LeagueModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SRLM.Contracts
{
    public interface ILeagueService
    {
        bool CreateLeague(LeagueCreate model);
        bool DeleteLeague(int id, string userId);
        IEnumerable<SelectListItem> GetGameSelectList();
        LeagueDetail GetLeagueById(int id);
        IEnumerable<LeagueListItem> GetLeagues();
        IEnumerable<SelectListItem> GetPlatformSelectList();
        IEnumerable<SelectListItem> GetRaceClassSelectList();
        bool UpdateLeague(LeagueEdit model);
        bool AddDriverToLeague(int id, string userId);
        bool RemoveDriverFromLeague(int id, string userId);
    }
}