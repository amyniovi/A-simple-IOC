using System;
using System.Collections.Generic;

namespace ControllerFactory
{
	public static class IOC
	{
		internal static Dictionary<Type, object> Dependencies = null;

		public static void BindTo(this Type type, Type solidType) {
			//enter write lock
			try {
				object registration = null;

				if (solidType == null)
					throw new ArgumentNullException();

				if ( Dependencies!=null && Dependencies.TryGetValue(type, out registration))
					throw new Exception("this type has been registered: " + type);
				
				registration = Activator.CreateInstance(solidType);
				Dependencies.Add(type, registration);
				
			}
			finally { 
			
			//exit write lock
			}
		}
		public static object Resolve(Type passedInDependency)

		{
			object registration = null;

			if (passedInDependency == null)
				throw new ArgumentNullException();

			if (Dependencies == null || Dependencies.Count == 0)
				throw new Exception("IOC Framework Missing Registrations, including a registration for : " + passedInDependency);


			Dependencies.TryGetValue(passedInDependency, out registration);
			return registration;//returns null if trygetvalue is false, test this

		}


	}
	public class Registration
	{

		// define this later, for now use "object"
	}
}
