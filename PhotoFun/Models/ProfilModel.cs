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

       public ProfilModel()
       {
           m_IdUtilRechercher = null;
           m_nbAbonnement = 0;
           m_Abonner = false;
       }
    }
}
