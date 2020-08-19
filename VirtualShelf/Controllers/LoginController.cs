using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using VirtualShelf.DAO;
using VirtualShelf.Models;

namespace VirtualShelf.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FazLogin(string usuario, string senha)
        {
            try
            {
                UsuarioDAO DAO = new UsuarioDAO();
                UsuarioViewModel user = DAO.ValidaLogin(usuario, senha);
                if (user != null)
                {
                    HttpContext.Session.SetString("Logado", "true");
                    HttpContext.Session.SetString("Id", user.Id.ToString());
                    if (user.Id == 1)
                        HttpContext.Session.SetString("admin", "true");
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    ViewBag.Erro = "Usuário ou senha inválidos!";
                    return View("Index");
                }
            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error;
                return View("Index");
                //return Json(new
                //{
                //    erro = "Ocorreu um erro: " + error
                //});
            }

        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.admin = HelperControllers.VerificaUserAdmin(HttpContext.Session);
            ViewBag.Logado = HelperControllers.VerificaUserLogado(HttpContext.Session);
            ViewBag.UserId = HelperControllers.ObtemUserId(HttpContext.Session);
        }



        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        
    }
}