using System;
using System.Web.Mvc;
using IOC.Controllers;

namespace IOC
{
	public static class IOCRegisterModule
	{
		public static void LoadUpDependencies(Kernel kernel)
		{
			kernel.Bind<IContext, Context>();
			kernel.Bind<ILogger, Logger>().InSingletonScope<ILogger>();
			kernel.Bind<ISomeRepo, SomeRepo>().InWebRequestScope<ISomeRepo>();
			//kernel.Bind<IAmCrap, SolidCrap>();
			kernel.Bind<HomeController, HomeController>();
		}
	}

}
