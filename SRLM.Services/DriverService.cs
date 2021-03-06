using SRLM.Contracts;
using SRLM.Data;
using SRLM.Models.DriverModels;
using SRLM.Models.LeagueModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Services
{
    public class DriverService : IDriverService
    {
        public IEnumerable<DriverListItem> GetDrivers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Users
                    .Select(e => new DriverListItem
                    {
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        GameTag = e.GameTag,
                        DiscordName = e.DiscordName,
                        Country = e.Country
                    });
                return query.ToArray();
            }
        }
        public DriverDetail GetDriverByDiscordName(string discordName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Users
                    .Single(e => e.DiscordName == discordName);

                var leagues =
                    entity
                    .Leagues
                    .Select(e => new LeagueListItem
                    {
                        LeagueId = e.LeagueId,
                        Name = e.Name,
                        Country = e.Country,
                        StartDate = e.StartDate,
                        Game = e.Game.Title,
                        GameImagePath = e.Game.ImagePath,
                        RaceClass = e.RaceClass.Name,
                        Platform = e.Platform.Name,
                        MaxDriverCount = e.MaxDriverCount,
                        DriverCount = e.Drivers.Count
                    });

                return new DriverDetail
                {
                    DiscordName = entity.DiscordName,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    GameTag = entity.GameTag,
                    Country = entity.Country,
                    CreatedUtc = entity.CreatedUtc,
                    LeaguesEntered = leagues.ToList()
                };
            }
        }

    }
}
