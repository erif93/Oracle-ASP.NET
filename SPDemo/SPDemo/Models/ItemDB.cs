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
        string cs = ConfigurationManager.ConnectionStrings["TOKO"].ConnectionString;

        //Inline QUERY
        //return list of all item
        //public List<ItemDB> ListAll()
        //{
        //    List<ItemDB> lst = new List<ItemDB>();
        //    OracleConnection con = new OracleConnection();
        //    con.ConnectionString = cs;
        //    con.Open();
        //    string query = "select * from toko.item";
        //    OracleCommand com = new OracleCommand(query, con);
        //    //com.CommandType = CommandType.StoredProcedure;
        //    OracleDataReader rdr = com.ExecuteReader();
        //    while (rdr.Read())
        //    {
        //        lst.Add(new ItemDB
        //        {
        //            ID = Convert.ToInt32(rdr["ID"]),
        //            NAME = rdr["NAME"].ToString(),
        //            PRICE = Convert.ToInt32(rdr["PRICE"]),
        //            STOCK = Convert.ToInt32(rdr["STOCK"]),
        //        });
        //    }
        //    return lst;
        //}

        //Store Procedure
        public List<ItemDB> ListAll()
        {
            List<ItemDB> lst = new List<ItemDB>();
            OracleConnection con = new OracleConnection();
            con.ConnectionString = cs;
            con.Open();
<<<<<<< HEAD
            string query = "select * from transaction.item";
            OracleCommand com = new OracleCommand(query, con);
            //com.CommandType = CommandType.StoredProcedure;
            OracleDataReader rdr = com.ExecuteReader();
=======
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "TOKO.GETITEMALL";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new OracleParameter("itemCur", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader rdr = cmd.ExecuteReader();
>>>>>>> 78bb02bc2127fb373c083917fd31a3e8023c9e52
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

        //Inline Query
        //Method for Adding an ItemDB  
        //public int Add(ItemDB itemDB)
        //{
        //    int i;
        //    using (OracleConnection con = new OracleConnection(cs))
        //    {
        //        con.Open();
        //        string query = "Insert into TOKO.ITEM(Name,Price,Stock) VALUES (:2, :3, :4)";
        //        OracleCommand com = con.CreateCommand();
        //        com.CommandText = query;
        //        com.CommandType = CommandType.Text;
        //        //com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.Add("2", OracleDbType.Varchar2, 50).Value = itemDB.NAME.ToString();
        //        com.Parameters.Add("3", OracleDbType.Int32, 10).Value = Convert.ToInt32(itemDB.PRICE);
        //        com.Parameters.Add("4", OracleDbType.Int32, 10).Value = Convert.ToInt32(itemDB.STOCK);
        //        i = com.ExecuteNonQuery();
        //    }
        //    return i;
        //}

        //Store Procedure
        public int Add(ItemDB itemDB)
        {
            int i;
            using (OracleConnection con = new OracleConnection(cs))
            {
                con.Open();
<<<<<<< HEAD
                string query = "Insert into TRANSACTION.ITEM(Name,Price,Stock) VALUES (:2, :3, :4)";
                OracleCommand com = con.CreateCommand();
                com.CommandText = query;
                com.CommandType = CommandType.Text;
                //com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("2", OracleDbType.Varchar2, 50).Value = itemDB.NAME.ToString();
                com.Parameters.Add("3", OracleDbType.Double, 10).Value = Convert.ToDouble(itemDB.PRICE);
                com.Parameters.Add("4", OracleDbType.Int16, 10).Value = Convert.ToInt16(itemDB.STOCK);
                i = com.ExecuteNonQuery();
=======
                string query = "TOKO.INSERT_ITEM";
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = query;
                cmd.Parameters.Add(new OracleParameter("i_name", OracleDbType.Varchar2)).Value = itemDB.NAME.ToString();
                cmd.Parameters.Add(new OracleParameter("i_price", OracleDbType.Varchar2)).Value = Convert.ToInt32(itemDB.PRICE);
                cmd.Parameters.Add(new OracleParameter("i_stock", OracleDbType.Varchar2)).Value = Convert.ToInt32(itemDB.STOCK);
                i = cmd.ExecuteNonQuery();

>>>>>>> 78bb02bc2127fb373c083917fd31a3e8023c9e52
            }
            return i;
        }

        //Method for Updating ItemDB record  
        //public int Update(ItemDB itemDB)
        //{
        //    int i;
        //    using (OracleConnection con = new OracleConnection(cs))
        //    {
        //        con.Open();
        //        string query = "Update TOKO.ITEM SET name=:2, price=:3, stock=:4 WHERE id=:1";
        //        OracleCommand com = con.CreateCommand();
        //        com.CommandText = query;
        //        com.CommandType = CommandType.Text;
        //        //com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.Add("2", OracleDbType.Varchar2, 50).Value = itemDB.NAME.ToString();
        //        com.Parameters.Add("3", OracleDbType.Int32, 10).Value = Convert.ToInt32(itemDB.PRICE);
        //        com.Parameters.Add("4", OracleDbType.Int32, 10).Value = Convert.ToInt32(itemDB.STOCK);


        //        com.Parameters.Add("1", OracleDbType.Int32, 10).Value = Convert.ToInt32(itemDB.ID);
        //        i = com.ExecuteNonQuery();
        //    }
        //    return i;
        //}

        //Store Procedure
        public int Update(ItemDB itemDB)
        {
            int i;
            using (OracleConnection con = new OracleConnection(cs))
            {
                con.Open();
<<<<<<< HEAD
                string query = "Update TRANSACTION.ITEM SET name=:2, price=:3, stock=:4 WHERE id=:1";
                OracleCommand com = con.CreateCommand();
                com.CommandText = query;
                com.CommandType = CommandType.Text;
                //com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("2", OracleDbType.Varchar2, 50).Value = itemDB.NAME.ToString();
                com.Parameters.Add("3", OracleDbType.Int32, 10).Value = Convert.ToInt32(itemDB.PRICE);
                com.Parameters.Add("4", OracleDbType.Int32, 10).Value = Convert.ToInt32(itemDB.STOCK);

=======
                string query = "TOKO.UPDATE_ITEM";
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = query;
                cmd.Parameters.Add(new OracleParameter("i_id", OracleDbType.Varchar2)).Value = Convert.ToInt32(itemDB.ID);
                cmd.Parameters.Add(new OracleParameter("i_name", OracleDbType.Varchar2)).Value = itemDB.NAME.ToString();
                cmd.Parameters.Add(new OracleParameter("i_price", OracleDbType.Varchar2)).Value = Convert.ToInt32(itemDB.PRICE);
                cmd.Parameters.Add(new OracleParameter("i_stock", OracleDbType.Varchar2)).Value = Convert.ToInt32(itemDB.STOCK);
                i = cmd.ExecuteNonQuery();
>>>>>>> 78bb02bc2127fb373c083917fd31a3e8023c9e52

            }
            return i;
        }

        //Method for Deleting an ItemDB  
        //public int Delete(int ID)
        //{
        //    int i;
        //    using (OracleConnection con = new OracleConnection(cs))
        //    {
        //        con.Open();
        //        string query = "Delete from TOKO.ITEM WHERE id=:1";
        //        OracleCommand com = con.CreateCommand();
        //        com.CommandText = query;
        //        com.CommandType = CommandType.Text;
        //        //com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.Add("1", OracleDbType.Int32, 10).Value = Convert.ToInt32(ID);
        //        i = com.ExecuteNonQuery();
        //    }
        //    return i;
        //}

        //Store Procedure
        public int Delete(int ID)
        {
            int i;
            using (OracleConnection con = new OracleConnection(cs))
            {
                con.Open();
<<<<<<< HEAD
                string query = "Delete from TRANSACTION.ITEM WHERE id=:1";
                OracleCommand com = con.CreateCommand();
                com.CommandText = query;
                com.CommandType = CommandType.Text;
                //com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("1", OracleDbType.Int32, 10).Value = Convert.ToInt32(ID);
                i = com.ExecuteNonQuery();
=======
                string query = "TOKO.DELETE_ITEM";
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = query;
                cmd.Parameters.Add(new OracleParameter("i_id", OracleDbType.Varchar2)).Value = Convert.ToInt32(ID);
                
                i = cmd.ExecuteNonQuery();

>>>>>>> 78bb02bc2127fb373c083917fd31a3e8023c9e52
            }
            return i;
        }
    }
}