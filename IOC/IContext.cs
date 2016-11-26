using System;
namespace IOC
{
	/*
	Contains information about the activation of a single instance.
	*/

	public interface IContext
	{
		//this should be readonly?
		Kernel CurrentKernel { get; set; }

		object GetScope();

		Type TargetImplementationType { get; set;}

		object TargetImplementationInstance { get; set;}

		Request Request { get; set; }

		LifeCycleScope Scope { get; set;}
	}

	
}
