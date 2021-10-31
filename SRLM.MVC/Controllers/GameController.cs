using Microsoft.AspNet.Identity;
using SRLM.Models.GameModels;
using SRLM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRLM.Web.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            var svc = CreateGameService();
            var model = svc.GetGames();

            return View(model);
        }

        //GET: Game/Create
        public ActionResult Create()
        {
            var svc = CreateGameService();
            var model = new GameCreate();

            model.Cars = svc.GetCarsSelectList();
            model.Tracks = svc.GetTracksSelectList();
            model.Platforms = svc.GetPlatformsSelectList();

            return View(model);
        }

        //POST: Game/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GameCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var svc = CreateGameService();

            if(svc.CreateGame(model))
            {
                TempData["SaveResult"] = "Game was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Game could not be created");
            return View(model);
        }

        private GameService CreateGameService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = new GameService(userId);
            return svc;
        }
    }
}