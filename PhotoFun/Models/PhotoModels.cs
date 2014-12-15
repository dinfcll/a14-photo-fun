using System;
using System.Globalization;

namespace PhotoFun.Models
{
    public class PhotoModels
    {
        public string Categorie { get; set; }

        public string Image { get; set; }

        public string Util { get; set; }

        public string Commentaires { get; set; }

        public string IdUniqueNomPhoto
        {
            get
            {
                var s = string.Format("{0}{1}{2}{3}{4}{5}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour.ToString(CultureInfo.InvariantCulture), DateTime.Now.Minute, DateTime.Now.Second);
                return s;
            }
        }
        public PhotoModels()
        {
            Categorie = null;
            Util = null;
            Image = null;
            Commentaires = null;
        }
    }
}
