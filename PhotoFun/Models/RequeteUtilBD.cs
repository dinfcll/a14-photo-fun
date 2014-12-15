using System.Collections.Generic;
using System.Data.SqlClient;

namespace PhotoFun.Models
{
    public class RequeteUtilBd
    {
        readonly PhotoFunBd _photofunbd = new PhotoFunBd();
        private readonly string _cs;

        public RequeteUtilBd()
        {
            _cs = _photofunbd.ConnexionString;
        }

        public bool InsererUtil(RegisterModel rm)
        {
            using (var conn = new SqlConnection(_cs))
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
            using (var conn = new SqlConnection(_cs))
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
                        courriel=_photofunbd.ReadSingleRow(sdr);
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

        public bool ExtraireNomSelonUtil(string util, out string nom)
        {
            using (var conn = new SqlConnection(_cs))
            {
                nom = "";
                bool resultat;
                try
                {
                    conn.Open();
                    var scExtraireNomSelonUtil = new SqlCommand("Select NomUtil from Utilisateurs where IDUtil='" + util + "';", conn);
                    var sdr = scExtraireNomSelonUtil.ExecuteReader();
                    while (sdr.Read())
                    {
                        nom = _photofunbd.ReadSingleRow(sdr);
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

        public bool ExtrairePrenomSelonUtil(string util, out string prenom)
        {
            using (var conn = new SqlConnection(_cs))
            {
                prenom = "";
                bool resultat;
                try
                {
                    conn.Open();
                    var scExtrairePrenomSelonUtil = new SqlCommand("Select PrenomUtil from Utilisateurs where IDUtil='" + util + "';", conn);
                    var sdr = scExtrairePrenomSelonUtil.ExecuteReader();
                    while (sdr.Read())
                    {
                        prenom = _photofunbd.ReadSingleRow(sdr);
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

        public bool ExtraireUtil(string idUtil)
        {
            using (var conn = new SqlConnection(_cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scExtraire = new SqlCommand("Select * from Utilisateurs where IDUtil='" + idUtil + "';", conn);
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

        public bool ExtraireUtil(string idUtil, out List<string> nomUtil)
        {
            nomUtil = new List<string>();
            using (var conn = new SqlConnection(_cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scExtraire = new SqlCommand("Select * from Utilisateurs where IDUtil='" + idUtil + "';", conn);
                    var sdr = scExtraire.ExecuteReader();
                    while (sdr.Read())
                    {
                        nomUtil.Add(_photofunbd.ReadSingleRow(sdr));
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

        public bool ExtraireUtilAvecPourcent(string idUtil, out List<string> nomUtil)
        {
            nomUtil = new List<string>();
            idUtil = "%" + idUtil + "%";
            using (var conn = new SqlConnection(_cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scExtraire = new SqlCommand("Select * from Utilisateurs where IDUtil like'" + idUtil + "';", conn);
                    var sdr = scExtraire.ExecuteReader();
                    while (sdr.Read())
                    {
                        nomUtil.Add(_photofunbd.ReadSingleRow(sdr));
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
            using (var conn = new SqlConnection(_cs))
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
            using (var conn = new SqlConnection(_cs))
            {
                string image = "";
                try
                {
                    conn.Open();
                    var scExtrairePhotoProfil = new SqlCommand("Select PhotoProfil from Utilisateurs where IDUtil='" + nomutil + "';", conn);
                    var sdr = scExtrairePhotoProfil.ExecuteReader();
                    while (sdr.Read())
                    {
                        image=_photofunbd.ReadSingleRow(sdr);
                    }
                    sdr.Close();
                    conn.Close();
                }
                catch
                {
                    image = null;
                }
                return image;
            }
        }
    }
}
