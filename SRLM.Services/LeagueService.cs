using SRLM.Contracts;
using SRLM.Data;
using SRLM.Models.LeagueModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRLM.Services
{
    public class LeagueService : ILeagueService
    {
        public IEnumerable<LeagueListItem> GetLeagues()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Leagues
                    .Select(e => new LeagueListItem
                    {
                        LeagueId = e.LeagueId,
                        Name = e.Name,
                        Country = e.Country,
                        StartDate = e.StartDate,
                        Game = e.Game.Title,
                        RaceClass = e.RaceClass.Name,
                        Platform = e.Platform.Name
                    });
                return query.ToArray();
            }
        }

        public bool CreateLeague(LeagueCreate model)
        {
            var entity = new League()
            {
                OwnerId = model.UserId,
                Name = model.Name,
                Country = model.Country,
                LobbySettings = model.LobbySettings,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                GameId = model.GameId,
                RaceClassId = model.RaceClassId,
                PlatformId = model.PlatformId,
                MaxDriverCount = model.MaxDriverCount,
                CreatedUtc = DateTimeOffset.UtcNow,
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Leagues.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public LeagueDetail GetLeagueById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Leagues
                    .Single(e => e.LeagueId == id);

                var drivers = 
                    entity
                    .Drivers
                    .Select(e => e.GameTag).ToList();

                return new LeagueDetail
                {
                    LeagueId = entity.LeagueId,
                    Name = entity.Name,
                    Country = entity.Country,
                    LobbySettings = entity.LobbySettings,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,
                    Game = entity.Game.Title,
                    RaceClass = entity.RaceClass.Name,
                    Platform = entity.Platform.Name,
                    Drivers = drivers,
                    MaxDriverCount = entity.MaxDriverCount,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc
                };
            }
        }

        public bool UpdateLeague(LeagueEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Leagues
                    .Single(e => e.LeagueId == model.LeagueId && e.OwnerId == model.UserId);

                entity.Name = model.Name;
                entity.Country = model.Country;
                entity.LobbySettings = model.LobbySettings;
                entity.StartDate = model.StartDate;
                entity.EndDate = model.EndDate;
                entity.GameId = model.GameId;
                entity.RaceClassId = entity.RaceClassId;
                entity.PlatformId = entity.PlatformId;
                entity.MaxDriverCount = entity.MaxDriverCount;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteLeague(int id, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Leagues
                    .Single(e => e.LeagueId == id && e.OwnerId == userId);
                ctx.Leagues.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public bool AddDriverToLeague(int id, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Leagues
                    .Single(e => e.LeagueId == id);
                var driver =
                    ctx
                    .Users
                    .Find(userId);

                if (entity.MaxDriverCount <= entity.Drivers.Count)
                {
                    return false;
                }

                entity.Drivers.Add(driver);
                return ctx.SaveChanges() == 1;
            }
        }
        
        public bool RemoveDriverFromLeague(int id, string userId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Leagues
                    .Single(e => e.LeagueId == id);
                var driver =
                    ctx
                    .Users
                    .Find(userId);

                entity.Drivers.Remove(driver);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<SelectListItem> GetGameSelectList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Games
                    .Select(g => new SelectListItem
                    {
                        Text = g.Title,
                        Value = g.GameId.ToString()
                    });
                return query.ToArray();
            }
        }

        public IEnumerable<SelectListItem> GetRaceClassSelectList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .RaceClasses
                    .Select(r => new SelectListItem
                    {
                        Text = r.Name,
                        Value = r.RaceClassId.ToString()
                    });
                return query.ToArray();
            }
        }

        public IEnumerable<SelectListItem> GetPlatformSelectList()
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
