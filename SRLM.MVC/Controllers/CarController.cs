using Microsoft.AspNet.Identity;
using SRLM.Contracts;
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
        private readonly ICarService _svc;
        public CarController(ICarService svc)
        {
            _svc = svc;
        }
        // GET: Car
        public ActionResult Index()
        {
            var model = _svc.GetCars();
            return View(model);
        }

        //GET: Car/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var model = new CarCreate();
            model.RaceClasses = _svc.RaceClassListItems();
            return View(model);
        }

        //POST: Car/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(CarCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.UserId = User.Identity.GetUserId();

            if(_svc.CreateCar(model))
            {
                TempData["SaveResult"] = "Car was created.";
                return RedirectToAction("Index", "Admin");
            }

            ModelState.AddModelError("", "Car could not be created.");
            model.RaceClasses = _svc.RaceClassListItems();
            return View(model);
        }

        //GET: Car/Details/{id}
        public ActionResult Details(int id)
        {
            var model = _svc.GetCarById(id);
            return View(model);
        }

        //GET: Car/Edit/{id}
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var detail = _svc.GetCarById(id);

            var model =
                new CarEdit
                {
                    CarId = detail.CarId,
                    Name = detail.Name,
                    RaceClassId = detail.RaceClassId,
                    RaceClasses = _svc.RaceClassListItems()
                };
            return View(model);
        }

        //POST: Car/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, CarEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if(model.CarId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            model.UserId = User.Identity.GetUserId();

            if(_svc.UpdateCar(model))
            {
                TempData["SaveResult"] = "Car was updated.";
                return RedirectToAction("Index", "Admin");
            }

            ModelState.AddModelError("", "Car could not be updated.");
            model.RaceClasses = _svc.RaceClassListItems();

            return View(model);
        }

        //GET: Car/Delete/{id}
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var model = _svc.GetCarById(id);
            return View(model);
        }

        //POST: Car/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCar(int id)
        {
            _svc.DeleteCar(id, User.Identity.GetUserId());

            TempData["SaveResult"] = "Car was deleted.";
            return RedirectToAction("Index", "Admin");
        }
    }
}