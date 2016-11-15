using System;
namespace ControllerFactory
{
	public static class IOCRegisterModule
	{
		public static void LoadUpDependencies()
		{
			
			IOC.Bind<IAmCrap>(new SolidCrap());
				
		
		}
	}
}


public interface IAmCrap { } 

public class SolidCrap : IAmCrap
{ }