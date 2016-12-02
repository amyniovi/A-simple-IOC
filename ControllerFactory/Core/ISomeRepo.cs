using System;
using System.Web.Mvc;
using IOC.Controllers;
using System.Collections;
using System.Collections.Generic;

namespace IOC
{

	public interface ISomeRepo
	{

		List<String> GetAll();

	}

}
