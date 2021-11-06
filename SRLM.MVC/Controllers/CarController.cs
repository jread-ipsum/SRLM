using Microsoft.AspNet.Identity;
using SRLM.Models.CarModels;
using SRLM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRLM.Web.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        // GET: Car
        public ActionResult Index()
        {
            var svc = CreateCarService();
            var model = svc.GetCars();

            return View(model);
        }

        //GET: Car/Create
        public ActionResult Create()
        {
            var svc = CreateCarService();
            var model = new CarCreate();

            model.RaceClasses = svc.RaceClassListItems();

            return View(model);
        }

        //POST: Car/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var svc = CreateCarService();

            if(svc.CreateCar(model))
            {
                TempData["SaveResult"] = "Car was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Car could not be created.");
            model.RaceClasses = svc.RaceClassListItems();
            return View(model);
        }

        //GET: Car/Details/{id}
        public ActionResult Details(int id)
        {
            var svc = CreateCarService();
            var model = svc.GetCarById(id);

            return View(model);
        }

        //GET: Car/Edit/{id}
        public ActionResult Edit(int id)
        {
            var svc = CreateCarService();
            var detail = svc.GetCarById(id);

            var model =
                new CarEdit
                {
                    CarId = detail.CarId,
                    Name = detail.Name,
                    RaceClassId = detail.RaceClassId,
                    RaceClasses = svc.RaceClassListItems()
                };

            return View(model);
        }

        //POST: Car/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CarEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if(model.CarId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var svc = CreateCarService();

            if(svc.UpdateCar(model))
            {
                TempData["SaveResult"] = "Car was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Car could not be updated.");
            model.RaceClasses = svc.RaceClassListItems();

            return View(model);
        }

        //GET: Car/Delete/{id}
        public ActionResult Delete(int id)
        {
            var svc = CreateCarService();
            var model = svc.GetCarById(id);

            return View(model);
        }

        //POST: Car/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteCar(int id)
        {
            var svc = CreateCarService();
            svc.DeleteCar(id);

            TempData["SaveResult"] = "Car was deleted.";
            return RedirectToAction("Index");
        }

        private CarService CreateCarService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = new CarService(userId);
            return svc;
        }
    }
}