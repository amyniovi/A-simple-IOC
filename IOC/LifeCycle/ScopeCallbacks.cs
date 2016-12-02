using System;
using System.Web;

namespace IOC
{
	public static class ScopeCallbacks
	{
		public static readonly Func<IContext, object> Transient = ctx => null;

		public static readonly Func<IContext, object> Thread = ctx =>
		{
			ctx.CurrentThread = System.Threading.Thread.CurrentThread;
			return ctx.CurrentThread;
		};

		public static readonly Func<IContext, object> Singleton = ctx => ctx.CurrentKernel;

		public static readonly Func<IContext, object> WebRequest = ctx =>
		{
			var httpRequest = HttpContext.Current.Request;
			ctx.Request = new HttpRequestWrapper(httpRequest);
			return ctx.Request;
		};
	}
}
