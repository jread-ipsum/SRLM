using SRLM.Models.LeagueModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Models.DriverModels
{
    public class DriverDetail
    {
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        [Display(Name ="Game Tag")]
        public string GameTag { get; set; }
        [Display(Name ="Discord Name")]
        public string DiscordName { get; set; }
        public string Country { get; set; }
        [Display(Name ="Date Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name ="Leagues Joined")]
        public List<LeagueListItem> LeaguesEntered { get; set; }
    }
}
