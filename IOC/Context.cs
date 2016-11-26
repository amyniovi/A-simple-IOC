using System;
namespace IOC
{
	/*
	Contains information about the activation of a single instance.
	*/

	public class Context : IContext
	{
		public Kernel CurrentKernel
		{
			get; set;
		}

		public Type TargetImplementationType
		{
			get; set;
		}

		public object GetScope()
		{
			throw new NotImplementedException();
		}

		public LifeCycleScope Scope{get; set;}

		public Request Request
		{
			get; set;
		}

		public object TargetImplementationInstance
		{
			get; set;
		}
	}
}
