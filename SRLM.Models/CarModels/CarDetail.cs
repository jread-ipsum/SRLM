using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRLM.Models.CarModels
{
    public class CarDetail
    {
        public int CarId { get; set; }
        public string Name { get; set; }
        public int RaceClassId { get; set; }

        [Display(Name = "Race Class")]
        public string RaceClassName { get; set; }
    }
}
