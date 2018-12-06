using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace SPDemo.Models
{
    public class ItemDB
    {
        //create Conection string

        string cs = ConfigurationManager.ConnectionStrings["MyContext"].ConnectionString;

        //return list of all item
        public List<ITEM> ListAll()
        {
            List<ITEM> lst = new List<ITEM>();
            OracleConnection con = new OracleConnection();
            con.ConnectionString = cs;
            con.Open();
            string query = "select * from toko.item";
            OracleCommand com = new OracleCommand(query, con);
            //com.CommandType = CommandType.StoredProcedure;
            OracleDataReader rdr = com.ExecuteReader();
            while (rdr.Read())
            {
                lst.Add(new ITEM
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    NAME = rdr["NAME"].ToString(),
                    PRICE = Convert.ToInt32(rdr["PRICE"]),
                    STOCK = Convert.ToInt32(rdr["STOCK"]),
                });
            }
            return lst;
        }

        //Method for Adding an ItemDB  
        public int Add(ITEM itemDB)
        {
            int i;
            using (OracleConnection con = new OracleConnection(cs))
            {
                con.Open();
                string query = "Insert into TOKO.ITEM(Name,Price,Stock) VALUES (:2, :3, :4)";
                OracleCommand com = con.CreateCommand();
                com.CommandText = query;
                com.CommandType = CommandType.Text;
                //com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("2", OracleDbType.Varchar2, 50).Value = itemDB.NAME.ToString();
                com.Parameters.Add("3", OracleDbType.Int32, 10).Value = Convert.ToInt32(itemDB.PRICE);
                com.Parameters.Add("4", OracleDbType.Int32, 10).Value = Convert.ToInt32(itemDB.STOCK);
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        //Method for Updating ItemDB record  
        public int Update(ITEM itemDB)
        {
            int i;
            using (OracleConnection con = new OracleConnection(cs))
            {
                con.Open();
                string query = "Update TOKO.ITEM SET name=:2, price=:3, stock=:4 WHERE id=:1";
                OracleCommand com = con.CreateCommand();
                com.CommandText = query;
                com.CommandType = CommandType.Text;
                //com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("2", OracleDbType.Varchar2, 50).Value = itemDB.NAME.ToString();
                com.Parameters.Add("3", OracleDbType.Int32, 10).Value = Convert.ToInt32(itemDB.PRICE);
                com.Parameters.Add("4", OracleDbType.Int32, 10).Value = Convert.ToInt32(itemDB.STOCK);


                com.Parameters.Add("1", OracleDbType.Int32, 10).Value = Convert.ToInt32(itemDB.ID);
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        //Method for Deleting an ItemDB  
        public int Delete(int ID)
        {
            int i;
            using (OracleConnection con = new OracleConnection(cs))
            {
                con.Open();
                string query = "Delete from TOKO.ITEM WHERE id=:1";
                OracleCommand com = con.CreateCommand();
                com.CommandText = query;
                com.CommandType = CommandType.Text;
                //com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("1", OracleDbType.Int32, 10).Value = Convert.ToInt32(ID);
                i = com.ExecuteNonQuery();
            }
            return i;
        }
    }
}