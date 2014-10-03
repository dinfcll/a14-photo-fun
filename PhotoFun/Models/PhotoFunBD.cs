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
            string cs = "Data Source=G264-09\\SQLEXPRESS;Initial Catalog=tempdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                try
                {
                    conn.Open();
                    SqlCommand scAjouter = new SqlCommand("Insert into Utilisateur (IDUtil,MotPasse,CourrielUtil,PrenomUtil,NomUtil) values ('"
                        + model.UserName + "', '" + model.Password + "', '" + model.Courriel + "', '" + model.PrenomUtil + "', '" + model.NomUtil + "');", conn);
                    scAjouter.ExecuteNonQuery();
                    conn.Close();
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
}