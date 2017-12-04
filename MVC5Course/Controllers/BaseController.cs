using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class BaseController : Controller
    {

        protected ProductRepository repo = RepositoryHelper.GetProductRepository();
        // GET: Bese
        protected override void HandleUnknownAction(string actionName)
        {
            //執行Result
            this.Redirect("/").ExecuteResult(this.ControllerContext);
        }
    }
}