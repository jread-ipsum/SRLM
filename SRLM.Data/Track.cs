using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Data
{
    public class Track
    {
        [Key]
        public int TrackId { get; set; }
        [Required]
        public string OwnerId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Country { get; set; }
        public virtual List<Game> Games { get; set; }
    }
}
