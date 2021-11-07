using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Models.LeagueModels
{
    public class LeagueListItem
    {
        public int LeagueId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public string Game { get; set; }
        public string RaceClass { get; set; }
        public string Platform { get; set; }
    }
}
