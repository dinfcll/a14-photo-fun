﻿using System;
using System.Data;

namespace PhotoFun.Models
{
    public class PhotoFunBd
    {
        private const string Cs = "Data Source=EQUIPE-01\\SQLEXPRESS ;Initial Catalog=DBPhotoFun;Integrated Security=True";

        public string ConnexionString
        {
            get
            {
                return Cs;
            }
        }

        public string ReadSingleRow(IDataRecord record)
        {
            return String.Format("{0}", record[0]);
        }
    }
}
