using SRLM.Data;
using SRLM.Models.PlatformModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Services
{
    public class PlatformService
    {
        private readonly Guid _userId;

        public PlatformService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<PlatformListItem> GetPlatforms()
        {
            using(var ctx = new ApplicationDbContext())
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
                    OwnerId = _userId,
                    Name = model.Name
                };
            using(var ctx = new ApplicationDbContext())
            {
                ctx.Platforms.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public PlatformDetail GetPlatformById(int id)
        {
            using(var ctx = new ApplicationDbContext())
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
             using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Platforms
                    .Single(e => e.PlatformId == model.PlatformId && e.OwnerId == _userId);

                entity.Name = model.Name;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeletePlatform(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Platforms
                    .Single(e => e.PlatformId == id && e.OwnerId == _userId);

                ctx.Platforms.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
