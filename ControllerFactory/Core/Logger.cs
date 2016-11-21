using System;
using System.Web.Mvc;
using ControllerFactory.Controllers;

namespace ControllerFactory
{

	public class Logger : ILogger
	{
		public void Log(string message)
		{
			Console.Write(message);
		}
	}
	
}
