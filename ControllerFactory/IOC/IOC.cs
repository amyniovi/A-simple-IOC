using System;
using System.Collections.Generic;

namespace ControllerFactory
{
	public static class IOC
	{
		internal static Dictionary<Type, object> Dependencies = new Dictionary<Type, object>();

		internal static Dictionary<Type, Type> RobertFace = new Dictionary<Type, Type>();

		public static void Bind2<T, U>()
		{
			//enter write lock
			try
			{
				RobertFace.Add(typeof(T), typeof(U));

			}
			finally
			{

				//exit write lock
			}
		}

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
			Type registration;

			if (RobertFace == null || RobertFace.Count == 0)
				throw new Exception("IOC Framework Missing Registrations, including a registration for : " + typeof(T));

			RobertFace.TryGetValue(typeof(T), out registration);

			return (T) Activator.CreateInstance(registration);
		}

	}


	public class Registration
	{

		// define this later, for now use type as dependency and "object" as solid
	}
}
