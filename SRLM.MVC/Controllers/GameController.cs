using Microsoft.AspNet.Identity;
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
        /*public ActionResult Create()
        {
            var gameSvc = CreateGameService();
            
        }*/

        private GameService CreateGameService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = new GameService(userId);
            return svc;
        }
    }
}