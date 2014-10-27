using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;


namespace PhotoFun.Models
{
    public class PhotoFunBD
    {
        //private const string cs = "Data Source=G264-10\\SQLEXPRESS;Initial Catalog=tempdb;Integrated Security=True";
        private const string cs = "Data Source=G264-09\\SQLEXPRESS;Initial Catalog=tempdb;Integrated Security=True";
        //private const string cs = "Data Source=EQUIPE-01\\SQLEXPRESS ;Initial Catalog=tempdb;Integrated Security=True";
        public bool InsererUtil(RegisterModel rm)
        {
            using (var conn = new SqlConnection(cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scAjouter = new SqlCommand("Insert into Utilisateurs (IDUtil, MotPasse, CourrielUtil, PrenomUtil, NomUtil) values ('"
                        + rm.UserName + "', '" + rm.Password + "', '" + rm.Courriel + "', '" + rm.PrenomUtil + "', '" + rm.NomUtil + "');", conn);
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

        private string ReadSingleRow(IDataRecord record)
        {
            return String.Format("{0}", record[0]);
        }
    }
}
