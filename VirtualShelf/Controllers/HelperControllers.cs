using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VirtualShelf.Controllers
{
    public class HelperControllers
    {

        public static Boolean VerificaUserLogado(ISession session)
        {
            string logado = session.GetString("Logado");
            if (logado == null)
                return false;
            else
                return true;
        }

        public static Boolean VerificaUserAdmin(ISession session)
        {
            string ehAdmin = session.GetString("admin");
            if (ehAdmin == null)
                return false;
            else
                return true;
        }

        public static int ObtemUserId(ISession session)
        {
            string id = session.GetString("Id");
            if(!string.IsNullOrEmpty(id))
                return Convert.ToInt32(id);
            else
                return -1;
        }

    }
}