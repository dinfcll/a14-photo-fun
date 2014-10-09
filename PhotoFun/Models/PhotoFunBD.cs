using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace PhotoFun.Models
{
    public class PhotoFunBD
    {

        public bool InsererUtil(RegisterModel rm)
        {
            bool resultat;
            string cs = "Data Source=G264-09\\SQLEXPRESS;Initial Catalog=tempdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                try
                {
                    conn.Open();
                    SqlCommand scAjouter = new SqlCommand("Insert into Utilisateurs (IDUtil, MotPasse, CourrielUtil, PrenomUtil, NomUtil) values ('"
                        + rm.UserName + "', '" + rm.Password + "', '" + rm.Courriel + "', '" + rm.PrenomUtil + "', '" + rm.NomUtil + "');", conn);
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

        public bool ExtraireUtil(string IDUtil)
        {
            bool resultat;
            string cs = "Data Source=G264-09\\SQLEXPRESS;Initial Catalog=tempdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                try
                {
                    conn.Open();
                    SqlCommand scAjouter = new SqlCommand("Select * from Utilisateurs where IDUtil='"+IDUtil+"';", conn);
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

        public bool MettreAJourUtil(LocalPasswordModel lpm, string usager)
        {
            bool resultat;
            string cs = "Data Source=G264-09\\SQLEXPRESS;Initial Catalog=tempdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                try
                {
                    conn.Open();
                    SqlCommand scAjouter = new SqlCommand("Update Utilisateurs set MotPasse='"+lpm.NewPassword+"' where IDUtil='"+usager+"';", conn);
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

        public bool EnregistrerPhoto(string path)
        {
            bool resultat;
            string cs = "Data Source=G264-09\\SQLEXPRESS;Initial Catalog=tempdb;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                try
                {
                    conn.Open();
                    SqlCommand scAjouter = new SqlCommand("Insert into Photo values IdPhoto.Nextval, ...");
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