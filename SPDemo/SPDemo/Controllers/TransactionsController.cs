using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPDemo.Models;

namespace SPDemo.Controllers
{
    public class TransactionsController : Controller
    {
        TransactionDB transacDB = new TransactionDB();
        ItemDB itemDB = new ItemDB();
        // GET: Transactions
        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult Add(TransactionDB transac)
        {
            return Json(transacDB.Add(transac), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetItem()
        {
            var Item = itemDB.ListAll().ToList();
            return Json(Item, JsonRequestBehavior.AllowGet);
        }
    }
}