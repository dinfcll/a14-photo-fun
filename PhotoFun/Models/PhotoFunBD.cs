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
                    SqlCommand scAjouter = new SqlCommand("Insert into Utilisateur (NumUtil,IDUtil,MotPasse,CourrielUtil,PrenomUtil,NomUtil) values (100,'"
                        + model.UserName + "', '" + model.Password + "', '" + model.Courriel + "', '" + model.PrenomUtil + "', '" + model.NomUtil + "');", conn);
                    scAjouter.Parameters.AddWithValue("IDUtil", model.UserName);
                    scAjouter.Parameters.AddWithValue("MotPasse", model.Password);
                    scAjouter.Parameters.AddWithValue("CourrielUtil", model.Courriel);
                    scAjouter.Parameters.AddWithValue("PrenomUtil", model.PrenomUtil);
                    scAjouter.Parameters.AddWithValue("NomUtil", model.NomUtil);
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