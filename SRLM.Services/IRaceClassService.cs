using SRLM.Models.RaceClassModels;
using System.Collections.Generic;

namespace SRLM.Services
{
    public interface IRaceClassService
    {
        bool CreateRaceClass(RaceClassCreate model);
        bool DeleteRaceClass(int raceClassId, string userId);
        RaceClassDetail GetRaceClassById(int id);
        IEnumerable<RaceClassListItem> GetRaceClasses();
        bool UpdateRaceClass(RaceClassEdit model);
    }
}