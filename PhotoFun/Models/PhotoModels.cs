using System;
using System.Globalization;

namespace PhotoFun.Models
{
    public class PhotoModels
    {
        private string m_cat;
        private string m_img;
        private string m_idutil;
        private string m_Commentaires;

        public string Categorie 
        {
            get
            {
                return m_cat;
            }
            set
            {
                m_cat = value;
            }
        }
        public string image 
        {
            get
            {
                return m_img;
            }
            set
            {
                m_img = value;
            }
        }
        public string util
        {
            get
            {
                return m_idutil;
            }
            set
            {
                m_idutil = value;
            }
        }
        public string Commentaires
        {
            get
            {
                return m_Commentaires;
            }
            set
            {
                m_Commentaires = value;
            }
        }
        public string IDUniqueNomPhoto
        {
            get
            {
                string s = DateTime.Now.Year.ToString()+DateTime.Now.Month.ToString()+DateTime.Now.Day.ToString()+DateTime.Now.Hour.ToString(CultureInfo.InvariantCulture) + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                return s;
            }
        }
        public PhotoModels()
        {
            m_cat = null;
            m_idutil = null;
            m_img = null;
            m_Commentaires = null;
        }
    }
}
