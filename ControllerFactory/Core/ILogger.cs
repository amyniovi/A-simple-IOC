using System;
using System.Web.Mvc;
using ControllerFactory.Controllers;

namespace ControllerFactory
{

	public interface ILogger
	{
		void Log(string message);
	}
	
}
