using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace PhotoFun.Models
{
    public class RequetePhotoBD
    {
        PhotoFunBD photofunbd = new PhotoFunBD();
        private string cs;

        public RequetePhotoBD()
        {
            cs = photofunbd.ConnexionString;
        }

        public bool MettreAJourLeCommentaireDeLaPhoto(string commentaire, string photo)
        {
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scModifier = new SqlCommand("Update Photos set Commentaire='" + commentaire + "' where Image='" + photo + "';", conn);
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
                        commentaire = photofunbd.ReadSingleRow(sdr);
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

        public bool EnregistrerPhoto(PhotoModels pm)
        {
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scEnregistrerPhoto = new SqlCommand("Insert into Photos (Categorie, Image, IDUtil, Commentaire) values ('" + pm.Categorie + "', '" + pm.image + "', '" + pm.util + "', '" + pm.Commentaires + "');", conn);
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

        public bool DetruirePhotoSelonUtil(string util, string image)
        {
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scDetruirePhotoSelonUtil = new SqlCommand("DELETE FROM PHOTOS WHERE IDUtil='" + util + "' and Image='" + image + "';", conn);
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

        public bool ExtrairePhotoSelonUtil(string NomUtil, out List<string> lstimage)
        {
            lstimage = new List<string>();
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
                        lstimage.Add(photofunbd.ReadSingleRow(sdr));
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
                        nomUtil = photofunbd.ReadSingleRow(sdr);
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
                        lstimage.Add(photofunbd.ReadSingleRow(sdr));
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
                        sJaime = photofunbd.ReadSingleRow(sdr);
                    }
                    sdr.Close();
                    conn.Close();
                    Jaime = Convert.ToInt32(sJaime);
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
                    var scExtraireDernieresPhotos = new SqlCommand("SELECT IMAGE FROM PHOTOS WHERE IdPhoto>(SELECT MAX(IdPhoto)-" + nbimage + " FROM PHOTOS);", conn);
                    var sdr = scExtraireDernieresPhotos.ExecuteReader();
                    while (sdr.Read())
                    {
                        lstimage.Add(photofunbd.ReadSingleRow(sdr));
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

        public bool AjouterUnLike(string nomimage)
        {
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                string like = "";
                int nblike;
                try
                {
                    conn.Open();
                    var scAjouterUnLike = new SqlCommand("SELECT NbJaime FROM PHOTOS WHERE Image='" + nomimage + "';", conn);
                    var sdr = scAjouterUnLike.ExecuteReader();
                    while (sdr.Read())
                    {
                        like = photofunbd.ReadSingleRow(sdr);
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
                        like = photofunbd.ReadSingleRow(sdr);
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
    }
}
