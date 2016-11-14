using System;
namespace ControllerFactory
{
	public static class IOCRegisterModule
	{
		public static void LoadUpDependencies()
		{
			typeof(IAmCrap).BindTo(typeof(SolidCrap));
		
		}
	}
}


public interface IAmCrap { }

public class SolidCrap : IAmCrap
{ }