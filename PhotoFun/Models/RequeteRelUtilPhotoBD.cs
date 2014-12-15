using System;
using System.Data.SqlClient;

namespace PhotoFun.Models
{
    public class RequeteRelUtilPhotoBd
    {
        readonly PhotoFunBd _photofunbd = new PhotoFunBd();
        private readonly string _cs;

        public RequeteRelUtilPhotoBd()
        {
            _cs=_photofunbd.ConnexionString;
        }

        public bool EnleveTousLesLiaisonsAvecLesUtils(string nomPhoto)
        {
            using (var conn = new SqlConnection(_cs))
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
            using (var conn = new SqlConnection(_cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    var scEnleveLiaisonPhotoUtil = new SqlCommand("Delete FROM relUtilPhoto WHERE IdUtil='" + nomUtil + "' and IdPhoto= (Select IdPhoto from Photos where Image='" + nomPhoto + "');", conn);
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
            using (var conn = new SqlConnection(_cs))
            {
                bool resultat;
                try
                {
                    conn.Open();
                    int nbrelation = 0;
                    var scAjouterUnLike = new SqlCommand("SELECT count(*) FROM relUtilPhoto WHERE IdUtil='" + nomUtil + "' and IdPhoto= (Select IdPhoto from Photos where Image='" + nomPhoto + "');", conn);
                    var sdr = scAjouterUnLike.ExecuteReader();
                    while (sdr.Read())
                    {
                        nbrelation = Convert.ToInt32(_photofunbd.ReadSingleRow(sdr));
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
            using (var conn = new SqlConnection(_cs))
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
    }
}
