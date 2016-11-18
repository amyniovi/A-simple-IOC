using System;
using System.Web.Mvc;

namespace ControllerFactory
{
	public static class IOCRegisterModule
	{
		public static void LoadUpDependencies()
		{
			IOC.Bind<ILogger, Logger>();
			IOC.Bind<ISomeRepo, SomeRepo>();
			IOC.Bind<IAmCrap, SolidCrap>();
			IOC.Bind<IControllerFactory, MyControllerFactory>();

		}
	}
}


public interface IAmCrap { }

public class SolidCrap : IAmCrap
{
	ILogger _logger;
	ISomeRepo _someRepo;
	public SolidCrap(ILogger logger, ISomeRepo someRepo)
	{
		_logger = logger;
		_someRepo = someRepo;
	}
}

public interface ILogger
{
	void Log(string message);
}

public class Logger : ILogger
{
	public void Log(string message)
	{
		Console.Write(message);
	}
}

public interface ISomeRepo { }
public class SomeRepo :ISomeRepo {
	ILogger _logger;
	public SomeRepo(ILogger logger)
	{
		_logger = logger;
	
	}
}