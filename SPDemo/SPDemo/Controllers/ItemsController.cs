using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;
using SPDemo.Models;

namespace SPDemo.Controllers
{
    public class ItemsController : Controller
    {
        ItemDB itemDB = new ItemDB();
        // GET: Items
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            return Json(itemDB.ListAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(ItemDB item)
        {
            return Json(itemDB.Add(item), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(ItemDB item)
        {
            return Json(itemDB.Update(item), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetbyID(int ID)
        {
            var Item = itemDB.ListAll().Find(x => x.ID.Equals(ID));
            return Json(Item, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int ID)
        {
            return Json(itemDB.Delete(ID), JsonRequestBehavior.AllowGet);
        }

    }
}
