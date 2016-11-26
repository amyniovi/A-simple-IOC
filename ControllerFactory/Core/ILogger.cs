using System;
using System.Web.Mvc;
using IOC.Controllers;

namespace IOC
{

	public interface ILogger
	{
		void Log(string message);
	}
	
}
