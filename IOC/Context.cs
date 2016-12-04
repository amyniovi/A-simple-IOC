using System;
using System.Threading;
using System.Web;
namespace IOC
{
	/*
	Contains metadata about the activation of a single instance.
	*/

	public class Context : IContext
	{
		public Kernel CurrentKernel { get; set; }

		public Thread CurrentThread { get; set; }

		public Type TargetImplementationType { get; set; }

		public object GetScope()
		{
			//hate case statements, another way to do this???
			switch (Scope)
			{
				case LifeCycleScope.TRANSIENT:
					return ScopeCallbacks.Transient(this);
				case LifeCycleScope.SINGLETON:
					return ScopeCallbacks.Singleton(this);
				case LifeCycleScope.THREAD:
				return ScopeCallbacks.Thread(this);
				case LifeCycleScope.WEBREQUEST:
					return ScopeCallbacks.WebRequest(this);
				default:
					return ScopeCallbacks.Transient(this);

			}
		}

		public LifeCycleScope Scope { get; set; }

		public object TargetImplementationInstance { get; set; }
	}
}
