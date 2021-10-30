using SRLM.Data;
using SRLM.Models.RaceClassModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Services
{
    public class RaceClassService
    {
        private readonly Guid _userId;
        public RaceClassService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<RaceClassListItem> GetRaceClasses()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .RaceClasses
                    .Select(
                        e => new RaceClassListItem
                         {
                             RaceClassId = e.RaceClassId,
                             Name = e.Name
                         });
                return query.ToArray();
            }
        }

        public bool CreateRaceClass(RaceClassCreate model)
        {
            var entity =
                new RaceClass()
                {
                    OwnerId = _userId,
                    Name = model.Name
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.RaceClasses.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public RaceClassDetail GetRaceClassById(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .RaceClasses
                    .Single(e => e.RaceClassId == id);
                return
                    new RaceClassDetail
                    {
                        RaceClassId = entity.RaceClassId,
                        Name = entity.Name
                    };
            }
        }

        public bool UpdateRaceClass(RaceClassEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .RaceClasses
                    .Single(e => e.RaceClassId == model.RaceClassId && e.OwnerId == _userId);

                entity.Name = model.Name;

                return ctx.SaveChanges() == 1;
            }
        }


        public bool DeleteRaceClass(int raceClassId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .RaceClasses
                    .Single(e => e.RaceClassId == raceClassId && e.OwnerId == _userId);

                ctx.RaceClasses.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
