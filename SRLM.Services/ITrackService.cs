using SRLM.Models.TrackModels;
using System.Collections.Generic;

namespace SRLM.Services
{
    public interface ITrackService
    {
        bool CreateTrack(TrackCreate model);
        bool DeleteTrack(int trackId, string userId);
        TrackDetail GetTrackById(int id);
        IEnumerable<TrackListItem> GetTracks();
        bool UpdateTrack(TrackEdit model);
    }
}