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
        
        [HttpPost]
        public JsonResult Add(IEnumerable<TransactionDB> transac)
        {
            bool status = false;
            int a = transacDB.Add(transac);

            if (a != 0)
            {
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        public JsonResult GetItem()
        {
            var Item = itemDB.ListAll().ToList();
            return new JsonResult { Data =Item, JsonRequestBehavior =  JsonRequestBehavior.AllowGet };
        }
    }
}