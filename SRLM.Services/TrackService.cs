using SRLM.Contracts;
using SRLM.Data;
using SRLM.Models.TrackModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Services
{
    public class TrackService : ITrackService
    {
        public IEnumerable<TrackListItem> GetTracks()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Tracks
                    .Select(
                        e => new TrackListItem
                        {
                            TrackId = e.TrackId,
                            Name = e.Name,
                            Country = e.Country
                        });
                return query.ToArray();
            }
        }
        public bool CreateTrack(TrackCreate model)
        {
            var entity =
                new Track()
                {
                    OwnerId = model.UserId,
                    Name = model.Name,
                    Country = model.Country
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Tracks.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public TrackDetail GetTrackById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Tracks
                    .Single(e => e.TrackId == id);
                return
                    new TrackDetail
                    {
                        TrackId = entity.TrackId,
                        Name = entity.Name,
                        Country = entity.Country
                    };
            }
        }
        public bool UpdateTrack(TrackEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Tracks
                    .Single(e => e.TrackId == model.TrackId && e.OwnerId == model.UserId);

                entity.Name = model.Name;
                entity.Country = model.Country;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteTrack(int trackId, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Tracks
                    .Single(e => e.TrackId == trackId && e.OwnerId == userId);

                ctx.Tracks.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
