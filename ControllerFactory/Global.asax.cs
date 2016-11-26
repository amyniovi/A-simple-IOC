using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IOC
{
	public class Global : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			var kernel = new Kernel();

			IOCRegisterModule.LoadUpDependencies(kernel);
			ControllerBuilder.Current.SetControllerFactory(new MyControllerFactory(kernel));
		}
	}
}

