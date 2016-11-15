using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace ControllerFactory.Controllers
{
	public class HomeController : Controller
	{
		private IAmCrap _crappy;
		private ILogger _logger;
		public HomeController(ILogger logger) {


			_logger = logger;
		}

		public ActionResult Index()
		{
			var mvcName = typeof(Controller).Assembly.GetName();
			var isMono = Type.GetType("Mono.Runtime") != null;

			ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
			ViewData["Runtime"] = isMono ? "Mono" : ".NET";

			IAmCrap crap = IOC.Resolve<IAmCrap>();
			return View();
		}
	}
}
