using Microsoft.AspNet.Identity;
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
        // GET: RaceClass
        public ActionResult Index()
        {
            var svc = CreateRaceClassService();
            var model = svc.GetRaceClasses();

            return View(model);
        }

        //GET: RaceClass/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: RaceClass/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RaceClassCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var svc = CreateRaceClassService();

            if(svc.CreateRaceClass(model))
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
            var svc = CreateRaceClassService();
            var model = svc.GetRaceClassById(id);

            return View(model);
        }

        //GET: RaceClass/Edit/{id}
        public ActionResult Edit(int id)
        {
            var svc = CreateRaceClassService();
            var detail = svc.GetRaceClassById(id);

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
        public ActionResult Edit(int id, RaceClassEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if(model.RaceClassId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var svc = CreateRaceClassService();

            if (svc.UpdateRaceClass(model))
            {
                TempData["SaveResult"] = "Race Class was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Race Class could not be updated.");
            return View(model);
        }

        //GET: RaceClass/Delete/{id}
        public ActionResult Delete(int id)
        {
            var svc = CreateRaceClassService();
            var model = svc.GetRaceClassById(id);

            return View(model);
        }

        //POST: RaceClass/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteRaceClass(int id)
        {
            var svc = CreateRaceClassService();

            svc.DeleteRaceClass(id);

            TempData["SaveResult"] = "Race Class was deleted.";

            return RedirectToAction("Index");
        }

        private RaceClassService CreateRaceClassService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = new RaceClassService(userId);
            return svc;
        }
    }
}