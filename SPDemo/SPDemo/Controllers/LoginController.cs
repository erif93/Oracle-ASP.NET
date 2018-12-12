using SPDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPDemo.Controllers
{
    public class LoginController : Controller
    {
        AccountDB AccountDB = new AccountDB();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(AccountDB account)
        {
            var k = Json(AccountDB.Login(account), JsonRequestBehavior.AllowGet);
            return k;
        }
    }
}