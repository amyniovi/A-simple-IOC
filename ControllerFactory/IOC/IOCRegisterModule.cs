﻿using System;
using System.Web.Mvc;

namespace ControllerFactory
{
	public static class IOCRegisterModule
	{
		public static void LoadUpDependencies()
		{
			IOC.Bind2<IAmCrap, SolidCrap>();
			IOC.Bind2<ILogger, Logger>();
			IOC.Bind2<IControllerFactory, MyControllerFactory>();

		}
	}
}


public interface IAmCrap { }

public class SolidCrap : IAmCrap
{ }

public interface ILogger { }

public class Logger : ILogger
{ }