using System;
using System.Web;

namespace IOC
{
	public static class ScopeCallbacks
	{
		private static object lockObj = new object();

		public static readonly Func<IContext, object> Transient = ctx => null;

		public static readonly Func<IContext, object> Thread = ctx =>
		{
			ctx.CurrentThread = System.Threading.Thread.CurrentThread;
			return ctx.CurrentThread;
		};

		public static readonly Func<IContext, object> Singleton = ctx => ctx.CurrentKernel;

		public static readonly Func<IContext, object> WebRequest = ctx =>
		{
			lock(lockObj)
			{
				var httpContext = HttpContext.Current;
				if (httpContext.Items.Contains(ctx))
					return ctx;

				httpContext.Items.Add(ctx, "");
				return ctx;
			}
		};
	}
}
