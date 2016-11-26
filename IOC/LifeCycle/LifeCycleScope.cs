using System;

namespace IOC
{
	public enum LifeCycleScope
	{
		SCOPE = 0,
		TRANSIENT,
		SINGLETON,
		THREAD,
		WEBREQUEST
	}
}
