using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Web;
namespace IOC
{
	/*
	 * Assumption: One Implementation binding per contract type. 
	*/
	public class Kernel
	{
		public const string BIND_ERROR = "BIND ERROR: ";
		public const string RESOLVE_ERROR = "RESOLVE ERROR: ";
		// Services: Contract - Implementation Types
		internal ConcurrentDictionary<Type, IContext> Services = new ConcurrentDictionary<Type, IContext>();
		//ImplementationConstructorDependencies
		internal Dictionary<Type, List<Type>> ImplementationCtorInfo = new Dictionary<Type, List<Type>>();

		//TODO:this List is storing web Requests, dont think that s right and its not working
		//Anyway, gotta make this List<> a concurrent collection.
		public List<HttpRequestBase> Requests = new List<HttpRequestBase>();

		/*
		 * Bind Contract Type to Implementation Type
		 * All this does is add types to the Services Dictionary
		 * It also adds the constructor dependency types for each implementation type in the ImplementationCtorInfo Dictionary 
		 */
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

		/*
		 * Returns Implementation Instance given Contract Type T
		 */
		public T Resolve<T>() where T : class
		{
			return (T)Resolve(typeof(T));
		}

		/*
		 * Returns Implementation Instance given Contract Type
		 */
		public object Resolve(Type type)
		{
			IContext registrationContext;
			List<object> registrationCtorSolidTypes = new List<object>();

			try
			{
				Services.TryGetValue(type, out registrationContext);

				if (registrationContext == null)
					throw new Exception(RESOLVE_ERROR + type + "has not been registered with the IOC.");

				CheckScope(registrationContext);
				if (registrationContext.TargetImplementationInstance != null)
					return registrationContext.TargetImplementationInstance;

				return ActivateNewService(registrationContext, registrationCtorSolidTypes);
			}
			catch (Exception e)
			{
				throw new Exception(RESOLVE_ERROR + e.Message);
			}
		}

		object ActivateNewService(IContext registrationContext, List<object> registrationCtorSolidTypes)
		{
			List<Type> registrationCtorDependencies;
			Type registration = registrationContext.TargetImplementationType;
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

		/*
		 * Returns Implementation Types for Constructor Dependencies for a Type
		 */
		static List<Type> GetTypeConstructorDependencies(Type registration)
		{
			List<Type> typeConstructorParams = new List<Type>();
			Type registrationType = registration;
			var ctors = registrationType.GetConstructors();

			if (ctors != null)
			{
				// Assuming class has only one constructor:
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

		/*
		 * This method retrieves the scope for the Context, ie calls a delegate (to be more precise a ‘Func’) that defines 
		 * the object associated with the life time  of the instance. As long as the object returned by the delegate 
		 * remains alive (not garbage collected), the associated instance is returned. 
		 * Otherwise a new instance is activated
		 */
		private void CheckScope(IContext context)
		{
			var scope = context.GetScope();
			if (scope == null)
			{
				context.TargetImplementationInstance = null;
				return;
			}
			//per Kernel Scope
			if (scope.GetType() == this.GetType())
			{
				if (context.CurrentKernel != this)//dont think this is possible anyway
				{
					context.TargetImplementationInstance = null;
					return;
				}

			}
			//per Thread Scope
			if (scope is Thread)
			{
				if (context.CurrentThread != Thread.CurrentThread)
				{
					context.TargetImplementationInstance = null;
					return;
				}

			}
			//per WebRequest Scope
			if (scope is IContext)
			{
				var httpContext = HttpContext.Current;
				if (!httpContext.Items.Contains(context))
					context.TargetImplementationInstance = null;
			}
			return;
		}

		/*
		 * A new Instance is activated for every request
		 */
		public Kernel InTransientScope<T>()
		{
			//do nothing, we dont cache an instance here
			return this;
		}

		/*
		 * A new Instance is activated per Thread
		 */
		public Kernel InThreadScope<T>()
		{
			//havent implemented this yet
			return this;
		}

		/*
		 * A new Instance is activated per Kernel
		 */
		public Kernel InSingletonScope<T>() where T : class
		{
			IContext singletonCtx = null;
			if (Services.TryGetValue(typeof(T), out singletonCtx))
				EnsureServiceHasBeenBoundToAContext(singletonCtx);

			SetUpContextScope<T>(singletonCtx, LifeCycleScope.SINGLETON);

			return this;
		}

		static void EnsureServiceHasBeenBoundToAContext(IContext context)
		{
			if (context.Scope == LifeCycleScope.SINGLETON && context.TargetImplementationInstance != null)
				//we already have our singleton, do nothing.
				throw new Exception("This service Type has already been bound ");

			if (context == null || (context != null && context.TargetImplementationType == null))
			{
				throw new Exception("This service has not been bound to a solid type. Call Bind<T1,T2> first then determine Scope ");
			}
		}

		/*
		 * A new Instance is activated per Web Request
		 */
		public Kernel InWebRequestScope<T>() where T : class
		{
			IContext webRequestCtx = null;
			if (Services.TryGetValue(typeof(T), out webRequestCtx))
				EnsureServiceHasBeenBoundToAContext(webRequestCtx);

			SetUpContextScope<T>(webRequestCtx, LifeCycleScope.WEBREQUEST);

			return this;
		}

		void SetUpContextScope<T>(IContext context, LifeCycleScope scope) where T : class
		{
			var oldContext = context;
			var instance = this.Resolve<T>();
			context.TargetImplementationInstance = instance;
			context.CurrentKernel = this;
			context.CurrentThread = Thread.CurrentThread;
			context.Scope = scope;

			Services.TryUpdate(typeof(T), context, oldContext);
		}
	}
}
