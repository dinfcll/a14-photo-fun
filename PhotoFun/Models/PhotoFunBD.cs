using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;


namespace PhotoFun.Models
{
    public class PhotoFunBD
    {
        private const string cs = "Data Source=g264-11\\sqlexpress ;Initial Catalog=tempdb;Integrated Security=True";

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

        public bool MettreAJourLeCommentaireDeLaPhoto(string commentaire, string photo)
        {
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scModifier= new SqlCommand("Update Photos set Commentaire='"+commentaire+"' where Image='"+photo+"';", conn);
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

        public string ExtraireCommentaireSelonPhoto(string image)
        {
            using (var conn = new SqlConnection(cs))
            {
                string commentaire = "";
                try
                {
                    conn.Open();
                    var scExtraireCommentaireSelonPhoto = new SqlCommand("Select Commentaire from Photos where Image='" + image + "';", conn);
                    var sdr = scExtraireCommentaireSelonPhoto.ExecuteReader();
                    while (sdr.Read())
                    {
                        commentaire=ReadSingleRow(sdr);
                    }
                    sdr.Close();
                    conn.Close();
                }
                catch
                {
                }
                return commentaire;
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

        public bool DetruirePhotoSelonUtil(string util,string image)
        {
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scDetruirePhotoSelonUtil = new SqlCommand("DELETE FROM PHOTOS WHERE IDUtil='"+util+"' and Image='"+image+"';",conn);
                    scDetruirePhotoSelonUtil.ExecuteNonQuery();
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

        public string ExtraireUtilSelonPhoto(string image)
        {
            using (var conn = new SqlConnection(cs))
            {
                string nomUtil = "";
                try
                {
                    conn.Open();
                    var scExtraireUtilSelonPhoto = new SqlCommand("Select IDUtil from Photos where Image='" + image + "';", conn);
                    var sdr = scExtraireUtilSelonPhoto.ExecuteReader();
                    while (sdr.Read())
                    {
                        nomUtil=ReadSingleRow(sdr);
                    }
                    sdr.Close();
                    conn.Close();
                }
                catch
                {
                }
                return nomUtil;
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

        public int RetourneLeNombreDeJAimeSelonPhoto(string Photo)
        {
            using (var conn = new SqlConnection(cs))
            {
                string sJaime = "";
                int Jaime;
                try
                {
                    conn.Open();
                    var scRetourneLeNombreDeJAimeSelonPhoto = new SqlCommand("SELECT NbJaime FROM PHOTOS WHERE Image='" + Photo + "';", conn);
                    var sdr = scRetourneLeNombreDeJAimeSelonPhoto.ExecuteReader();
                    while (sdr.Read())
                    {
                        sJaime = ReadSingleRow(sdr);
                    }
                    sdr.Close();
                    conn.Close();
                    Jaime= Convert.ToInt32(sJaime);
                    return Jaime;
                }
                catch
                {
                    return 0;
                }
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

        public bool AjouterUnLike(string nomimage)
        {
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                string like="";
                int nblike;
                try
                {
                    conn.Open();
                    var scAjouterUnLike = new SqlCommand("SELECT NbJaime FROM PHOTOS WHERE Image='" + nomimage + "';", conn);
                    var sdr = scAjouterUnLike.ExecuteReader();
                    while (sdr.Read())
                    {
                       like=ReadSingleRow(sdr);
                    }
                    sdr.Close();
                    nblike = Convert.ToInt32(like);
                    nblike++;
                    scAjouterUnLike = new SqlCommand("UPDATE PHOTOS set NbJaime=" + nblike + " where Image='" + nomimage + "';", conn);
                    scAjouterUnLike.ExecuteNonQuery();
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

        public bool EnleveUnLike(string nomimage)
        {
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                string like = "";
                int nblike;
                try
                {
                    conn.Open();
                    var scEnleveUnLike = new SqlCommand("SELECT NbJaime FROM PHOTOS WHERE Image='" + nomimage + "';", conn);
                    var sdr = scEnleveUnLike.ExecuteReader();
                    while (sdr.Read())
                    {
                        like = ReadSingleRow(sdr);
                    }
                    sdr.Close();
                    nblike = Convert.ToInt32(like);
                    nblike--;
                    scEnleveUnLike = new SqlCommand("UPDATE PHOTOS set NbJaime=" + nblike + " where Image='" + nomimage + "';", conn);
                    scEnleveUnLike.ExecuteNonQuery();
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

        public bool InsererDonneesProfil(ProfilModel pm)
        {
            using(var conn=new SqlConnection(cs))
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

        public bool EnleveTousLesLiaisonsAvecLesUtils(string nomPhoto)
        {
            using(var conn=new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scEnleveLiaisonPhotoUtil = new SqlCommand("Delete FROM relUtilPhoto WHERE IdPhoto= (Select IdPhoto from Photos where Image='" + nomPhoto + "');", conn);
                    scEnleveLiaisonPhotoUtil.ExecuteNonQuery();
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
                        NbAbonnement=Convert.ToInt32(ReadSingleRow(sdr));
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

        public bool EnleveLiaisonPhotoUtil(string nomUtil, string nomPhoto)
        {
            using(var conn=new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scEnleveLiaisonPhotoUtil = new SqlCommand("Delete FROM relUtilPhoto WHERE IdUtil='"+nomUtil+"' and IdPhoto= (Select IdPhoto from Photos where Image='"+nomPhoto+"');", conn);
                    scEnleveLiaisonPhotoUtil.ExecuteNonQuery();
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
            int NbAbonnement=0;
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
                        NbAbonnement=Convert.ToInt32(ReadSingleRow(sdr));
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

        public bool VerifLiaisonPhotoUtil(string nomUtil, string nomPhoto)
        {
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    int nbrelation=0;
                    var scAjouterUnLike = new SqlCommand("SELECT count(*) FROM relUtilPhoto WHERE IdUtil='" + nomUtil + "' and IdPhoto= (Select IdPhoto from Photos where Image='" + nomPhoto + "');", conn);
                    var sdr = scAjouterUnLike.ExecuteReader();
                    while (sdr.Read())
                    {
                         nbrelation=Convert.ToInt32(ReadSingleRow(sdr));
                    }
                    sdr.Close();
                    conn.Close();
                    if (nbrelation > 0)
                    {
                        resultat = false;
                    }
                    else
                    {
                        resultat = true;
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

        public bool AjoutRelationUtilPhoto(string nomUtil, string nomPhoto)
        {
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scAjouterRelation = new SqlCommand("Insert into relUtilPhoto (IDUtil,IdPhoto) values('" + nomUtil
                        + "',(Select IdPhoto from Photos where Image='" + nomPhoto + "'));", conn);
                    scAjouterRelation.ExecuteNonQuery();
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
