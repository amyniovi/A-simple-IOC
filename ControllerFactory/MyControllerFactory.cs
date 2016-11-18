﻿using System;
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

		public override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
		{
			//Type cType = Type.GetType(controllerName);

			//ConstructorInfo info = cType.GetConstructor(new Type[] { ILogger, IAmCrap });
			//_myController = 
			//string name = requestContext.RouteData.Values["controller"].ToString();
			//Type cType = Type.GetType(string.Format
			// ("CustomControllerFactory.Controllers.{0}",controllerName));
			// typeof(Home);                                    

			//IController myController = Activator.CreateInstance(cType) as IController;
			//i need to finc the controller dynamically and my IOC needs to register dependencies for a 


			//return (IController) IOC.Resolve(cType);
			return new HomeController(IOC.Resolve<ILogger>());
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



