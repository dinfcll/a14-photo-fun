using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PhotoFun.Models
{
    public class RequeteAbonnementUtilBd
    {
        readonly PhotoFunBd _photofunbd = new PhotoFunBd();
        private readonly string _cs;

        public RequeteAbonnementUtilBd()
        {
            _cs = _photofunbd.ConnexionString;
        }

        public bool AbonnerUtil(string nomUtilCourant, string nomUtilAbonner)
        {
            using (var conn = new SqlConnection(_cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    if (VerifAbonnement(nomUtilAbonner, nomUtilCourant) == false)
                    {
                        var scEnregistrerAbonnement = new SqlCommand("Insert into AbonnementUtil (IdUtilConnecter, IdUtilAbonner) values ('" + nomUtilCourant + "', '" + nomUtilAbonner + "');", conn);
                        scEnregistrerAbonnement.ExecuteNonQuery();
                        conn.Close();
                        resultat = true;
                    }
                    else
                    {
                        resultat = false;
                    }
                }
                catch
                {
                    resultat = false;
                }
                return resultat;
            }
        }

        public bool CompteNbAbonnement(ProfilModel pm, out int nbAbonnement)
        {
            nbAbonnement = 0;
            using (var conn = new SqlConnection(_cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scNbAbonnement = new SqlCommand("Select count(IdUtilConnecter) from AbonnementUtil where IdUtilAbonner='"
                        + pm.IdUtilRechercher + "'", conn);
                    var sdr = scNbAbonnement.ExecuteReader();
                    while (sdr.Read())
                    {
                        nbAbonnement = Convert.ToInt32(_photofunbd.ReadSingleRow(sdr));
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

        public bool VerifAbonnement(string pm, string utilConnecter)
        {
            int nbAbonnement = 0;
            using (var conn = new SqlConnection(_cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scNbAbonnement = new SqlCommand("Select count(*) from AbonnementUtil where IdUtilAbonner='"
                        + pm + "' and IdUtilConnecter='" + utilConnecter + "'", conn);
                    var sdr = scNbAbonnement.ExecuteReader();
                    while (sdr.Read())
                    {
                        nbAbonnement = Convert.ToInt32(_photofunbd.ReadSingleRow(sdr));
                    }
                    sdr.Close();
                    conn.Close();
                    if (nbAbonnement > 0)
                    {
                        resultat = true;
                    }
                    else
                    {
                        resultat = false;
                    }
                }
                catch
                {
                    resultat = false;
                }
                return resultat;
            }
        }

        public bool SupprimerRelAbonnement(string utilAbonner, string utilConnecter)
        {
            using (var conn = new SqlConnection(_cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scSupprimer = new SqlCommand("Delete from AbonnementUtil where IdUtilAbonner='"
                        + utilAbonner + "' and IdUtilConnecter='" + utilConnecter + "'", conn);
                    scSupprimer.ExecuteNonQuery();
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

        public bool ExtraireLesAbonnementsSelonUtil(string nomUtil, out List<string> mesAbonnements)
        {
            using (var conn = new SqlConnection(_cs))
            {
                mesAbonnements = new List<string>();
                bool resultat;
                try
                {
                    conn.Open();
                    var scNbAbonnement = new SqlCommand("Select IdUtilAbonner from AbonnementUtil where IdUtilConnecter='"
                        + nomUtil + "'", conn);
                    var sdr = scNbAbonnement.ExecuteReader();
                    while (sdr.Read())
                    {
                        mesAbonnements.Add(_photofunbd.ReadSingleRow(sdr));
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
    }
}
