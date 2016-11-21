using System;
using System.Collections.Generic;

namespace ControllerFactory
{

	public static class IOC
	{
		// Services: Contract - Implementation Types
		internal static Dictionary<Type, Type> Services = new Dictionary<Type, Type>();
		//ImplementationConstructorDependencies
		internal static Dictionary<Type, List<Type>> ImplementationCtorInfo = new Dictionary<Type, List<Type>>();

		public static void Bind<T, U>()
		{
			//enter write lock
			try
			{
				Services.Add(typeof(T), typeof(U));
				var ctorDependencies = GetTypeConstructorDependencies(typeof(U));
				foreach (var type in ctorDependencies)
				{
					if (Services.ContainsKey(type))
					{

						//constructor dependencies for type have already been registered to a concrete type

					}
					else {
						throw new Exception("Constructor of type: " + type + " has unregistered Dependencies with the IOC");
					}

				}
				if (ctorDependencies != null && ctorDependencies.Count > 0)
					ImplementationCtorInfo.Add(typeof(U), ctorDependencies);


			}
			catch (Exception e)
			{

				throw new Exception("BIND ERROR: " + e.Message);
			}
			finally
			{

				//exit write lock
			}
		}


		//simplified version, just news up the solid type assumming it has a parameterless constructor
		public static object Resolve(Type type)
		{
			Type registration;
			List<Type> registrationCtorDependencies;
			List<object> registrationCtorSolidTypes = new List<object>();

			//if (Services == null || Services.Count == 0)
			//	throw new Exception("IOC Framework Missing Registrations, including a registration for : " + type);

			try
			{
				Services.TryGetValue(type, out registration);

				if (ImplementationCtorInfo.TryGetValue(registration, out registrationCtorDependencies))
				{
					//hard shit
					foreach (var dependencyType in registrationCtorDependencies)
					{
						registrationCtorSolidTypes.Add(Resolve(dependencyType));


					}
					return Activator.CreateInstance(registration, registrationCtorSolidTypes.ToArray());
				}



				return Activator.CreateInstance(registration);
			}
			catch (Exception e)
			{

				throw new Exception("RESOLVE EROR: " + e.Message);
			}
		}

		public static T Resolve<T>() where T : class
		{
			return (T)Resolve(typeof(T));
		}

		static List<Type> GetTypeConstructorDependencies(Type registration)
		{
			List<Type> typeConstructorParams = new List<Type>();
			Type registrationType = registration;
			var ctors = registrationType.GetConstructors();

			if (ctors != null)
			{
				// Assuming class ObjectType has only one constructor:
				if (ctors.Length > 1)
					throw new Exception("The Services registered in the IOC should not have more than one constructor.");

				if (ctors.Length != 0)
				{
					var ctor = ctors[0];

					foreach (var param in ctor.GetParameters())
					{
						typeConstructorParams.Add(param.ParameterType);
					}
				}

			}
			return typeConstructorParams;
		}
	}


}
