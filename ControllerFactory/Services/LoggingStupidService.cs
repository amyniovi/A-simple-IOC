using System;
namespace ControllerFactory
{
	public class LoggingStupidService : IStupidService
	{
		private IStupidService _stupidService;
		
		public LoggingStupidService(IStupidService stupidService)
		{
			_stupidService = stupidService;
		}

		public string Stupid()
		{
			Console.WriteLine("Begin Stupid");

			return _stupidService.Stupid();
		}
	}
}
