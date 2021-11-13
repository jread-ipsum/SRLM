using Microsoft.AspNet.Identity;
using SRLM.Contracts;
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
        private readonly IGameService _svc;
        public GameController(IGameService svc)
        {
            _svc = svc;
        }
        // GET: Game
        public ActionResult Index()
        {
            var model = _svc.GetGames();
            return View(model);
        }

        //GET: Game/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var model = new GameCreate();

            model.Cars = _svc.GetCarsSelectList();
            model.Tracks = _svc.GetTracksSelectList();
            model.Platforms = _svc.GetPlatformsSelectList();

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

            model.UserId = User.Identity.GetUserId();

            if (_svc.CreateGame(model))
            {
                TempData["SaveResult"] = "Game was created.";
                return RedirectToAction("Index", "Admin");
            }

            ModelState.AddModelError("", "Game could not be created");

            model.Cars = _svc.GetCarsSelectList();
            model.Tracks = _svc.GetTracksSelectList();
            model.Platforms = _svc.GetPlatformsSelectList();

            return View(model);
        }

        //GET: Game/Details/{id}
        public ActionResult Details(int id)
        {
            var model = _svc.GetGameById(id);
            return View(model);
        }

        //GET: Game/Edit/{id}
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var detail = _svc.GetGameById(id);
            var model = new GameEdit
            {
                GameId = detail.GameId,
                Title = detail.Title,
                Cars = _svc.GetCarsSelectList(),
                Tracks = _svc.GetTracksSelectList(),
                Platforms = _svc.GetPlatformsSelectList()
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

            if (model.GameId != id)
                ModelState.AddModelError("", "Id Mismatch");

            model.UserId = User.Identity.GetUserId();

            if (_svc.UpdateGame(model))
            {
                TempData["SaveResult"] = "Game was updated.";
                return RedirectToAction("Index", "Admin");
            }
            ModelState.AddModelError("", "Game could not be updated.");
            return View(model);
        }

        //GET: Game/Delete/{id}
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var model = _svc.GetGameById(id);
            return View(model);
        }

        //POST: Game/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteGame(int id)
        {
            _svc.DeleteGame(id, User.Identity.GetUserId());

            TempData["SaveResult"] = "Game was deleted.";
            return RedirectToAction("Index", "Admin");
        }
    }
}