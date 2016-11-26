using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;


namespace IOC
{
	public class MyControllerFactory : DefaultControllerFactory
	{
		private IController _myController = null;
		private Kernel _kernel;


		public MyControllerFactory(Kernel kernel)
		{
			_kernel = kernel;
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			_myController = (IController)_kernel.Resolve(controllerType);
			return _myController;
		}
		public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
		{
			return SessionStateBehavior.Default;
		}

		public override void ReleaseController(IController controller)
		{
			//destroy scope
			 base.ReleaseController(_myController);
		}

	}
}



