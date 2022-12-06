using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Easy.Hosts.Site.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error404()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult Error500()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}