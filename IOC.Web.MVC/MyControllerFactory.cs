using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

/*
This class inherits from DefaultControllerFactory in MVC framework.
Used to allow constructor dependency injection in the MVC Controller class.
In an MVC project in Global.asax, you determine this class will be used instead of the DefaultControllerFactory. 
A kernel is passed in this class, this is the point of contact of the IOC and MVC project. 
Resource location is used to resolve the controller class.
This class is part of the NUGET package for MVC related projects only
*/
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
			base.ReleaseController(_myController);
		}

	}
}



