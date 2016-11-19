using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using System.Collections.Generic;
using ControllerFactory.Controllers;
using System.Reflection;

namespace ControllerFactory
{
	public class MyControllerFactory : DefaultControllerFactory
	{
		private IController _myController = null;
		private string _controllerNamespace = "ControllerFactory.Controllers.";

		public override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
		{
			//to make it easy: (simplest implementation)
			Type controllerType = Type.GetType(string.Concat(_controllerNamespace, controllerName , "Controller"));

			return (IController) IOC.Resolve(controllerType);
		}

		public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
		{
			return SessionStateBehavior.Default;
		}

		public override void ReleaseController(IController controller)
		{
			var disposable = _myController as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		IController CreateController<T>(List<T> ConstructorDependencies, RequestContext requestContext, string controllerName)
		{
			throw new NotImplementedException();
		}

		IController CreateController<T>(T ConstructorDependency, RequestContext requestContext, string controllerName)
		{
			throw new NotImplementedException();
		}
	}
}



