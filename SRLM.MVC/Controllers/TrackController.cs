using Microsoft.AspNet.Identity;
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
        // GET: Track
        public ActionResult Index()
        {
            var svc = CreateTrackService();
            var model = svc.GetTracks();

            return View(model);
        }

        //GET: Track/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Track/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrackCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var svc = CreateTrackService();

            if (svc.CreateTrack(model))
            {
                TempData["SaveResult"] = "Track was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Track could not be created.");
            return View(model);
        }

        //GET: Track/Detail/{id}
        public ActionResult Details(int id)
        {
            var svc = CreateTrackService();
            var model = svc.GetTrackById(id);

            return View(model);
        }

        //GET: Track/Edit/{id}
        public ActionResult Edit(int id)
        {
            var svc = CreateTrackService();
            var detail = svc.GetTrackById(id);

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
        public ActionResult Edit(int id, TrackEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if(model.TrackId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var svc = CreateTrackService();

            if(svc.UpdateTrack(model))
            {
                TempData["SaveResult"] = "Track was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Track could not be updated.");
            return View(model);
        }

        //GET: Track/Delete/{id}
        public ActionResult Delete(int id)
        {
            var svc = CreateTrackService();
            var model = svc.GetTrackById(id);

            return View(model);
        }

        //POST: Track/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteTrack(int id)
        {
            var svc = CreateTrackService();

            svc.DeleteTrack(id);

            TempData["SaveResult"] = "Track was deleted.";

            return RedirectToAction("Index");
        }

        private TrackService CreateTrackService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = new TrackService(userId);
            return svc;
        }
    }
}