using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;


namespace PhotoFun.Models
{
    public class RequeteUtilBD
    {
        PhotoFunBd photofunbd = new PhotoFunBd();
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

        public bool ExtraireCourrielSelonUtil(string util, out string courriel)
        {
            using (var conn = new SqlConnection(cs))
            {
                courriel="";
                bool resultat;
                try
                {
                    conn.Open();
                    var scExtraireCourrielSelonUtil = new SqlCommand("Select CourrielUtil from Utilisateurs where IDUtil='" + util + "';", conn);
                    var sdr = scExtraireCourrielSelonUtil.ExecuteReader();
                    while (sdr.Read())
                    {
                        courriel=photofunbd.ReadSingleRow(sdr);
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

        public bool ExtraireNomSelonUtil(string util, out string Nom)
        {
            using (var conn = new SqlConnection(cs))
            {
                Nom = "";
                bool resultat;
                try
                {
                    conn.Open();
                    var scExtraireNomSelonUtil = new SqlCommand("Select NomUtil from Utilisateurs where IDUtil='" + util + "';", conn);
                    var sdr = scExtraireNomSelonUtil.ExecuteReader();
                    while (sdr.Read())
                    {
                        Nom = photofunbd.ReadSingleRow(sdr);
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

        public bool ExtrairePrenomSelonUtil(string util, out string Prenom)
        {
            using (var conn = new SqlConnection(cs))
            {
                Prenom = "";
                bool resultat;
                try
                {
                    conn.Open();
                    var scExtrairePrenomSelonUtil = new SqlCommand("Select PrenomUtil from Utilisateurs where IDUtil='" + util + "';", conn);
                    var sdr = scExtrairePrenomSelonUtil.ExecuteReader();
                    while (sdr.Read())
                    {
                        Prenom = photofunbd.ReadSingleRow(sdr);
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

        public bool MettreAJourPhotoProfil(string image, string nomutil)
        {
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scMettreAJourPhotoProfil = new SqlCommand("Update Utilisateurs set PhotoProfil='" + image + "' where IDUtil='" + nomutil + "';", conn);
                    scMettreAJourPhotoProfil.ExecuteNonQuery();
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

        public string ExtrairePhotoProfil(string nomutil)
        {
            using (var conn = new SqlConnection(cs))
            {
                string image = "";
                try
                {
                    conn.Open();
                    var scExtrairePhotoProfil = new SqlCommand("Select PhotoProfil from Utilisateurs where IDUtil='" + nomutil + "';", conn);
                    var sdr = scExtrairePhotoProfil.ExecuteReader();
                    while (sdr.Read())
                    {
                        image=photofunbd.ReadSingleRow(sdr);
                    }
                    sdr.Close();
                    conn.Close();
                }
                catch
                {
                }
                return image;
            }
        }
    }
}
