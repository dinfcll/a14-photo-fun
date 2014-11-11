using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;


namespace PhotoFun.Models
{
    public class PhotoFunBD
    {
        private const string cs = "Data Source=DOMINIC_PC\\SQLEXPRESS ;Initial Catalog=tempdb;Integrated Security=True";
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

        public bool ExtrairePhotoSelonCategorie(string Categorie, out List<string> lstimage)
        {
            lstimage = new List<string>();
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scExtrairePhotoSelonUtil = new SqlCommand("Select Image from Photos where Categorie='" + Categorie + "';", conn);
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
