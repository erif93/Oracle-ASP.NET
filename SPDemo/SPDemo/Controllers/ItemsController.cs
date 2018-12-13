using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;
using SPDemo.Models;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Configuration;
using System.Data;

namespace SPDemo.Controllers
{
    public class ItemsController : Controller
    {
        ItemDB itemDB = new ItemDB();
        string cs = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;
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

        public ActionResult exportItem()
        {
            List<ItemDB> allItems = new List<ItemDB>();
            OracleConnection con = new OracleConnection();
            con.ConnectionString = cs;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "TOKO.GETITEMALL";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new OracleParameter("itemCur", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                allItems.Add(new ItemDB
                {
                    ID = Convert.ToInt32(rdr.GetValue(0)),
                    NAME = rdr.GetValue(1).ToString(),
                    PRICE = Convert.ToInt32(rdr.GetValue(2)),
                    STOCK = Convert.ToInt32(rdr.GetValue(3)),
                });
            }
            

            ReportDocument repdoc = new ReportDocument();
            repdoc.Load(Path.Combine(Server.MapPath("~/Reports"), "CR_Item.rpt"));

            repdoc.SetDataSource(allItems);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = repdoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ItemList.pdf");
        }

    }
}
