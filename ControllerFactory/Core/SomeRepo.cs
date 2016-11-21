using System;
using System.Web.Mvc;
using ControllerFactory.Controllers;

namespace ControllerFactory
{
	
	public class SomeRepo : ISomeRepo
	{
		ILogger _logger;
		public SomeRepo(ILogger logger)
		{
			_logger = logger;

		}
	}
}
