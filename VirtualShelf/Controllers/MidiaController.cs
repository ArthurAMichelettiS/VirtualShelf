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
    public class MidiaController : PadraoController<MidiaViewModel>
    {

        public MidiaController()
        {
            DAO = new MidiaDAO();

            GeraProximoId = true;
        }

        protected override void PreencheDadosParaView(string Operacao, MidiaViewModel model)
        {
            if (model.tipoMidiaId == 1)
            {
                ViewBag.TipoMidiaId = 1;
                ViewBag.TipoMidia = "Jogo";
            }
            else if (model.tipoMidiaId == 2)
            {
                ViewBag.TipoMidiaId = 2;
                ViewBag.TipoMidia = "Filme";
            }
            else if (model.tipoMidiaId == 3)
            {
                ViewBag.TipoMidiaId = 3;
                ViewBag.TipoMidia = "Livro";
            }

            PreparaListaParaCombo();

            

                model.Lancamento = DateTime.Now;

            base.PreencheDadosParaView(Operacao, model);
        }

        public byte[] ConvertImageToByte(IFormFile file)
        {
            if (file != null)
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    return ms.ToArray();
                }
            else
                return null;
        }
        protected override void ValidaDados(MidiaViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);

            if (model.Imagem == null && operacao == "I")
                ModelState.AddModelError("Imagem", "Escolha uma imagem.");
            if (model.Imagem != null && model.Imagem.Length / 1024 / 1024 >= 2)
                ModelState.AddModelError("Imagem", "Imagem limitada a 2 mb.");
            if (ModelState.IsValid)
            {
                //na alteração, se não foi informada a imagem, iremos manter a que já estava salva.
                if (operacao == "A" && model.Imagem == null)
                {
                    MidiaViewModel midia = DAO.Consulta(model.Id);
                    model.ImagemEmByte = midia.ImagemEmByte;
                }
                else
                {
                    model.ImagemEmByte = ConvertImageToByte(model.Imagem);
                }
            }

            if (string.IsNullOrEmpty(model.Nome))
                ModelState.AddModelError("Nome", "Preencha o nome!");

            if (string.IsNullOrEmpty(model.Desenvolvedora))
                ModelState.AddModelError("Desenvolvedora", "Preencha a desenvolvedora!");

            if (string.IsNullOrEmpty(model.Descricao))
                ModelState.AddModelError("Descricao", "Preencha a descrição!");

            if (model.Lancamento > DateTime.Now)
                ModelState.AddModelError("Lancamento", "Data inválida!");

            if (model.GeneroId == 0)
                ModelState.AddModelError("GeneroId", "Selecione o gênero!");
        }

        private void PreparaListaParaCombo()
        {
            try
            {
                GeneroDAO generoDao = new GeneroDAO();
                var generos = generoDao.Listagem();
                List<SelectListItem> listaGenero = new List<SelectListItem>();

                listaGenero.Add(new SelectListItem("Selecione um genero...", "0"));
                foreach (var g in generos)
                {
                    SelectListItem item = new SelectListItem(g.Nome, g.Id.ToString());
                    listaGenero.Add(item);
                }
                ViewBag.Generos = listaGenero;
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

        public IActionResult Details(int id)
        {
            MidiaViewModel midis = DAO.Consulta(id);
            if (midis == null)
                return RedirectToAction("index");
            else
                return View("Details", midis);

        }


        public IActionResult AdicionaNaVitrine(int idF, bool ehInteresse)
        {
            
            try
            {
                int userId = HelperControllers.ObtemUserId(HttpContext.Session);

                (DAO as MidiaDAO).InsereMidiaUsuario(userId, idF, (ehInteresse ? 1 : 2));
            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error.Message;
                //return Json(new
                //{
                //    erro = "Ocorreu um erro: " + error
                //});
            }
            

            return RedirectToAction("index", "vitrine");
        }


        public IActionResult RemoveDaVitrine(int idF)
        {
            try
            {
                int userId = HelperControllers.ObtemUserId(HttpContext.Session);

                (DAO as MidiaDAO).RemoveMidiaUsuario(userId, idF);
            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error.Message;
                //return Json(new
                //{
                //    erro = "Ocorreu um erro: " + error
                //});
            }


            return RedirectToAction("index", "vitrine");
        }


        public IActionResult Detalhes(int idF)
        {
            MidiaViewModel m = new MidiaViewModel();
            try
            {
                m = DAO.Consulta(idF);
            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error.Message;
                //return Json(new
                //{
                //    erro = "Ocorreu um erro: " + error
                //});
            }


            return View("Details", m);
        }



        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }


        public IActionResult Jogos()
        {
            ViewBag.TipoMidiaId = 1;
            ViewBag.TipoMidia = "Jogo";
            List<MidiaViewModel> lista;
            try
            {
                int userId = HelperControllers.ObtemUserId(HttpContext.Session);

                lista = (DAO as MidiaDAO).ListaMidias(1, userId);
            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error.Message;

                lista = new List<MidiaViewModel>();

                //return Json(new
                //{
                //    erro = "Ocorreu um erro: " + error
                //});
            }

            return View("Index",lista);
        }

        public IActionResult Filmes()
        {
            ViewBag.TipoMidiaId = 2;
            ViewBag.TipoMidia = "Filme";
            List<MidiaViewModel> lista;
            try
            {
                int userId = HelperControllers.ObtemUserId(HttpContext.Session);

                lista = (DAO as MidiaDAO).ListaMidias(2, userId);
            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error.Message;

                lista = new List<MidiaViewModel>();

                //return Json(new
                //{
                //    erro = "Ocorreu um erro: " + error
                //});
            }
            return View("Index", lista);
        }

        public IActionResult Livros()
        {
            ViewBag.TipoMidiaId = 3;
            ViewBag.TipoMidia = "Livro";
            List<MidiaViewModel> lista;
            try
            {
                int userId = HelperControllers.ObtemUserId(HttpContext.Session);

                lista = (DAO as MidiaDAO).ListaMidias(3, userId);
            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error.Message;

                lista = new List<MidiaViewModel>();

                //return Json(new
                //{
                //    erro = "Ocorreu um erro: " + error
                //});
            }
            return View("Index", lista);
        }


        public IActionResult CriaJogo()
        {
            ViewBag.TipoMidiaId = 1;
            ViewBag.TipoMidia = "Jogo";
            return Create(0);
        }

        public IActionResult CriaFilme()
        {
            ViewBag.TipoMidiaId = 2;
            ViewBag.TipoMidia = "Filme";
            return Create(0);
        }

        public IActionResult CriaLivro()
        {
            ViewBag.TipoMidiaId = 3;
            ViewBag.TipoMidia = "Livro";
            return Create(0);
        }

        public override IActionResult Index()
        {
            return View("Sucesso");
        }
    }
}