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
        private int m_idphoto=1;

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
        public int IdPhoto
        {
            get
            {
                return m_idphoto;
            }
        }
        public PhotoModels()
        {
            m_cat = null;
            m_idutil = 
            m_img = null;
            m_idphoto++;
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
