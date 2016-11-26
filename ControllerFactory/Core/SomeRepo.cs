using System;
using System.Web.Mvc;
using IOC.Controllers;

namespace IOC
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
