using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;

namespace Utilitaire
{
    public class sErreurs
    {
        private String _MessageUtilisateur, _MessageApplication;
        public sErreurs(String mu, String ma)
        {
            _MessageUtilisateur = mu;
            _MessageApplication = ma;
        }
        public String MessageUtilisateur()
        {
            return (_MessageUtilisateur);
        }
        public String MessageApplication()
        {
            return (_MessageApplication);
        }
    }
}
