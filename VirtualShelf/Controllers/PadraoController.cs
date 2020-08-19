using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualShelf.DAO;
using VirtualShelf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VirtualShelf.Controllers
{
    public class PadraoController<T> : Controller where T : PadraoViewModel
    {
        protected PadraoDAO<T> DAO { get; set; }
        protected bool GeraProximoId { get; set; }
        public virtual IActionResult Index()
        {
            var lista = DAO.Listagem();
            return View(lista);
        }
        public IActionResult Create(int id)
        {
            ViewBag.Operacao = "I";
            T model = Activator.CreateInstance(typeof(T)) as T;
            PreencheDadosParaView("I", model);
            return View("Form", model);
        }
        protected virtual void PreencheDadosParaView(string Operacao, T model)
        {
            if (GeraProximoId && Operacao == "I")
                model.Id = DAO.ProximoId();
        }
        public IActionResult Salvar(T model, string Operacao, string tipoMidia)
        {
            try
            {
                
                ValidaDados(model, Operacao);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    PreencheDadosParaView(Operacao, model);
                    return View("Form", model);
                }
                else
                {
                    if (Operacao == "I")
                        DAO.Insert(model);
                    else
                        DAO.Update(model);
                    return RedirectToAction("index");
                }
            }
            catch (Exception erro)
            {
                ViewBag.Erro = "Ocorreu um erro: " + erro.Message;
                ViewBag.Operacao = Operacao;
                PreencheDadosParaView(Operacao, model);
                return View("Form", model);
            }
        }
        protected virtual void ValidaDados(T model, string operacao)
        {
            if (operacao == "I" && DAO.Consulta(model.Id) != null)
                ModelState.AddModelError("Id", "Código já está em uso!");
            if (operacao == "A" && DAO.Consulta(model.Id) == null)
                ModelState.AddModelError("Id", "Este registro não existe!");
            
        }
        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Operacao = "A";
                var model = DAO.Consulta(id);
                if (model == null)
                    return RedirectToAction("index");
                else
                {
                    PreencheDadosParaView("A", model);
                    return View("Form", model);
                }
            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error.Message;

                return RedirectToAction("index");
            }
        }
        public virtual IActionResult Delete(int id)
        {
            try
            {
                DAO.Delete(id);
                return RedirectToAction("index");
            }
            catch(Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error.Message;

                return RedirectToAction("index");
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            AtribuiViewBags();
            if (!HelperControllers.VerificaUserLogado(HttpContext.Session))
                context.Result = RedirectToAction("Index", "Login");
            else
            {
                ViewBag.Logado = true;
                base.OnActionExecuting(context);
            }
        }

        public void AtribuiViewBags()
        {
            ViewBag.admin = HelperControllers.VerificaUserAdmin(HttpContext.Session);
            ViewBag.Logado = HelperControllers.VerificaUserLogado(HttpContext.Session);
            ViewBag.UserId = HelperControllers.ObtemUserId(HttpContext.Session);
        }

    }
}