using SRLM.Models.CarModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SRLM.Services
{
    public interface ICarService
    {
        bool CreateCar(CarCreate model);
        bool DeleteCar(int id, string userId);
        CarDetail GetCarById(int id);
        IEnumerable<CarListItem> GetCars();
        IEnumerable<SelectListItem> RaceClassListItems();
        bool UpdateCar(CarEdit model);
    }
}