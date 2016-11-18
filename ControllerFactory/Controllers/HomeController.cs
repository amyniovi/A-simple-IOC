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
		private ILogger _logger;

		public HomeController(ILogger logger) {
			_logger = logger;
		}

		public ActionResult Index()
		{
			var mvcName = typeof(Controller).Assembly.GetName();
			var isMono = Type.GetType("Mono.Runtime") != null;
			var crap = IOC.Resolve<IAmCrap>();
			//var crap = Activator.CreateInstance(typeof(SolidCrap), new object[] { new Logger(), new SomeRepo(new Logger())});
			//var mycrap = new SolidCrap(new Logger(), new SomeRepo(new Logger()));
			//IStupidService stupidService = new LoggingStupidService(new StupidService());
			IStupidService stupidService =
				isMono ? (IStupidService) new LoggingStupidService(new StupidService()) : new ReallyStupidService();

			ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor + crap.GetType();
			ViewData["Runtime"] = isMono ? stupidService.Stupid() : ".NET";



			return View();
		}
	}
}
