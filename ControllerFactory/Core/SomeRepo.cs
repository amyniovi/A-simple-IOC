using System;
using System.Collections.Generic;
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

		public List<string> GetAll()
		{
			return new List<string>() { "macbook", "macbook Air", "macbook Pro" };
		}
	}
}
