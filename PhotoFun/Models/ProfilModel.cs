using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoFun.Models
{
    public class ProfilModel
    {
        private string m_IdUtilRechercher;
        private int m_nbAbonnement;
        private bool m_Abonner;
        private string m_Courriel;
        private string m_NomUtil;
        private string m_PrenomUtil;

        public ProfilModel()
        {
            m_IdUtilRechercher = null;
            m_nbAbonnement = 0;
            m_Abonner = false;
            m_Courriel = null;
            m_NomUtil = null;
            m_PrenomUtil = null;
        }

        
        public string Courriel
        { 
            get
            {
                return m_Courriel;
            }
            set
            {
                m_Courriel = value;
            } 
        }

        public string NomUtil
        {
            get 
            {
                return m_NomUtil;
            }
            set 
            {
                m_NomUtil = value;
            } 
        }
        public string PrenomUtil
        {
            get 
            {
                return m_PrenomUtil;
            }
            set 
            {
                m_PrenomUtil = value;
            }
        }

        public string IdUtilRechercher
        {
            get
            {
                return m_IdUtilRechercher;
            }
            set
            {
                m_IdUtilRechercher = value;
            }
        }

        public int NbAbonnement
        {
            get
            {
                return m_nbAbonnement;
            }
            set
            {
                m_nbAbonnement = value;
            }
        }

        public bool Abonner
        {
            get
            {
                return m_Abonner;
            }
            set
            {
                m_Abonner = value;
            }
        }
    }
}
