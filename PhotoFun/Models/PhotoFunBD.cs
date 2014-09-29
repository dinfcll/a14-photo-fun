using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace PhotoFun.Models
{
    public class PhotoFunBD
    {
        
        public bool InsererUtil(RegisterModel model)
        {
            bool resultat;
            try
            {
                SqlConnection conn = new SqlConnection("ConnectionMaBD");
                SqlCommand scAjouter = new SqlCommand("Insert into Utilisateur values(NumUtil.NextVal" + model.UserName + ", " + model.Password + ", "
                    + model.Courriel + ", " + model.PrenomUtil + ", " + model.NomUtil + ");");
                scAjouter.Connection.Open();
                scAjouter.ExecuteNonQuery();
                resultat = true;

            }
            catch
            {
                resultat = false;
            }
            return resultat;
        }
    }
}