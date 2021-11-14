using SRLM.Models.DriverModels;
using System.Collections.Generic;

namespace SRLM.Contracts
{
    public interface IDriverService
    {
        DriverDetail GetDriverByDiscordName(string discordName);
        IEnumerable<DriverListItem> GetDrivers();
    }
}