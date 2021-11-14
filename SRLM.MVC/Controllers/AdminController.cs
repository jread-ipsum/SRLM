using SRLM.Contracts;
using SRLM.Models.AdminDashboardModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRLM.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    { 
        private readonly IPlatformService _platformSvc;
        private readonly IGameService _gameSvc;
        private readonly ITrackService _trackSvc;
        private readonly ICarService _carSvc;
        private readonly IRaceClassService _raceClassSvc;
        private readonly ILeagueService _leagueSvc;
        public AdminController(IPlatformService platformSvc, IGameService gameSvc, ITrackService trackSvc, ICarService carSvc, IRaceClassService raceClassSvc, ILeagueService leagueSvc)
        {
            _platformSvc = platformSvc;
            _gameSvc = gameSvc;
            _trackSvc = trackSvc;
            _carSvc = carSvc;
            _raceClassSvc = raceClassSvc;
            _leagueSvc = leagueSvc;
        }
        // GET: Admin
        public ActionResult Index()
        {
            var model = new AdminDashboard();
            model.PlatformListItems = _platformSvc.GetPlatforms();
            model.GameListItems = _gameSvc.GetGames();
            model.TrackListItems = _trackSvc.GetTracks();
            model.CarListItems = _carSvc.GetCars();
            model.RaceClassListItems = _raceClassSvc.GetRaceClasses();
            model.LeagueListItems = _leagueSvc.GetLeagues();
            return View(model);
        }
    }
}