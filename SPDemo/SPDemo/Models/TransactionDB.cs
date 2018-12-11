﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace SPDemo.Models
{
    public class TransactionDB
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public int QTY { get; set; }
        public int PRICE { get; set; }
        public int ID_ITEM { get; set; }
        public int ID_TRANSAKSI { get; set; }
        public int TOTAL { get; set; }

        public TransactionDB() { }

        string cs = ConfigurationManager.ConnectionStrings["OracleDBContext"].ConnectionString;

        public int Add(IEnumerable<TransactionDB> transac)
        {
            Type type = typeof(TransactionDB);
            var propIdItem = type.GetProperty("ID_ITEM");
            var propQty = type.GetProperty("QTY");
            var propPrice = type.GetProperty("PRICE");
            var propName = type.GetProperty("NAME");

            int i;
            using (OracleConnection con = new OracleConnection(cs))
            {
                con.Open();
                string query = "TOKO.INSERT_TRANSMASTER";
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = query;
                cmd.Parameters.Add(new OracleParameter("i_date", OracleDbType.Date)).Value = DateTime.Now;
                cmd.Parameters.Add(new OracleParameter("i_total", OracleDbType.Int32)).Value = Convert.ToInt32(0);
                i = cmd.ExecuteNonQuery();

                string query2 = "TOKO.GETMAXID";
                OracleCommand cmd2 = new OracleCommand();
                cmd2.Connection = con;
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.CommandText = query2;
                cmd2.Parameters.Add(new OracleParameter("maxId", OracleDbType.Int32)).Direction = ParameterDirection.ReturnValue;
                cmd2.ExecuteNonQuery();
                int idmax = int.Parse(cmd2.Parameters["maxId"].Value.ToString());

                foreach (var item in transac)
                {
                    int IdItem = Convert.ToInt32(propIdItem.GetValue(item, null));
                    int QtyItem = Convert.ToInt32(propQty.GetValue(item, null));
                    int PriceItem = Convert.ToInt32(propPrice.GetValue(item, null));
                    string NameItem = propName.GetValue(item, null).ToString();
                    string query3 = "TOKO.INSERT_DETILTRANSAKSI";
                    OracleCommand cmd3 = new OracleCommand();
                    cmd3.Connection = con;
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.CommandText = query3;
                    cmd3.Parameters.Add(new OracleParameter("detil_item", OracleDbType.Int32)).Value = Convert.ToInt32(IdItem);
                    cmd3.Parameters.Add(new OracleParameter("detil_transmaster", OracleDbType.Int32)).Value = idmax;
                    cmd3.Parameters.Add(new OracleParameter("detil_name", OracleDbType.Varchar2)).Value = NameItem;
                    cmd3.Parameters.Add(new OracleParameter("detil_price", OracleDbType.Int32)).Value = Convert.ToInt32(PriceItem);
                    cmd3.Parameters.Add(new OracleParameter("detil_qty", OracleDbType.Int32)).Value = Convert.ToInt32(QtyItem);
                    cmd3.Parameters.Add(new OracleParameter("detil_createdate", OracleDbType.Date)).Value = DateTime.Now;
                    i = cmd3.ExecuteNonQuery();
                }
                
            }
            return i;
        }

    }
}