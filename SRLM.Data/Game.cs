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
        public string ImagePath { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }

        public virtual ICollection<Platform> Platforms { get; set; }
    }
}
