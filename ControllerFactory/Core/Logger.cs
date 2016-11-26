using System;
using System.Web.Mvc;
using IOC.Controllers;

namespace IOC
{

	public class Logger : ILogger
	{
		public void Log(string message)
		{
			Console.Write(message);
		}
	}
	
}
