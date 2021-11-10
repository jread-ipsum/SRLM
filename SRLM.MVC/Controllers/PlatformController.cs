using Microsoft.AspNet.Identity;
using SRLM.Contracts;
using SRLM.Models.PlatformModels;
using SRLM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRLM.Web.Controllers
{
    [Authorize]
    public class PlatformController : Controller
    {
        private readonly IPlatformService _svc;
        public PlatformController(IPlatformService svc)
        {
            _svc = svc;
        }

        // GET: Platform
        public ActionResult Index()
        {
            var model = _svc.GetPlatforms();

            return View(model);
        }
       
        //GET: Platform/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View();
        }
        
        //POST: Platform/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(PlatformCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.UserId = User.Identity.GetUserId();

            if (_svc.CreatePlatform(model))
            {
                TempData["SaveResult"] = "Platform was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Platform could not be created.");
            return View(model);
        }
        
        //GET: Platform/Details/{id}
        public ActionResult Details(int id)
        {
            var model = _svc.GetPlatformById(id);

            return View(model);
        }
        
        //GET: Platform/Edit/{id}
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var detail = _svc.GetPlatformById(id);

            var model =
                new PlatformEdit
                {
                    PlatformId = detail.PlatformId,
                    Name = detail.Name
                };
            return View(model);
        }
        
        //POST: Platform/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, PlatformEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if(model.PlatformId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            model.UserId = User.Identity.GetUserId();

            if (_svc.UpdatePlatform(model))
            {
                TempData["SaveResult"] = "Platform was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Platform could not be updated.");
            return View(model);
        }
        
        //GET: Platform/Delete/{id}
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var model = _svc.GetPlatformById(id);
            return View(model);
        }
        
        //POST: Platform/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeletePlatform(int id)
        {
            _svc.DeletePlatform(id, User.Identity.GetUserId());

            TempData["SaveResult"] = "Platform was deleted.";
            return RedirectToAction("Index");
        }
    }
}