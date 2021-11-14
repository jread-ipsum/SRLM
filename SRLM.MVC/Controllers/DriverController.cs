using SRLM.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRLM.MVC.Controllers
{
    public class DriverController : Controller
    {
        private readonly IDriverService _svc;
        public DriverController(IDriverService svc)
        {
            _svc = svc;
        }

        // GET: Driver
        public ActionResult Index()
        {
            var model = _svc.GetDrivers();
            return View(model);
        }

        //GET: Driver/Details/{DiscordName}
        public ActionResult Details(string discordName)
        {
            var model = _svc.GetDriverByDiscordName(discordName);
            return View(model);
        }
    }
}