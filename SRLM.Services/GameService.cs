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
        public bool CreateGame(GameCreate model, string path)
        {
            var entity = new Game();
       
            entity.OwnerId = model.UserId;
            entity.Title = model.Title;
            entity.Cars = new List<Car>();
            entity.Tracks = new List<Track>();
            entity.Platforms = new List<Platform>();
            entity.ImagePath = path;

            using (var ctx = new ApplicationDbContext())
            {
                foreach (int carId in model.CarIds)
                {
                    var car = ctx.Cars.Find(carId);
                    if (car != null)
                    {
                        entity.Cars.Add(car);
                    }
                }
                foreach (var trackId in model.TrackIds)
                {
                    var track = ctx.Tracks.Find(trackId);
                    if (track != null)
                    {
                        entity.Tracks.Add(track);
                    }
                }
                foreach (var platformId in model.PlatformIds)
                {
                    var platform = ctx.Platforms.Find(platformId);
                    if (platform != null)
                    {
                        entity.Platforms.Add(platform);
                    }
                }

                ctx.Games.Add(entity);
                return ctx.SaveChanges() >0;
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

                entity.Cars = new List<Car>();
                foreach (int carId in model.CarIds)
                {
                    var car = ctx.Cars.Find(carId);
                    if (car != null)
                    {
                        entity.Cars.Add(car);
                    }
                }

                entity.Tracks = new List<Track>();
                foreach (var trackId in model.TrackIds)
                {
                    var track = ctx.Tracks.Find(trackId);
                    if (track != null)
                    {
                        entity.Tracks.Add(track);
                    }
                }

                entity.Platforms = new List<Platform>();
                foreach (var platformId in model.PlatformIds)
                {
                    var platform = ctx.Platforms.Find(platformId);
                    if (platform != null)
                    {
                        entity.Platforms.Add(platform);
                    }
                }

                return ctx.SaveChanges() == 1;
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
    }
}
