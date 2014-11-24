using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace PhotoFun.Models
{
    public class RequeteProfilUtilBD
    {
        PhotoFunBD photofunbd = new PhotoFunBD();
        private string cs;

        public RequeteProfilUtilBD()
        {
            cs = photofunbd.ConnexionString;
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
    }
}
