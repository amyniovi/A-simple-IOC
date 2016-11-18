using System;
namespace ControllerFactory
{
	public class ReallyStupidService : IStupidService
	{
		public ReallyStupidService()
		{
		}

		public string Stupid()
		{
			return "I am REALLY stupid";
		}
	}
}
