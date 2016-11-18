using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ControllerFactory.Controllers
{
    public class LoggingController : Controller
    {
		private Controller controller = null;
		
		public LoggingController(Controller controller)
		{
		}

		public void Execute(RequestContext requestContext)
		{
			controller.Execute(requestContext);
		}

		public ActionResult Index()
        {
            return View ();
        }
    }
}
