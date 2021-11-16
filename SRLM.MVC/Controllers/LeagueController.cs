using Microsoft.AspNet.Identity;
using SRLM.Contracts;
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
        private readonly ILeagueService _svc;
        public LeagueController(ILeagueService svc)
        {
            _svc = svc;
        }
        // GET: League
        public ActionResult Index()
        {
            var model = _svc.GetLeagues();
            return View(model);
        }
        
        //GET: League/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var model = new LeagueCreate();

            model.Games = _svc.GetGameSelectList();
            model.RaceClasses = _svc.GetRaceClassSelectList();
            model.Platforms = _svc.GetPlatformSelectList();

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

            model.UserId = User.Identity.GetUserId();

            if (_svc.CreateLeague(model))
            {
                TempData["SaveResult"] = "League was created.";
                return RedirectToAction("Index", "Admin");
            }
            ModelState.AddModelError("", "League could not be created.");

            model.Games = _svc.GetGameSelectList();
            model.RaceClasses = _svc.GetRaceClassSelectList();
            model.Platforms = _svc.GetPlatformSelectList();

            return View(model);
        }

        //GET: League/Details/{id}
        public ActionResult Details(int id)
        {
            var model = _svc.GetLeagueById(id);
            return View(model);
        }

        //GET: League/Edit/{id}
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var detail = _svc.GetLeagueById(id);
            var model = new LeagueEdit
            {
                LeagueId = detail.LeagueId,
                Name = detail.Name,
                Country = detail.Country,
                LobbySettings = detail.LobbySettings,
                StartDate = detail.StartDate,
                EndDate = detail.EndDate,
                MaxDriverCount = detail.MaxDriverCount,
                Games = _svc.GetGameSelectList(),
                RaceClasses = _svc.GetRaceClassSelectList(),
                Platforms = _svc.GetPlatformSelectList()
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

            model.UserId = User.Identity.GetUserId();

            if (_svc.UpdateLeague(model))
            {
                TempData["SaveResult"] = "League was updated.";
                return RedirectToAction("Index", "Admin");
            }
            ModelState.AddModelError("", "League could not be updated.");
            return View(model);
        }

        //GET: League/Delete/{id}
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var model = _svc.GetLeagueById(id);
            return View(model);
        }
        
        //POST: League/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteLeague(int id)
        {
            _svc.DeleteLeague(id, User.Identity.GetUserId());

            TempData["SaveResult"] = "League ws deleted.";
            return RedirectToAction("Index", "Admin");
        }

        //GET: League/AddDriver/{id}
        public ActionResult AddDriver(int id)
        {
            var model = _svc.GetLeagueById(id);
            return View(model);
        }

        //POST: League/AddDriver/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("AddDriver")]
        public ActionResult AddDriverToLeague(int id)
        {
            var result = _svc.AddDriverToLeague(id, User.Identity.GetUserId());
            if(result is false)
            {
                TempData["FailedSaveResult"] = "You have either already joined this league or there are currently no available spots.";
                return View();
                //return RedirectToAction("Details", id);
            }

            TempData["SaveResult"] = "Successfully added to the League.";
            //return View();
            return RedirectToAction("Index");
        }

        //GET: League/RemoveDriver/{id}
        public ActionResult RemoveDriver(int id)
        {
            var model = _svc.GetLeagueById(id);
            return View(model);
        }

        //POST: League/RemoveDriver/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("RemoveDriver")]
        public ActionResult RemoveDriverFromLeague(int id)
        {
            _svc.RemoveDriverFromLeague(id, User.Identity.GetUserId());

            TempData["SaveResult"] = "Successfully removed from the League.";
            return RedirectToAction("Details", id);
        }
    }
}