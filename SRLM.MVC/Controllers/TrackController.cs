using Microsoft.AspNet.Identity;
using SRLM.Contracts;
using SRLM.Models.TrackModels;
using SRLM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRLM.Web.Controllers
{
    [Authorize]
    public class TrackController : Controller
    {
        private readonly ITrackService _svc;
        public TrackController(ITrackService svc)
        {
            _svc = svc;
        }
        
        // GET: Track
        public ActionResult Index()
        {
            var model = _svc.GetTracks();
            return View(model);
        }
        
        //GET: Track/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
       
        //POST: Track/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(TrackCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.UserId = User.Identity.GetUserId();
            if (_svc.CreateTrack(model))
            {
                TempData["SaveResult"] = "Track was created.";
                return RedirectToAction("Index", "Admin");
            }

            ModelState.AddModelError("", "Track could not be created.");
            return View(model);
        }
        
        //GET: Track/Detail/{id}
        public ActionResult Details(int id)
        {
            var model = _svc.GetTrackById(id);

            return View(model);
        }
        
        //GET: Track/Edit/{id}
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var detail = _svc.GetTrackById(id);

            var model =
                new TrackEdit
                {
                    TrackId = detail.TrackId,
                    Name = detail.Name,
                    Country = detail.Country
                };

            return View(model);
        }
       
        //POST: Track/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, TrackEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if(model.TrackId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            model.UserId = User.Identity.GetUserId();
            if (_svc.UpdateTrack(model))
            {
                TempData["SaveResult"] = "Track was updated.";
                return RedirectToAction("Index", "Admin");
            }

            ModelState.AddModelError("", "Track could not be updated.");
            return View(model);
        }
        
        //GET: Track/Delete/{id}
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var model = _svc.GetTrackById(id);

            return View(model);
        }
       
        //POST: Track/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteTrack(int id)
        {
            _svc.DeleteTrack(id, User.Identity.GetUserId());

            TempData["SaveResult"] = "Track was deleted.";

            return RedirectToAction("Index", "Admin");
        }
    }
}