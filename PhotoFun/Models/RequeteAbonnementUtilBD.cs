using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace PhotoFun.Models
{
    public class RequeteAbonnementUtilBD
    {
        PhotoFunBd photofunbd = new PhotoFunBd();
        private string cs;

        public RequeteAbonnementUtilBD()
        {
            cs = photofunbd.ConnexionString;
        }

        public bool AbonnerUtil(string NomUtilCourant, string NomUtilAbonner)
        {
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    if (VerifAbonnement(NomUtilAbonner, NomUtilCourant) == false)
                    {
                        var scEnregistrerAbonnement = new SqlCommand("Insert into AbonnementUtil (IdUtilConnecter, IdUtilAbonner) values ('" + NomUtilCourant + "', '" + NomUtilAbonner + "');", conn);
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

        public bool CompteNbAbonnement(ProfilModel pm, out int NbAbonnement)
        {
            NbAbonnement = 0;
            using (var conn = new SqlConnection(cs))
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
                        NbAbonnement = Convert.ToInt32(photofunbd.ReadSingleRow(sdr));
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

        public bool VerifAbonnement(string pm, string UtilConnecter)
        {
            int NbAbonnement = 0;
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scNbAbonnement = new SqlCommand("Select count(*) from AbonnementUtil where IdUtilAbonner='"
                        + pm + "' and IdUtilConnecter='" + UtilConnecter + "'", conn);
                    var sdr = scNbAbonnement.ExecuteReader();
                    while (sdr.Read())
                    {
                        NbAbonnement = Convert.ToInt32(photofunbd.ReadSingleRow(sdr));
                    }
                    sdr.Close();
                    conn.Close();
                    if (NbAbonnement > 0)
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

        public bool SupprimerRelAbonnement(string UtilAbonner, string UtilConnecter)
        {
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scSupprimer = new SqlCommand("Delete from AbonnementUtil where IdUtilAbonner='"
                        + UtilAbonner + "' and IdUtilConnecter='" + UtilConnecter + "'", conn);
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

        public bool ExtraireLesAbonnementsSelonUtil(string NomUtil, out List<string> MesAbonnements)
        {
            using (var conn = new SqlConnection(cs))
            {
                MesAbonnements = new List<string>();
                bool resultat;
                try
                {
                    conn.Open();
                    var scNbAbonnement = new SqlCommand("Select IdUtilAbonner from AbonnementUtil where IdUtilConnecter='"
                        + NomUtil + "'", conn);
                    var sdr = scNbAbonnement.ExecuteReader();
                    while (sdr.Read())
                    {
                        MesAbonnements.Add(photofunbd.ReadSingleRow(sdr));
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
