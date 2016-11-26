using System;
namespace IOC
{
	public class StupidService : IStupidService
	{
		public string Stupid()
		{
			return "I am stupid";
		}
	}
}
