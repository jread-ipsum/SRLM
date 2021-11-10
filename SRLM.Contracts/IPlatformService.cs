using SRLM.Models.PlatformModels;
using System.Collections.Generic;

namespace SRLM.Contracts
{
    public interface IPlatformService
    {
        bool CreatePlatform(PlatformCreate model);
        bool DeletePlatform(int id, string userId);
        PlatformDetail GetPlatformById(int id);
        IEnumerable<PlatformListItem> GetPlatforms();
        bool UpdatePlatform(PlatformEdit model);
    }
}