using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Util;

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
                string s = DateTime.Now.Day.ToString()+DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
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

        public bool ReinitialiserPhotoModel()
        {
            return true;
        }

        public bool ConfirmeDonnees()
        {
            if (m_cat != null && m_idutil != null && m_img != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
