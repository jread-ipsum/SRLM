using SRLM.Data;
using SRLM.Models.CarModels;
using SRLM.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRLM.Services
{
    public class GameService
    {
        private readonly Guid _userId;

        public GameService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<GameListItem> GetGames()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Games
                    .Select(
                        e => new GameListItem
                        {
                            GameId = e.GameId,
                            Title = e.Title
                        });
                return query.ToArray();
            }
        }

        public bool CreateGame(GameCreate model)
        {
             
        }

    }
}
