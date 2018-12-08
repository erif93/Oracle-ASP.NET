using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SPDemo.Models
{
    public class TransactionDB
    {
        public int ID { get; set; }
        public int ID_ITEM { get; set; }
        public int ID_TRANSAKSI { get; set; }
        public int QTY { get; set; }
        public DateTime CREATEDATE { get; set; }

        public TransactionDB() { }

        string cs = ConfigurationManager.ConnectionStrings["OracleDBContext"].ConnectionString;


    }
}