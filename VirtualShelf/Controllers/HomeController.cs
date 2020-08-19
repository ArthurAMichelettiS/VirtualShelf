using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VirtualShelf.Models;
using VirtualShelf.DAO;

namespace VirtualShelf.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                MidiaDAO dao = new MidiaDAO();
                ViewBag.Img1 = dao.MidiaTop(1);
                ViewBag.Img2 = dao.MidiaTop(2);
                ViewBag.Img3 = dao.MidiaTop(3);
            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error;
                //return Json(new
                //{
                //    erro = "Ocorreu um erro: " + error
                //});
            }
            
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.admin = HelperControllers.VerificaUserAdmin(HttpContext.Session);
            ViewBag.Logado = HelperControllers.VerificaUserLogado(HttpContext.Session);
            ViewBag.UserId = HelperControllers.ObtemUserId(HttpContext.Session);
        }
    }
}
