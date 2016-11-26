using System;
namespace IOC
{
	public class BindingConfiguration
	{
		public BindingConfiguration()
		{
			this.ScopeCallback = LifeCycleScope.TRANSIENT;
		}

		public LifeCycleScope ScopeCallback { get; set; }

	}
}
