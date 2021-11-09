using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Data
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        [Required]
        public string OwnerId { get; set; }
        [Required]
        public string Title { get; set; }
        public virtual List<Car> Cars { get; set; }
        public virtual List<Track> Tracks { get; set; }

        public virtual List<Platform> Platforms { get; set; }
    }
}
