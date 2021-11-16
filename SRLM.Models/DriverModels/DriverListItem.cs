using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Models.DriverModels
{
    public class DriverListItem
    {
        public string UserId { get; set; }
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        [Display(Name ="Game Tag")]
        public string GameTag { get; set; }
        [Display(Name ="Discord Name")]
        public string DiscordName { get; set; }
        public string Country { get; set; }
    }
}
