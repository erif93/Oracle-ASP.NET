using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace SPDemo.Models
{
    public class ItemDB
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public decimal PRICE { get; set; }
        public decimal STOCK { get; set; }

        public ItemDB() { }
        //create Conection string
        string cs = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;

        //Store Procedure
        public List<ItemDB> ListAll()
        {
            List<ItemDB> lst = new List<ItemDB>();
            OracleConnection con = new OracleConnection();
            con.ConnectionString = cs;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "TRANSACTION.GETITEMALL";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new OracleParameter("itemCur", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                lst.Add(new ItemDB
                {
                    ID = Convert.ToInt32(rdr.GetValue(0)),
                    NAME = rdr.GetValue(1).ToString(),
                    PRICE = Convert.ToInt32(rdr.GetValue(2)),
                    STOCK = Convert.ToInt32(rdr.GetValue(3)),
                });
            }
            
            return lst;
        }
        
        //Store Procedure
        public int Add(ItemDB itemDB)
        {
            int i;
            using (OracleConnection con = new OracleConnection(cs))
            {
                con.Open();
                string query = "TRANSACTION.INSERT_ITEM";
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = query;
                cmd.Parameters.Add(new OracleParameter("i_name", OracleDbType.Varchar2)).Value = itemDB.NAME.ToString();
                cmd.Parameters.Add(new OracleParameter("i_price", OracleDbType.Varchar2)).Value = Convert.ToInt32(itemDB.PRICE);
                cmd.Parameters.Add(new OracleParameter("i_stock", OracleDbType.Varchar2)).Value = Convert.ToInt32(itemDB.STOCK);
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        
        //Store Procedure
        public int Update(ItemDB itemDB)
        {
            int i;
            using (OracleConnection con = new OracleConnection(cs))
            {
                con.Open();
                string query = "TRANSACTION.UPDATE_ITEM";
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = query;
                cmd.Parameters.Add(new OracleParameter("i_id", OracleDbType.Varchar2)).Value = Convert.ToInt32(itemDB.ID);
                cmd.Parameters.Add(new OracleParameter("i_name", OracleDbType.Varchar2)).Value = itemDB.NAME.ToString();
                cmd.Parameters.Add(new OracleParameter("i_price", OracleDbType.Varchar2)).Value = Convert.ToInt32(itemDB.PRICE);
                cmd.Parameters.Add(new OracleParameter("i_stock", OracleDbType.Varchar2)).Value = Convert.ToInt32(itemDB.STOCK);
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        

        //Store Procedure
        public int Delete(int ID)
        {
            int i;
            using (OracleConnection con = new OracleConnection(cs))
            {
                con.Open();
                string query = "TRANSACTION.DELETE_ITEM";
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = query;
                cmd.Parameters.Add(new OracleParameter("i_id", OracleDbType.Varchar2)).Value = Convert.ToInt32(ID);
                
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
    }
}