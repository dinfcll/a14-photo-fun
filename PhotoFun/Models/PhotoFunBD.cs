using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace PhotoFun.Models
{
    public class PhotoFunBD
    {
        SqlConnection conn = new SqlConnection("ConnectionMaBD");
        public bool InsererUtil(RegisterModel model)
        {
            bool resultat;
            try
            {
                SqlCommand scAjouter = new SqlCommand("Insert into Utilisateur values(NumUtil.NextVal" + model.UserName + ", " + model.Password + ", "
                    +model.Courriel + ", " + model.PrenomUtil + ", " + model.NomUtil + ");");
                scAjouter.BeginExecuteNonQuery();
                resultat=true;
            }
            catch
            {
                resultat=false;
            }
            return resultat;
        }
    }
}