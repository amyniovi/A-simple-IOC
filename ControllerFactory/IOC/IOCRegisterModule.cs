using System;
using System.Web.Mvc;

namespace ControllerFactory
{
	public static class IOCRegisterModule
	{
		public static void LoadUpDependencies()
		{

			IOC.Bind<IAmCrap>(new SolidCrap());
			IOC.Bind<ILogger>(new Logger());
			IOC.Bind<IControllerFactory>(new MyControllerFactory());

		}
	}
}


public interface IAmCrap { }

public class SolidCrap : IAmCrap
{ }

public interface ILogger { }

public class Logger : ILogger
{ }