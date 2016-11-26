using System;
using System.Threading;

namespace IOC
{
	//why are these static and readonly ??????geez
	public class ScopeCallbacks
	{
		public static readonly Func<IContext, object> Transient = ctx => null;

		public static readonly Func<IContext, object> Thread = ctx => System.Threading.Thread.CurrentThread;

		public static readonly Func<IContext, object> Singleton = ctx => ctx.CurrentKernel;
	}
}
