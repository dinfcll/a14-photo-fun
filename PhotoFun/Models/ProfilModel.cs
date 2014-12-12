namespace PhotoFun.Models
{
    public class ProfilModel
    {
        public ProfilModel()
        {
            IdUtilRechercher = null;
            NbAbonnement = 0;
            Abonner = false;
            Courriel = null;
            NomUtil = null;
            PrenomUtil = null;
        }

        
        public string Courriel { get; set; }

        public string NomUtil { get; set; }

        public string PrenomUtil { get; set; }

        public string IdUtilRechercher { get; set; }

        public int NbAbonnement { get; set; }

        public bool Abonner { get; set; }
    }
}
