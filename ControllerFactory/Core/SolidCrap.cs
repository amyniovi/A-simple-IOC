using System;
using System.Web.Mvc;
using ControllerFactory.Controllers;

namespace ControllerFactory
{

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
	
}
