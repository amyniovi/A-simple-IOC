using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using System.Collections.Generic;
using ControllerFactory.Controllers;
using System.Reflection;

namespace ControllerFactory
{
	public class MyControllerFactory : IControllerFactory
	{
		private IController _myController = null;


		public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
		{
			//Type cType = Type.GetType(controllerName);


			//ConstructorInfo info = cType.GetConstructor(new Type[] { ILogger, IAmCrap });
			//_myController = 
			//string name = requestContext.RouteData.Values["controller"].ToString();
			//Type cType = Type.GetType(string.Format
			// ("CustomControllerFactory.Controllers.{0}",controllerName));
			// typeof(Home);                                    

			//IController myController = Activator.CreateInstance(cType) as IController;
			//i need to finc the controller dynamically and my IOC needs to register dependencies for a type
			return new HomeController(IOC.Resolve<ILogger>());
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
