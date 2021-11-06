using Microsoft.AspNet.Identity;
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
        // GET: Platform
        public ActionResult Index()
        {
            var svc = CreatePlatformService();
            var model = svc.GetPlatforms();

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

            var svc = CreatePlatformService();

            if(svc.CreatePlatform(model))
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
            var svc = CreatePlatformService();
            var model = svc.GetPlatformById(id);

            return View(model);
        }
        
        //GET: Platform/Edit/{id}
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var svc = CreatePlatformService();
            var detail = svc.GetPlatformById(id);

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

            var svc = CreatePlatformService();

            if(svc.UpdatePlatform(model))
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
            var svc = CreatePlatformService();
            var model = svc.GetPlatformById(id);

            return View(model);
        }
        
        //POST: Platform/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeletePlatform(int id)
        {
            var svc = CreatePlatformService();

            svc.DeletePlatform(id);

            TempData["SaveResult"] = "Platform was deleted.";

            return RedirectToAction("Index");
        }
        private PlatformService CreatePlatformService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = new PlatformService(userId);
            return svc;
        }
    }
}