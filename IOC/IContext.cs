using System;
using System.Threading;
using System.Web;

namespace IOC
{
	/*
	Contains information about the activation of a single instance.
	*/

	public interface IContext
	{
		Kernel CurrentKernel { get; set; }

		object GetScope();
		//Solid Type's type
		Type TargetImplementationType { get; set;}
		//Solid Type's instance
		object TargetImplementationInstance { get; set;}

		LifeCycleScope Scope { get; set;}

		Thread CurrentThread { get; set;}
	}
}
