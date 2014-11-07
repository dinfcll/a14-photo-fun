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

       public ProfilModel()
       {
           m_IdUtilRechercher = null;
           m_nbAbonnement = 0;
       }
    }
}