using SRLM.Models.GameModels;
using System.Collections.Generic;
using System.Web.Mvc;
namespace SRLM.Contracts
{
    public interface IGameService
    {
        bool CreateGame(GameCreate model, string path);
        bool DeleteGame(int id, string userId);
        IEnumerable<SelectListItem> GetCarsSelectList();
        GameDetail GetGameById(int id);
        IEnumerable<GameListItem> GetGames();
        IEnumerable<SelectListItem> GetPlatformsSelectList();
        IEnumerable<SelectListItem> GetTracksSelectList();
        bool UpdateGame(GameEdit model);
    }
}