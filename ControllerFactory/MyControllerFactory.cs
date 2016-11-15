using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using System.Collections.Generic;

namespace ControllerFactory
{
	public class NinjectControllerFactory<T> : INinjectControllerFactory<T>
	{
		private IController _myController = null;


		public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
		{
			//_myController = 
			//string name = requestContext.RouteData.Values["controller"].ToString();
			//Type cType = Type.GetType(string.Format
			// ("CustomControllerFactory.Controllers.{0}",controllerName));
			// typeof(Home);                                    

			//IController myController = Activator.CreateInstance(cType) as IController;
			return _myController;
		}

		public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
		{
			return SessionStateBehavior.Default;
		}

		public void ReleaseController(IController controller)
		{
			var disposable = _myController as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		IController INinjectControllerFactory<T>.CreateController(List<T> ConstructorDependencies, RequestContext requestContext, string controllerName)
		{
			throw new NotImplementedException();
		}

		IController INinjectControllerFactory<T>.CreateController(T ConstructorDependency, RequestContext requestContext, string controllerName)
		{
			throw new NotImplementedException();
		}
	}
	}


/*
public class My_Controller_Factory : DefaultControllerFactory
{
	public override IController CreateController
	(System.Web.Routing.RequestContext requestContext, string controllerName)
	{
		string controllername = requestContext.RouteData.Values
		["controller"].ToString();
		// Debug.WriteLine(string.Format("Controller Name : {0}", controllername));            
		Type controllerType = Type.GetType(string.Format
			// ("Custom_Controller_Factory.Controllers.{0}",controllername));
			// typeof(Home);            
	   IController controller = Activator.CreateInstance(controllerType) as IController;
		return controller;
	}
	public override void ReleaseController(IController controller)
	{
		IDisposable dispose = controller as IDisposable; if (dispose != null)
		{
			dispose.Dispose();
		}
	}
}
*/
