using Microsoft.AspNet.Identity;
using SRLM.Contracts;
using SRLM.Models.RaceClassModels;
using SRLM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRLM.Web.Controllers
{
    [Authorize]
    public class RaceClassController : Controller
    {
        private readonly IRaceClassService _svc;
        public RaceClassController(IRaceClassService svc)
        {
            _svc = svc;
        }

        // GET: RaceClass
        public ActionResult Index()
        {
            var model = _svc.GetRaceClasses();
            return View(model);
        }

        //GET: RaceClass/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View();
        }
        
        //POST: RaceClass/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(RaceClassCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.UserId = User.Identity.GetUserId();

            if (_svc.CreateRaceClass(model))
            {
                TempData["SaveResult"] = "Race Class was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Race Class could not be created.");
            return View(model);
        }
       
        //GET: RaceClass/Detail/{id}
        public ActionResult Details(int id)
        {
            var model = _svc.GetRaceClassById(id);
            return View(model);
        }
        
        //GET: RaceClass/Edit/{id}
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var detail = _svc.GetRaceClassById(id);
            var model =
                new RaceClassEdit
                {
                    RaceClassId = detail.RaceClassId,
                    Name = detail.Name
                };
            return View(model);
        }
        
        //POST: RaceClass/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, RaceClassEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if(model.RaceClassId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            model.UserId = User.Identity.GetUserId();

            if (_svc.UpdateRaceClass(model))
            {
                TempData["SaveResult"] = "Race Class was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Race Class could not be updated.");
            return View(model);
        }
        
        //GET: RaceClass/Delete/{id}
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var model = _svc.GetRaceClassById(id);
            return View(model);
        }
        
        //POST: RaceClass/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteRaceClass(int id)
        {
            _svc.DeleteRaceClass(id, User.Identity.GetUserId());

            TempData["SaveResult"] = "Race Class was deleted.";

            return RedirectToAction("Index");
        }
    }
}