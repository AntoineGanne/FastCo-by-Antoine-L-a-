using System;
using System.Collections.Generic;
using System.Linq;
using Utilitaire;

namespace Metier
{
    public class Service
    {

        private static Service instance;
        private static commercialEntities unCommercial;
        /// <summary>
        /// Obtenir le singleton et le créer s'il n'existe pas
        /// </summary>
        public static Service getInstance()
        {
            if (Service.instance == null)
            {
                Service.instance = new Service();
                // on définit un contexte commun à toutes les requêtes
                unCommercial = new commercialEntities();
            }
            return Service.instance;
        }
        // on rend le constructeur privé
        private Service()
        {
        }



        /// <summary>
        /// Ajouter la commande courante dans la base de données
        /// </summary>
        public void ajouterCommande(commandes uneCde)
        {
            try
            {

                // On ajoute l'objet
                unCommercial.commandes.Add(uneCde);
                unCommercial.SaveChanges();
            }
            catch (MonException erreur)
            {
                throw erreur;
            }
        }


        /// <summary>
        /// Rechercher une commande d'après son numéro
        /// </summary>
        /// <param name="no_cmd">Numéro de la commande</param>
        /// <returns>Commande courante</returns>

        public commandes RechercheCommande(String no_cmd)
        {
            // sErreurs er = new sErreurs("Erreur sur recherche d'un client.", "Client.RechercheUnClient()");
            commandes uneCde;
            try
            {

                uneCde = (from c in unCommercial.commandes
                          where c.NO_COMMAND == no_cmd
                          select c).FirstOrDefault();
                return uneCde;
            }
            catch (Exception e)
            {
                throw new MonException(e.Message, "requête client", e.Message);
            }

        }

        /// <summary>
        /// Supprimer une commande d'après son numéro
        /// </summary>
        /// <param name="no_cmd">Numéro de la commande</param>
        /// <returns>Commande courante</returns>

        public void SupprimerCommande(String no_cmd)
        {
            // sErreurs er = new sErreurs("Erreur sur recherche d'un client.", "Client.RechercheUnClient()");
            commandes uneCde;
            try
            {

                uneCde = (from c in unCommercial.commandes
                          where c.NO_COMMAND == no_cmd
                          select c).FirstOrDefault();
                unCommercial.commandes.Remove(uneCde);
                unCommercial.SaveChanges();
            }
            catch (Exception e)
            {
                throw new MonException(e.Message, "requête suppression commande", e.Message);
            }

        }




        /// <summary>
        /// Lister les commandes de la base
        /// </summary>
        /// <param name="tri">champ de tri</param>
        /// <param name="ordre">sens du tri</param>
        /// <returns>Liste de commandes</returns>
        public List<commandes> getLesCommandes(String tri = "NO_COMMAND", String ordre = "ASC")
        {

            sErreurs err = new sErreurs("", "");
            // Requête SQL équivalente 
            String mysql = "SELECT NO_COMMAND, NO_VENDEUR, NO_CLIENT, DATE_CDE, FACTURE ";
            mysql += "FROM COMMANDES ";
            mysql += "ORDER BY " + tri + " " + ordre;

            List<commandes> mesCdes;
            try
            {
                var rq = from c in unCommercial.commandes
                         orderby tri, ordre
                         select c;
                mesCdes = rq.ToList<commandes>();
                return mesCdes;
            }
            catch (MonException erreur)
            {
                throw erreur;
            }
        }

        ///
        ///  Classe Clientel
        /// <summary>
        /// Lire un utilisateur sur son ID
        /// </summary>
        /// <param name="numCli">N° de l'utilisateur à lire</param>
        /// 

        public clientel RechercheUnClient(String numCli)
        {

            sErreurs er = new sErreurs("Erreur sur recherche d'un client.",
                "Client.RechercheUnClient()");
            clientel unclient;
            try
            {
                unclient = (from c in unCommercial.clientel
                            where c.NO_CLIENT == numCli
                            select c).FirstOrDefault();
                return unclient;
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }

        }

        /// <summary>
        /// Lister les clients de la base
        /// </summary>
        /// <returns>Liste de numéros de clients</returns>
        public List<String> LectureNoClient()
        {

            sErreurs er = new sErreurs("Erreur sur lecture du client.",
                 "Clientel.LectureNoClient()");
            try
            {
                var mesNumeros = (from c in unCommercial.clientel
                                  select c.NO_CLIENT).Distinct();
                return mesNumeros.ToList<String>();
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }
        }

        ///  Classe Vendeur
        /// <summary>
        /// Lire un vendeur sur son ID
        /// </summary>
        /// <param name="numVend">N° du vendeur à lire</param>
        /// 

        public vendeur RechercheUnVendeur(String numVend)
        {
            sErreurs er = new sErreurs("Erreur sur recherche d'un vendeur.",
                "Client.RechercheUnvendeur()");
            vendeur unVendeur;
            try
            {
                unVendeur = (from v in unCommercial.vendeur
                             where v.NO_VENDEUR == numVend
                             select v).FirstOrDefault();
                return unVendeur;
            }
            catch (Exception e)
            {
                throw new MonException(e.Message, "requête vendeur", e.Message);
            }
        }


        /// <summary>
        /// Lister les venderus de la base
        /// </summary>
        /// <returns>liste de vendeurs</returns>
        public List<String> LectureNoVendeur()
        {


            sErreurs er = new sErreurs("Erreur sur lecture du vendeur.", "Vendeur.LectureNoVendeur()");
            try
            {

                var mmesNumeros = (from v in unCommercial.vendeur
                                   select v.NO_VENDEUR).Distinct();

                return mmesNumeros.ToList<String>();
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }




        /// <summary>
        /// Récupérer la liste des articles
        /// </summary>
        /// <param name="tri">champ de tri</param>
        /// <param name="ordre">sens du tri</param>
        /// <returns>lste d'articles</returns>
        public List<articles> getLesArticles(String tri = "NO_ARTICLE", String ordre = "ASC")
        {
            sErreurs err = new sErreurs("", "");

            // Requête SQL équivalente 
            String mysql = "SELECT NO_ARTICLE, LIB_ARTICLE, QTE_DISPO, VILLE_ART, PRIX_ART, INTERROMPU ";
            mysql += "FROM ARTICLES ";
            mysql += "ORDER BY " + tri + " " + ordre;
            List<articles> mesArticles;
            try
            {
                var rq = from a in unCommercial.articles
                         orderby tri, ordre
                         select a;
                mesArticles = rq.ToList<articles>();
                return mesArticles;
            }
            catch (MonException erreur)
            {
                throw erreur;
            }

        }

        /// <summary>
        /// Détermine si un numéro d'article est libre ou déjà utilisé
        /// </summary>
        /// <param name="no">numéro d'article</param>
        /// <returns>booléen</returns>
        public bool NumeroLibre(String no)
        {
            List<articles> tous = getLesArticles();
            foreach (articles art in tous)
                if (art.NO_ARTICLE == no)
                    return false;
            return true;
        }

        /// <summary>
        /// Ajouter l'article courant dans la base de données
        /// </summary>
        public void ajouterArticle(articles unArt)
        {
            sErreurs err = new sErreurs("", "");
            try
            {
                // enregistrer les détails de l'article
                unCommercial.articles.Add(unArt);
                unCommercial.SaveChanges();

            }
            catch (MonException erreur)
            {
                throw erreur;
            }
        }


        /// <summary>
        /// Rechercher un article d'après son numéro
        /// </summary>
        /// <param name="no_cmd">Numéro de l'article</param>
        /// <returns>article courante</returns>
        public articles RechercheArticle(String no_art)
        {
            sErreurs er = new sErreurs("Erreur sur recherche d'un article", "Client.RechercherunArticle");
            articles unArt = null;
            try
            {

                unArt = (from a in unCommercial.articles
                         where a.NO_ARTICLE == no_art
                         select a).FirstOrDefault();
                return unArt;
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }

        }

        /// <summary>
        /// Afficher la liste des articles de la commande
        /// </summary>
        /// <param name="no_cmd">numéro de la commande</param>
        /// <returns>liste d'objets correspondants aux  détails d'articles</returns>
        public List<DetailCde> getLesDetails(String no_cmd)
        {
            sErreurs err = new sErreurs("", "");
            //MySqlConnection cnx = Connexion.getInstance().getConnexion();
            /*String mysql = "SELECT A.NO_ARTICLE, A.LIB_ARTICLE, A.QTE_DISPO, A.VILLE_ART" 
            mysql+="A.PRIX_ART, A.INTERROMPU, D.QTE_CDEE, D.LIVREE, (A.PRIX_ART*D.QTE_CDEE) as TOTAL ";
            mysql += "FROM ARTICLES A, DETAIL_CDE D ";
            mysql += "WHERE A.NO_ARTICLE = D.NO_ARTICLE AND D.NO_COMMAND = '" + no_cmd + "'";*/
            try
            {
                var mesDetails = (from a in unCommercial.articles
                                  from de in unCommercial.detail_cde
                                  where a.NO_ARTICLE == de.NO_ARTICLE
                                 && de.NO_COMMAND == no_cmd
                                  // On doit faire appell aux accesseurs
                                  //  pour initialiser les propriétés privées
                                  select new DetailCde
                                  {
                                      Art = a,
                                      Qte_cdee = (int)de.QTE_CDEE,
                                      Livree = de.LIVREE,
                                      Total = (Double)(de.QTE_CDEE * a.PRIX_ART)
                                  }).ToList();
                return mesDetails;
            }

            catch (MonException erreur)
            {
                throw erreur;
            }
        }

    }
}
