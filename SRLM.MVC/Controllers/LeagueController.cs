using Microsoft.AspNet.Identity;
using SRLM.Models.LeagueModels;
using SRLM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRLM.MVC.Controllers
{
    [Authorize]
    public class LeagueController : Controller
    {
        // GET: League
        public ActionResult Index()
        {
            var svc = CreateLeagueService();
            var model = svc.GetLeagues();
            return View(model);
        }
        
        //GET: League/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var svc = CreateLeagueService();
            var model = new LeagueCreate();

            model.Games = svc.GetGameSelectList();
            model.RaceClasses = svc.GetRaceClassSelectList();
            model.Platforms = svc.GetPlatformSelectList();

            return View(model);
        }

        //POST: League/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(LeagueCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var svc = CreateLeagueService();
            if (svc.CreateLeague(model))
            {
                TempData["SaveResult"] = "League was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "League could not be created.");

            model.Games = svc.GetGameSelectList();
            model.RaceClasses = svc.GetRaceClassSelectList();
            model.Platforms = svc.GetPlatformSelectList();

            return View(model);
        }

        //GET: League/Details/{id}
        public ActionResult Details(int id)
        {
            var svc = CreateLeagueService();
            var model = svc.GetLeagueById(id);
            return View(model);
        }

        //GET: League/Edit/{id}
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var svc = CreateLeagueService();
            var detail = svc.GetLeagueById(id);
            var model = new LeagueEdit
            {
                LeagueId = detail.LeagueId,
                Name = detail.Name,
                Country = detail.Country,
                LobbySettings = detail.LobbySettings,
                StartDate = detail.StartDate,
                EndDate = detail.EndDate,
                MaxDriverCount = detail.MaxDriverCount,
                Games = svc.GetGameSelectList(),
                RaceClasses = svc.GetRaceClassSelectList(),
                Platforms = svc.GetPlatformSelectList()
            };
            return View(model);
        }

        //POST: League/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, LeagueEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (model.LeagueId != id)
                ModelState.AddModelError("", "Id Mismatch");

            var svc = CreateLeagueService();
            if (svc.UpdateLeague(model))
            {
                TempData["SaveResult"] = "League was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "League could not be updated.");
            return View(model);
        }

        //GET: League/Delete/{id}
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var svc = CreateLeagueService();
            var model = svc.GetLeagueById(id);
            return View(model);
        }
        
        //POST: League/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteLeague(int id)
        {
            var svc = CreateLeagueService();
            svc.DeleteLeague(id);

            TempData["SaveResult"] = "League ws deleted.";
            return RedirectToAction("Index");
        }
        
        private LeagueService CreateLeagueService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = new LeagueService(userId);
            return svc;
        }
    }
}