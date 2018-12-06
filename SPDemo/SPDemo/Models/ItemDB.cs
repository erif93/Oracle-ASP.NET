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
            //string query = "select * from toko.item";
            OracleCommand com = new OracleCommand("GETDATA", con);
            com.CommandType = CommandType.StoredProcedure;
            OracleDataReader rdr = com.ExecuteReader();
            while (rdr.Read())
            {
                lst.Add(new ITEM
                {
                    NAME = rdr["kc_rec.Name"].ToString(),
                    PRICE = Convert.ToInt32(rdr["kc_rec.Price"]),
                    STOCK = Convert.ToInt32(rdr["kc_rec.Stock"]),
                });
            }
            return lst;
        }

        //Method for Adding an ItemDB  
        //public int Add(ITEM itemDB)
        //{
        //    int i;
        //    using (OracleConnection con = new OracleConnection(cs))
        //    {
        //        con.Open();
        //        OracleCommand com = new OracleCommand("insert_item", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.Add("i_id", itemDB.);
        //        com.Parameters.Insert("@Name", itemDB.Name);
        //        com.Parameters.AddWithValue("@Age", itemDB.Age);
        //        com.Parameters.AddWithValue("@State", itemDB.State);
        //        com.Parameters.AddWithValue("@Action", "Insert");
        //        i = com.ExecuteNonQuery();
        //    }
        //    return i;
        //}

        //Method for Updating ItemDB record  
        //public int Update(ItemDB itemDB)
        //{
        //    int i;
        //    using (OracleConnection con = new OracleConnection(cs))
        //    {
        //        con.Open();
        //        OracleCommand com = new OracleCommand("InsertUpdateItemDB", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("@Id", itemDB.ItemDBID);
        //        com.Parameters.AddWithValue("@Name", itemDB.Name);
        //        com.Parameters.AddWithValue("@Age", itemDB.Age);
        //        com.Parameters.AddWithValue("@State", itemDB.State);
        //        com.Parameters.AddWithValue("@Country", itemDB.Country);
        //        com.Parameters.AddWithValue("@Action", "Update");
        //        i = com.ExecuteNonQuery();
        //    }
        //    return i;
        //}

        //Method for Deleting an ItemDB  
        //public int Delete(int ID)
        //{
        //    int i;
        //    using (OracleConnection con = new OracleConnection(cs))
        //    {
        //        con.Open();
        //        OracleCommand com = new OracleCommand("DeleteItemDB", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("@Id", ID);
        //        i = com.ExecuteNonQuery();
        //    }
        //    return i;
        //}
    }
}