using SPDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPDemo.Controllers
{
    public class AccountsController : Controller
    {
        AccountDB AccountDB = new AccountDB();
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            return Json(AccountDB.ListAll(), JsonRequestBehavior.AllowGet);
        }

        // GET: Accounts/Details/5
        public JsonResult GetbyID(int ID)
        {
            var account = AccountDB.ListAll().Find(x => x.ID.Equals(ID));
            //Ini Dipakai kalau pakai metode decode karena two way
            //account.PASSWORD = DecryptString(account.PASSWORD);
            return Json(account, JsonRequestBehavior.AllowGet);
        }

        // GET: Accounts/Create
        public JsonResult Add(AccountDB account)
        {
            var k= Json(AccountDB.Add(account), JsonRequestBehavior.AllowGet);
            return k;
        }

        public JsonResult Update(AccountDB account)
        {
            var k=Json(AccountDB.Update(account), JsonRequestBehavior.AllowGet);
            return k;
        }

        public JsonResult Delete(int ID)
        {
            return Json(AccountDB.Delete(ID), JsonRequestBehavior.AllowGet);
        }
        
        //Decode dari base64, md5 gk bisa decrypt karena oneway
        public string DecryptString(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException fe)
            {
                decrypted = "";
            }
            return decrypted;
        }
    }
}
