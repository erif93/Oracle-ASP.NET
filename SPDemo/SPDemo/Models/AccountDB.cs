using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace SPDemo.Models
{
    public class AccountDB
    {
        public int ID { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }

        public AccountDB() { }
        //create Conection string
        string cs = ConfigurationManager.ConnectionStrings["OracleDBContext"].ConnectionString;

        //return list of all item
        public List<AccountDB> ListAll()
        {
            List<AccountDB> lst = new List<AccountDB>();
            OracleConnection con = new OracleConnection();
            con.ConnectionString = cs;
            con.Open();
            string query = "select * from transaction.account";
            OracleCommand com = new OracleCommand(query, con);
            //com.CommandType = CommandType.StoredProcedure;
            OracleDataReader rdr = com.ExecuteReader();
            while (rdr.Read())
            {
                lst.Add(new AccountDB
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    USERNAME = rdr["USERNAME"].ToString(),
                    PASSWORD = rdr["PASSWORD"].ToString(),
                });
            }
            return lst;
        }

        //Method for Adding an ItemDB  
        public int Add(AccountDB accountDB)
        {
            int i;
            using (OracleConnection con = new OracleConnection(cs))
            {
                con.Open();
                string query = "Insert into TRANSACTION.Account(Username,Password) VALUES (:2, :3)";
                OracleCommand com = con.CreateCommand();
                com.CommandText = query;
                com.CommandType = CommandType.Text;
                //com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("2", OracleDbType.Varchar2, 50).Value = accountDB.USERNAME.ToString();
                com.Parameters.Add("3", OracleDbType.Varchar2, 50).Value = EnryptString(accountDB.PASSWORD.ToString());
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        //Method for Updating ItemDB record  
        public int Update(AccountDB accountDB)
        {
            int i;
            using (OracleConnection con = new OracleConnection(cs))
            {
                con.Open();
                string query = "Update TRANSACTION.ACCOUNT SET username=:2, password=:3 WHERE id=:1";
                OracleCommand com = con.CreateCommand();
                com.CommandText = query;
                com.CommandType = CommandType.Text;
                //com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("2", OracleDbType.Varchar2, 50).Value = accountDB.USERNAME.ToString();
                com.Parameters.Add("3", OracleDbType.Varchar2, 50).Value = EnryptString(accountDB.PASSWORD.ToString());
                com.Parameters.Add("1", OracleDbType.Int32, 10).Value = Convert.ToInt32(accountDB.ID);
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
                string query = "Delete from TRANSACTION.ACCOUNT WHERE id=:1";
                OracleCommand com = con.CreateCommand();
                com.CommandText = query;
                com.CommandType = CommandType.Text;
                //com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("1", OracleDbType.Int32, 10).Value = Convert.ToInt32(ID);
                i = com.ExecuteNonQuery();
            }
            return i;
        }        

        public string EnryptString(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
    }
}