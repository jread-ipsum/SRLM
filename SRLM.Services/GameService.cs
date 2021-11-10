using SRLM.Contracts;
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
    public class GameService : IGameService
    {
        public IEnumerable<GameListItem> GetGames()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Games
                    .Select(g => new GameListItem
                    {
                        GameId = g.GameId,
                        Title = g.Title
                    });
                return query.ToArray();
            }
        }
        public bool CreateGame(GameCreate model)
        {
            var entity = new Game()
            {
                OwnerId = model.UserId,
                Title = model.Title,
                Cars = CarList(model.CarIds),
                Tracks = TrackList(model.TrackIds),
                Platforms = PlatformList(model.PlatformIds)
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Games.Add(entity);
                return ctx.SaveChanges() > 0;
            }
        }
        public GameDetail GetGameById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Games
                    .Single(g => g.GameId == id);
                return
                    new GameDetail
                    {
                        GameId = entity.GameId,
                        Title = entity.Title,
                        CarNames = entity.Cars.Select(c => c.Name),
                        TrackNames = entity.Tracks.Select(t => t.Name),
                        PlatformNames = entity.Platforms.Select(p => p.Name)
                    };
            }
        }
        public bool UpdateGame(GameEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Games
                    .Single(g => g.GameId == model.GameId && g.OwnerId == model.UserId);

                entity.Title = model.Title;
                entity.Cars = CarList(model.CarIds);
                entity.Tracks = TrackList(model.TrackIds);
                entity.Platforms = PlatformList(model.PlatformIds);

                return ctx.SaveChanges() > 0;
            }
        }
        public bool DeleteGame(int id, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Games
                    .Single(g => g.GameId == id && g.OwnerId == userId);
                ctx.Games.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<SelectListItem> GetCarsSelectList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Cars
                    .Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.CarId.ToString()
                    });
                return query.ToArray();
            }
        }
        public IEnumerable<SelectListItem> GetTracksSelectList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Tracks
                    .Select(t => new SelectListItem
                    {
                        Text = t.Name,
                        Value = t.TrackId.ToString()
                    });
                return query.ToArray();
            }
        }
        public IEnumerable<SelectListItem> GetPlatformsSelectList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Platforms
                    .Select(p => new SelectListItem
                    {
                        Text = p.Name,
                        Value = p.PlatformId.ToString()
                    });
                return query.ToArray();
            }
        }
        private List<Car> CarList(List<int> carIds)
        {
            var cars = new List<Car>();
            using (var ctx = new ApplicationDbContext())
            {
                foreach (int carId in carIds)
                {
                    var car = ctx.Cars.Find(carId);
                    if (car != null)
                    {
                        cars.Add(car);
                    }
                }
                return cars;
            }
        }
        private List<Track> TrackList(List<int> trackIds)
        {
            var tracks = new List<Track>();
            using (var ctx = new ApplicationDbContext())
            {
                foreach (var trackId in trackIds)
                {
                    var track = ctx.Tracks.Find(trackId);
                    if (track != null)
                    {
                        tracks.Add(track);
                    }
                }
                return tracks;
            }
        }
        private List<Platform> PlatformList(List<int> platformIds)
        {
            var platforms = new List<Platform>();
            using (var ctx = new ApplicationDbContext())
            {
                foreach (var platformId in platformIds)
                {
                    var platform = ctx.Platforms.Find(platformId);
                    if (platform != null)
                    {
                        platforms.Add(platform);
                    }
                }
                return platforms;
            }
        }
    }
}
