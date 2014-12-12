using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;


namespace PhotoFun.Models
{
    public class PhotoFunBD
    {
        private const string cs = "Data Source=G264-11\\SQLEXPRESS ;Initial Catalog=tempdb;Integrated Security=True";

        public string ConnexionString
        {
            get
            {
                return cs;
            }
        }

        public string ReadSingleRow(IDataRecord record)
        {
            return String.Format("{0}", record[0]);
        }
    }
}
