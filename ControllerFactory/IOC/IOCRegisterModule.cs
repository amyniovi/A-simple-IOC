using System;
using System.Web.Mvc;
using IOC.Controllers;

namespace IOC
{
	public static class IOCRegisterModule
	{
		public static void LoadUpDependencies(Kernel kernel)
		{

			kernel.Bind<ILogger, Logger>().InSingletonScope<ILogger>();
			kernel.Bind<ISomeRepo, SomeRepo>();
			kernel.Bind<IAmCrap, SolidCrap>();
			kernel.Bind<HomeController, HomeController>();
		}
	}

}
