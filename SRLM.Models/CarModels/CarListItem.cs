using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM.Models.CarModels
{
    public class CarListItem
    {
        public int CarId { get; set; }
        public string Name { get; set; }

        [Display(Name="Race Class")]
        public string RaceClass { get; set; }
    }
}
