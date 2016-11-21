using System;
using System.Web.Mvc;
using ControllerFactory.Controllers;

namespace ControllerFactory
{
	public static class IOCRegisterModule
	{
		public static void LoadUpDependencies(Kernel kernel)
		{

			kernel.Bind<ILogger, Logger>();
			kernel.Bind<ISomeRepo, SomeRepo>();
			kernel.Bind<IAmCrap, SolidCrap>();
			kernel.Bind<HomeController, HomeController>();
		}
	}

}
