
using SRLM.Contracts;
using SRLM.Data;
using SRLM.Models.CarModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRLM.Services
{
    public class CarService : ICarService
    {
        public IEnumerable<SelectListItem> RaceClassListItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var listItems =
                    ctx
                    .RaceClasses
                    .Select(e => new SelectListItem
                    {
                        Text = e.Name,
                        Value = e.RaceClassId.ToString()
                    });
                return listItems.ToArray();
            }
        }

        public IEnumerable<CarListItem> GetCars()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Cars
                    .Select(e => new CarListItem
                    {
                        CarId = e.CarId,
                        Name = e.Name,
                        RaceClass = e.RaceClass.Name
                    });
                return query.ToArray();
            }
        }
        public bool CreateCar(CarCreate model)
        {
            var entity =
                new Car()
                {
                    OwnerId = model.UserId,
                    Name = model.Name,
                    RaceClassId = model.RaceClassId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Cars.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public CarDetail GetCarById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Cars
                    .Single(e => e.CarId == id);

                return
                    new CarDetail
                    {
                        CarId = entity.CarId,
                        Name = entity.Name,
                        RaceClassName = entity.RaceClass.Name
                    };
            }
        }
        public bool UpdateCar(CarEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Cars
                    .Single(e => e.CarId == model.CarId && e.OwnerId == model.UserId);

                entity.Name = model.Name;
                entity.RaceClassId = model.RaceClassId;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteCar(int id, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Cars
                    .Single(e => e.CarId == id && e.OwnerId == userId);

                ctx.Cars.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
