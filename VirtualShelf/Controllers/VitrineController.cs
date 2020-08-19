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
    public class VitrineController : Controller
    {
        public IActionResult Index()
        {
            List<ListagemViewModel> lista = new List<ListagemViewModel>();

            try
            {

                int userId = HelperControllers.ObtemUserId(HttpContext.Session);
                ViewBag.vitrineId = userId;
                ListagemDAO dao = new ListagemDAO();
                lista = dao.ListagemGeral(userId);
                PreparaListaTipoParaCombo();
                PreparaListaCategoriaParaCombo();
            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error.Message;
                //return Json(new
                //{
                //    erro = "Ocorreu um erro: " + error
                //});
            }

            return View(lista);
        }

        private void PreparaListaTipoParaCombo()
        {
            try
            {
                TipoDAO dao = new TipoDAO();
                var estados = dao.Listagem();

                List<SelectListItem> listaEstados = new List<SelectListItem>();
                listaEstados.Add(new SelectListItem("Selecione um Tipo...", "0"));
                foreach (var e in estados)
                {
                    SelectListItem item = new SelectListItem(e.Nome, e.Id.ToString());
                    listaEstados.Add(item);
                }
                ViewBag.Tipo = listaEstados;

            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error.Message;
                //return Json(new
                //{
                //    erro = "Ocorreu um erro: " + error
                //});
            }
            
        }

        public IActionResult AtualizaGridIndex(int tipo, int Categoria, int userId)
        {
            List<ListagemViewModel> lista = new List<ListagemViewModel>();
            try
            {
                ViewBag.UserId = HelperControllers.ObtemUserId(HttpContext.Session);
                ListagemDAO dao = new ListagemDAO();

                lista = dao.ListagemGeral(userId, Categoria, tipo, "");

            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error.Message;
                //return Json(new
                //{
                //    erro = "Ocorreu um erro: " + error
                //});
            }

            

            return PartialView("pvGrid", lista);
        }

        private void PreparaListaCategoriaParaCombo()
        {
            try
            {
                GeneroDAO dao = new GeneroDAO();
                var estados = dao.Listagem();

                List<SelectListItem> listaEstados = new List<SelectListItem>();
                listaEstados.Add(new SelectListItem("Selecione um Genero...", "0"));
                foreach (var e in estados)
                {
                    SelectListItem item = new SelectListItem(e.Nome, e.Id.ToString());
                    listaEstados.Add(item);
                }
                ViewBag.Categoria = listaEstados;
            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error.Message;
                //return Json(new
                //{
                //    erro = "Ocorreu um erro: " + error
                //});
            }
            
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.UserId = HelperControllers.ObtemUserId(HttpContext.Session);
            ViewBag.admin = HelperControllers.VerificaUserAdmin(HttpContext.Session);
            if (!HelperControllers.VerificaUserLogado(HttpContext.Session))
                context.Result = RedirectToAction("Index", "Login");
            else
            {
                ViewBag.Logado = true;
                base.OnActionExecuting(context);
            }
        }
        public IActionResult AdicionaNaVitrine(int idMidia, int idStatus, int tipoMidiaId)
        {

            try
            {
                int userId = HelperControllers.ObtemUserId(HttpContext.Session);

                MidiaDAO dao = new MidiaDAO();
                dao.InsereMidiaUsuario(userId, idMidia, idStatus);
            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error.Message;
                //return Json(new
                //{
                //    erro = "Ocorreu um erro: " + error
                //});
            }
            
            return RedirectToAction("index");
        }

        public IActionResult VisitarVitrine(int id)
        {
            List<ListagemViewModel> lista = new List<ListagemViewModel>();

            try
            {
                ViewBag.vitrineId = id;
                ListagemDAO dao = new ListagemDAO();
                lista = dao.ListagemGeral(id);
                PreparaListaTipoParaCombo();
                PreparaListaCategoriaParaCombo();
            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error.Message;
                //return Json(new
                //{
                //    erro = "Ocorreu um erro: " + error
                //});
            }

            return View("index", lista);
        }

    }
}