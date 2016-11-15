using System;
using System.Collections.Generic;

namespace ControllerFactory
{
	public static class IOC
	{
		internal static Dictionary<Type, object> Dependencies = new Dictionary<Type, object>();

		public static void Bind<T>(T solidType) where T:class 
		{
			//enter write lock
			try {
				
				object registration = null;

				if (solidType == null)
					throw new ArgumentNullException();

				if ( Dependencies!=null && Dependencies.TryGetValue(typeof(T), out registration))
					throw new Exception("this type has been registered: " + typeof(T));

				//registration = Activator.CreateInstance(solidType);

					Dependencies.Add(typeof(T), solidType);
				
			}
			finally { 
			
			//exit write lock
			}
		}
		//simplified version, just news up the solid type assumming it has a parameterless constructor
		public static T Resolve<T>()

		{
			object registration = null;

			if (Dependencies == null || Dependencies.Count == 0)
				throw new Exception("IOC Framework Missing Registrations, including a registration for : " + typeof(T));

			Dependencies.TryGetValue(typeof(T), out registration);
			return (T)registration;//returns null if trygetvalue is false, test this

		}

	}


	public class Registration
	{

		// define this later, for now use type as dependency and "object" as solid
	}
}
