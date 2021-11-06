using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Models.GameModels
{
    public class GameDetail
    {
        public int GameId { get; set; }
        public string Title { get; set; }
        public IEnumerable<string> CarNames { get; set; }
        public IEnumerable<string> TrackNames { get; set; }
        public IEnumerable<string> PlatformNames { get; set; }
    }
}
