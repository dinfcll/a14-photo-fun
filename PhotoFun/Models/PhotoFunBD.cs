using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;


namespace PhotoFun.Models
{
    public class PhotoFunBD
    {
        private const string cs = "Data Source=G264-09\\SQLEXPRESS ;Initial Catalog=tempdb;Integrated Security=True";
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
                    var scExtraire = new SqlCommand("Select * from Utilisateurs where IDUtil='"+IDUtil+"';", conn);
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
                        NomUtil.Add(ReadSingleRow(sdr));
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
                    var scModifier= new SqlCommand("Update Utilisateurs set MotPasse='"+lpm.NewPassword+"' where IDUtil='"+usager+"';", conn);
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

        public bool EnregistrerPhoto(PhotoModels pm)
        {
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scEnregistrerPhoto = new SqlCommand("Insert into Photos (Categorie, Image, IDUtil, Commentaire) values ('" + pm.Categorie + "', '" + pm.image + "', '" + pm.util + "', '"+pm.Commentaires+"');", conn);
                    scEnregistrerPhoto.ExecuteNonQuery();
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

        public bool ExtrairePhotoSelonUtil(string NomUtil,out List<string> lstimage)
        {
            lstimage= new List<string>();
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scExtrairePhotoSelonUtil = new SqlCommand("Select Image from Photos where IDUtil='" + NomUtil + "';", conn);
                    var sdr = scExtrairePhotoSelonUtil.ExecuteReader();
                    while (sdr.Read())
                    {
                        lstimage.Add(ReadSingleRow(sdr));
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

        public bool ExtrairePhotoSelonCategorie(string Categorie, out List<string> lstimage)
        {
            lstimage = new List<string>();
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scExtrairePhotoSelonCategorie = new SqlCommand("Select Image from Photos where Categorie='" + Categorie + "';", conn);
                    var sdr = scExtrairePhotoSelonCategorie.ExecuteReader();
                    while (sdr.Read())
                    {
                        lstimage.Add(ReadSingleRow(sdr));
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

        public bool ExtraireDernieresPhotos(int nbimage, out List<string> lstimage)
        {
            lstimage = new List<string>();
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scExtraireDernieresPhotos = new SqlCommand("SELECT IMAGE FROM PHOTOS WHERE IdPhoto>(SELECT MAX(IdPhoto)-"+nbimage+" FROM PHOTOS);",conn);
                    var sdr = scExtraireDernieresPhotos.ExecuteReader();
                    while(sdr.Read())
                    {
                        lstimage.Add(ReadSingleRow(sdr));
                    }
                    sdr.Close();
                    conn.Close();
                    resultat=true;
                }
                catch
                {
                    resultat = false;
                }
                return resultat;
            }
        }

        public bool AbonnerUtil(string NomUtilCourant, string NomUtilAbonner)
        {
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    if (VerifAbonnement(NomUtilAbonner, NomUtilCourant)==false)
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

        public bool InsererDonneesProfil(ProfilModel pm)
        {
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scAjouterProfil = new SqlCommand("Insert into ProfilUtil ( IDUtilRechercher, nbAbonnement) values ('"
                        + pm.IdUtilRechercher + "', '" + pm.NbAbonnement + "');", conn);
                    scAjouterProfil.ExecuteNonQuery();
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

        public bool CompteNbAbonnement(ProfilModel pm, out List<string> lstNbAbonnement)
        {
            lstNbAbonnement = new List<string>();
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
                        lstNbAbonnement.Add(ReadSingleRow(sdr));
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
            var lstNbAbonnement = new List<string>();
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scNbAbonnement = new SqlCommand("Select count(*) from AbonnementUtil where IdUtilAbonner='"
                        + pm + "' and IdUtilConnecter='"+UtilConnecter+"'", conn);
                    var sdr = scNbAbonnement.ExecuteReader();
                    while (sdr.Read())
                    {
                        lstNbAbonnement.Add(ReadSingleRow(sdr));
                    }
                    sdr.Close();
                    conn.Close();
                    if (Convert.ToInt32(lstNbAbonnement[0]) > 0)
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
        private string ReadSingleRow(IDataRecord record)
        {
            return String.Format("{0}", record[0]);
        }
    }
}
