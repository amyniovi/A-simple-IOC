using System;
namespace ControllerFactory
{
	public class StupidService : IStupidService
	{
		public string Stupid()
		{
			return "I am stupid";
		}
	}
}
