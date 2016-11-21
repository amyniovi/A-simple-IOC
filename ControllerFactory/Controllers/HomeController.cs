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
		private IAmCrap _iamCrap;

		public HomeController(ILogger logger, IAmCrap iamCrap) {
			_logger = logger;
			_iamCrap = iamCrap;
		}

		public ActionResult Index()
		{
			//var kernel = new Kernel();
			var mvcName = typeof(Controller).Assembly.GetName();
			var isMono = Type.GetType("Mono.Runtime") != null;
			// var crap = kernel.Resolve<IAmCrap>();
			//var crap = Activator.CreateInstance(typeof(SolidCrap), new object[] { new Logger(), new SomeRepo(new Logger())});
			//var mycrap = new SolidCrap(new Logger(), new SomeRepo(new Logger()));
			//IStupidService stupidService = new LoggingStupidService(new StupidService());
			IStupidService stupidService =
				isMono ? (IStupidService) new LoggingStupidService(new StupidService()) : new ReallyStupidService();

			ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor + _iamCrap.GetType();
			ViewData["Runtime"] = isMono ? stupidService.Stupid() : ".NET";

			return View();
		}
	}
}
