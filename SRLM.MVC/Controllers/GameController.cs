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
        [Authorize(Roles ="Admin")]
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
        [Authorize(Roles = "Admin")]
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

            model.Cars = svc.GetCarsSelectList();
            model.Tracks = svc.GetTracksSelectList();
            model.Platforms = svc.GetPlatformsSelectList();

            return View(model);
        }

        //GET: Game/Details/{id}
        public ActionResult Details(int id)
        {
            var svc = CreateGameService();
            var model = svc.GetGameById(id);
            return View(model);
        }

        //GET: Game/Edit/{id}
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var svc = CreateGameService();
            var detail = svc.GetGameById(id);
            var model =
                new GameEdit
                {
                    GameId = detail.GameId,
                    Title = detail.Title,
                    Cars = svc.GetCarsSelectList(),
                    Tracks = svc.GetTracksSelectList(),
                    Platforms = svc.GetPlatformsSelectList()
                };
            return View(model);
        }

        //POST: Game/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, GameEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if(model.GameId != id)
                ModelState.AddModelError("", "Id Mismatch");

            var svc = CreateGameService();

            if (svc.UpdateGame(model))
            {
                TempData["SaveResult"] = "Game was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Game could not be updated.");
            return View(model);
        }

        //GET: Game/Delete/{id}
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var svc = CreateGameService();
            var model = svc.GetGameById(id);
            return View(model);
        }

        //POST: Game/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteGame(int id)
        {
            var svc = CreateGameService();
            svc.DeleteGame(id);

            TempData["SaveResult"] = "Game was deleted.";
            return RedirectToAction("Index");
        }

        private GameService CreateGameService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = new GameService(userId);
            return svc;
        }
    }
}