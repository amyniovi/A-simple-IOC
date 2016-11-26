using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace IOC
{

	public class Kernel
	{
		public const string BIND_ERROR = "BIND ERROR: ";
		public const string RESOLVE_ERROR = "RESOLVE ERROR: ";
		// Services: Contract - Implementation Types
		internal ConcurrentDictionary<Type, IContext> Services = new ConcurrentDictionary<Type, IContext>();
		//ImplementationConstructorDependencies
		internal Dictionary<Type, List<Type>> ImplementationCtorInfo = new Dictionary<Type, List<Type>>();

		public Kernel Bind<T, U>()
		{
			//enter write lock
			try
			{
				if (!(typeof(U) is IContext))
				{
					var serviceContext = new Context();
					serviceContext.TargetImplementationType = typeof(U);

					if (!Services.TryAdd(typeof(T), serviceContext))
						throw new Exception(BIND_ERROR + " Remove duplicate binding");
				}
				else
					if (!Services.TryAdd(typeof(T), (IContext)typeof(U)))
					throw new Exception(BIND_ERROR + " Remove duplicate binding");

				var ctorDependencies = GetTypeConstructorDependencies(typeof(U));
				foreach (var type in ctorDependencies)
				{
					if (Services.ContainsKey(type))
					{
						//constructor dependencies for type have already been registered to a concrete type
						//do nothing
					}
					else {
						throw new Exception(BIND_ERROR + "Constructor of type: " + type + " has unregistered Dependencies with the IOC");
					}

				}
				if (ctorDependencies != null && ctorDependencies.Count > 0)
					ImplementationCtorInfo.Add(typeof(U), ctorDependencies);

				return this;
			}
			catch (Exception e)
			{
				throw new Exception(BIND_ERROR + e.Message);
			}
			finally
			{
				//exit write lock
			}
		}

		public object Resolve(Type type)
		{
			Type registration;
			IContext registrationContext;
			List<Type> registrationCtorDependencies;
			List<object> registrationCtorSolidTypes = new List<object>();

			try
			{
				Services.TryGetValue(type, out registrationContext);
				if (registrationContext.Scope == LifeCycleScope.SINGLETON)
					return registrationContext.TargetImplementationInstance;

				registration = registrationContext.TargetImplementationType;
				if (ImplementationCtorInfo.TryGetValue(registration, out registrationCtorDependencies))
				{
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
				throw new Exception(RESOLVE_ERROR + e.Message);
			}
		}

		public T Resolve<T>() where T : class
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

		public Kernel InScope<T>()
		{
			//implement
			return this;
		}

		public Kernel InTransientScope<T>()
		{
			//implement
			return this;
		}

		//per kernel lifecycle (do we need to do anything extra to ensure its per kernel?
		public Kernel InSingletonScope<T>() where T : class
		{
			IContext singletonCtx = null;
			if (Services.TryGetValue(typeof(T), out singletonCtx))
				if (singletonCtx.Scope == LifeCycleScope.SINGLETON && singletonCtx.TargetImplementationInstance != null)
					//we already have our singleton, do nothing.
					return this;
			if (singletonCtx == null || (singletonCtx != null && singletonCtx.TargetImplementationType == null))
			{
				//we have already bound the service but not in sinlgeton scope 
				throw new Exception("This service has not been bound to a type, Call Bind<T1,T2> first then determine Scope ");
			}
			//we dont really need to make it lazy?
			var singleton = this.Resolve<T>();//new Lazy<T>(() => , true);
			singletonCtx.TargetImplementationInstance = singleton;
			singletonCtx.Scope = LifeCycleScope.SINGLETON;

			Services[typeof(T)] = singletonCtx;

			return this;
		}

		public Kernel InWebRequestScope<T>(System.Net.HttpWebRequest request)
		{
			//implement
			return this;
		}
	}


	/*just testing*/
	public class SubKernel
	{

		public void Dowork()
		{

			var k = new Kernel();
			k.Bind<IContext, IContext>().InSingletonScope<IContext>();
		}
	}

}
