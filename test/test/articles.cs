//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace test
{
    using System;
    using System.Collections.Generic;
    
    public partial class articles
    {
        public articles()
        {
            this.compose = new HashSet<compose>();
            this.compose1 = new HashSet<compose>();
            this.detail_cde = new HashSet<detail_cde>();
        }
    
        public string NO_ARTICLE { get; set; }
        public string LIB_ARTICLE { get; set; }
        public int QTE_DISPO { get; set; }
        public string VILLE_ART { get; set; }
        public decimal PRIX_ART { get; set; }
        public string INTERROMPU { get; set; }
    
        public virtual ICollection<compose> compose { get; set; }
        public virtual ICollection<compose> compose1 { get; set; }
        public virtual ICollection<detail_cde> detail_cde { get; set; }
    }
}
