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
            var entity = new Game()
            {
                OwnerId = _userId,
                Title = model.Title,
                Cars = new List<Car>(),
                Tracks = new List<Track>(),
                Platforms = new List<Platform>()
            };

            using(var ctx = new ApplicationDbContext())
            {
                foreach (int carId in model.CarIds)
                {
                    var car = ctx.Cars.Find(carId);
                    if(car != null)
                    {
                        entity.Cars.Add(car);
                    }
                }
                foreach(int trackId in model.TrackIds)
                {
                    var track = ctx.Tracks.Find(trackId);
                    if(track != null)
                    {
                        entity.Tracks.Add(track);
                    }
                }
                foreach(int platformId in model.PlatformIds)
                {
                    var platform = ctx.Platforms.Find(platformId);
                    if(platform != null)
                    {
                        entity.Platforms.Add(platform);
                    }
                }
                ctx.Games.Add(entity);
                return ctx.SaveChanges() == 1;
            }

        }

        public IEnumerable<SelectListItem> GetCarsSelectList()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Cars
                    .Select(e => new SelectListItem
                    {
                        Text = e.Name,
                        Value = e.CarId.ToString()
                    });
                return query.ToArray();
            }
        }
        public IEnumerable<SelectListItem> GetTracksSelectList()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Tracks
                    .Select(e => new SelectListItem 
                    {
                        Text = e.Name,
                        Value = e.TrackId.ToString()
                    });
                return query.ToArray();
            }
        }
        public IEnumerable<SelectListItem> GetPlatformsSelectList()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Platforms
                    .Select(e => new SelectListItem 
                    {
                        Text = e.Name,
                        Value = e.PlatformId.ToString()
                    });
                return query.ToArray();
            }
        }
    }
}
