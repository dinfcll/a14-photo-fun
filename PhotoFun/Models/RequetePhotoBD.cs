using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PhotoFun.Models
{
    public class RequetePhotoBd
    {
        readonly PhotoFunBd _photofunbd = new PhotoFunBd();
        private readonly string _cs;

        public RequetePhotoBd()
        {
            _cs = _photofunbd.ConnexionString;
        }

        public bool MettreAJourLeCommentaireDeLaPhoto(string commentaire, string photo)
        {
            using (var conn = new SqlConnection(_cs))
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
            using (var conn = new SqlConnection(_cs))
            {
                string commentaire = "";
                try
                {
                    conn.Open();
                    var scExtraireCommentaireSelonPhoto = new SqlCommand("Select Commentaire from Photos where Image='" + image + "';", conn);
                    var sdr = scExtraireCommentaireSelonPhoto.ExecuteReader();
                    while (sdr.Read())
                    {
                        commentaire = _photofunbd.ReadSingleRow(sdr);
                    }
                    sdr.Close();
                    conn.Close();
                }
                catch
                {
                    commentaire = "Une erreur s'est produite.";
                }
                return commentaire;
            }
        }

        public bool EnregistrerPhoto(PhotoModels pm)
        {
            using (var conn = new SqlConnection(_cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scEnregistrerPhoto = new SqlCommand("Insert into Photos (Categorie, Image, IDUtil, Commentaire) values ('" + pm.Categorie + "', '" + pm.Image + "', '" + pm.Util + "', '" + pm.Commentaires + "');", conn);
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
            using (var conn = new SqlConnection(_cs))
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

        public bool ExtrairePhotoSelonUtil(string nomUtil, out List<string> lstimage)
        {
            lstimage = new List<string>();
            using (var conn = new SqlConnection(_cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scExtrairePhotoSelonUtil = new SqlCommand("Select Image from Photos where IDUtil='" + nomUtil + "';", conn);
                    var sdr = scExtrairePhotoSelonUtil.ExecuteReader();
                    while (sdr.Read())
                    {
                        lstimage.Add(_photofunbd.ReadSingleRow(sdr));
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
            using (var conn = new SqlConnection(_cs))
            {
                string nomUtil = "";
                try
                {
                    conn.Open();
                    var scExtraireUtilSelonPhoto = new SqlCommand("Select IDUtil from Photos where Image='" + image + "';", conn);
                    var sdr = scExtraireUtilSelonPhoto.ExecuteReader();
                    while (sdr.Read())
                    {
                        nomUtil = _photofunbd.ReadSingleRow(sdr);
                    }
                    sdr.Close();
                    conn.Close();
                }
                catch
                {
                    nomUtil = "Une erreur s'est produite. Nom d'utilisateur non disponible";
                }
                return nomUtil;
            }
        }

        public bool ExtrairePhotoSelonCategorie(string categorie, out List<string> lstimage)
        {
            lstimage = new List<string>();
            using (var conn = new SqlConnection(_cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scExtrairePhotoSelonCategorie = new SqlCommand("Select Image from Photos where Categorie='" + categorie + "';", conn);
                    var sdr = scExtrairePhotoSelonCategorie.ExecuteReader();
                    while (sdr.Read())
                    {
                        lstimage.Add(_photofunbd.ReadSingleRow(sdr));
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

        public int RetourneLeNombreDeJAimeSelonPhoto(string photo)
        {
            using (var conn = new SqlConnection(_cs))
            {
                string sJaime = "";
                try
                {
                    conn.Open();
                    var scRetourneLeNombreDeJAimeSelonPhoto = new SqlCommand("SELECT NbJaime FROM PHOTOS WHERE Image='" + photo + "';", conn);
                    var sdr = scRetourneLeNombreDeJAimeSelonPhoto.ExecuteReader();
                    while (sdr.Read())
                    {
                        sJaime = _photofunbd.ReadSingleRow(sdr);
                    }
                    sdr.Close();
                    conn.Close();
                    var jaime = Convert.ToInt32(sJaime);
                    return jaime;
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
            using (var conn = new SqlConnection(_cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scExtraireDernieresPhotos = new SqlCommand("SELECT IMAGE FROM PHOTOS WHERE IdPhoto>(SELECT MAX(IdPhoto)-" + nbimage + " FROM PHOTOS);", conn);
                    var sdr = scExtraireDernieresPhotos.ExecuteReader();
                    while (sdr.Read())
                    {
                        lstimage.Add(_photofunbd.ReadSingleRow(sdr));
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
            using (var conn = new SqlConnection(_cs))
            {
                bool resultat;
                string like = "";
                try
                {
                    conn.Open();
                    var scAjouterUnLike = new SqlCommand("SELECT NbJaime FROM PHOTOS WHERE Image='" + nomimage + "';", conn);
                    var sdr = scAjouterUnLike.ExecuteReader();
                    while (sdr.Read())
                    {
                        like = _photofunbd.ReadSingleRow(sdr);
                    }
                    sdr.Close();
                    var nblike = Convert.ToInt32(like);
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
            using (var conn = new SqlConnection(_cs))
            {
                bool resultat;
                string like = "";
                try
                {
                    conn.Open();
                    var scEnleveUnLike = new SqlCommand("SELECT NbJaime FROM PHOTOS WHERE Image='" + nomimage + "';", conn);
                    var sdr = scEnleveUnLike.ExecuteReader();
                    while (sdr.Read())
                    {
                        like = _photofunbd.ReadSingleRow(sdr);
                    }
                    sdr.Close();
                    var nblike = Convert.ToInt32(like);
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
