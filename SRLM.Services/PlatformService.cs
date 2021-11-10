using SRLM.Contracts;
using SRLM.Data;
using SRLM.Models.PlatformModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Services
{
    public class PlatformService : IPlatformService
    {
        public IEnumerable<PlatformListItem> GetPlatforms()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Platforms
                    .Select(
                        e => new PlatformListItem
                        {
                            PlatformId = e.PlatformId,
                            Name = e.Name
                        });
                return query.ToArray();
            }
        }
        public bool CreatePlatform(PlatformCreate model)
        {
            var entity =
                new Platform()
                {
                    OwnerId = model.UserId,
                    Name = model.Name
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Platforms.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public PlatformDetail GetPlatformById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Platforms
                    .Single(e => e.PlatformId == id);
                return
                    new PlatformDetail
                    {
                        PlatformId = entity.PlatformId,
                        Name = entity.Name
                    };
            }
        }
        public bool UpdatePlatform(PlatformEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Platforms
                    .Single(e => e.PlatformId == model.PlatformId && e.OwnerId == model.UserId);

                entity.Name = model.Name;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeletePlatform(int id, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Platforms
                    .Single(e => e.PlatformId == id && e.OwnerId == userId);

                ctx.Platforms.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
