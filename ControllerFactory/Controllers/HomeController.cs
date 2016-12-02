using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace IOC.Controllers
{	
	public class HomeController : Controller
	{
		private ILogger _logger;
		private ISomeRepo _repo;

		public HomeController(ILogger logger, ISomeRepo repo) {
			_logger = logger;
			_repo = repo;
		}

		public ActionResult Index()
		{
			
			var mvcName = typeof(Controller).Assembly.GetName();
			var isMono = Type.GetType("Mono.Runtime") != null;

			//IStupidService stupidService = new LoggingStupidService(new StupidService());
		//	IStupidService stupidService =
		//		isMono ? (IStupidService) new LoggingStupidService(new StupidService()) : new ReallyStupidService();

			ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor + _repo.GetType();
		//	ViewData["Runtime"] = isMono ? stupidService.Stupid() : ".NET";

			return View();
		}
	}
}
