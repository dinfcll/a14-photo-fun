using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;


namespace PhotoFun.Models
{
    public class RequeteUtilBD
    {
        PhotoFunBD photofunbd = new PhotoFunBD();
        private string cs;

        public RequeteUtilBD()
        {
            cs = photofunbd.ConnexionString;
        }

        public bool InsererUtil(RegisterModel rm)
        {
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scAjouter = new SqlCommand("Insert into Utilisateurs (IDUtil, CourrielUtil, PrenomUtil, NomUtil) values ('"
                        + rm.UserName + "', '" + rm.Courriel + "', '" + rm.PrenomUtil + "', '" + rm.NomUtil + "');", conn);
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
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scExtraire = new SqlCommand("Select * from Utilisateurs where IDUtil='" + IDUtil + "';", conn);
                    scExtraire.ExecuteNonQuery();
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

        public bool ExtraireUtil(string IDUtil, out List<string> NomUtil)
        {
            NomUtil = new List<string>();
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scExtraire = new SqlCommand("Select * from Utilisateurs where IDUtil='" + IDUtil + "';", conn);
                    var sdr = scExtraire.ExecuteReader();
                    while (sdr.Read())
                    {
                        NomUtil.Add(photofunbd.ReadSingleRow(sdr));
                    }
                    sdr.Close();
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

        public bool ExtraireUtilAvecPourcent(string IDUtil, out List<string> NomUtil)
        {
            NomUtil = new List<string>();
            IDUtil = "%" + IDUtil + "%";
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scExtraire = new SqlCommand("Select * from Utilisateurs where IDUtil like'" + IDUtil + "';", conn);
                    var sdr = scExtraire.ExecuteReader();
                    while (sdr.Read())
                    {
                        NomUtil.Add(photofunbd.ReadSingleRow(sdr));
                    }
                    sdr.Close();
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
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scModifier = new SqlCommand("Update Utilisateurs set MotPasse='" + lpm.NewPassword + "' where IDUtil='" + usager + "';", conn);
                    scModifier.ExecuteNonQuery();
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
