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
using System.Text.RegularExpressions;


namespace VirtualShelf.Controllers
{
    public class UsuarioController : PadraoController<UsuarioViewModel>
    {
        public UsuarioController()
        {
            DAO = new UsuarioDAO();

            GeraProximoId = true;
        }

        protected override void ValidaDados(UsuarioViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);

            if (string.IsNullOrEmpty(model.Nome))
                ModelState.AddModelError("Nome", "Preencha o nome!");

            //verificando se os campos estão preenchidos

            if (string.IsNullOrEmpty(model.Login))
                ModelState.AddModelError("Login", "Preencha o login!");

            if (string.IsNullOrEmpty(model.Senha))
                ModelState.AddModelError("Senha", "Preencha a senha!");

            if (string.IsNullOrEmpty(model.Email))
                ModelState.AddModelError("Email", "Preencha o email!");

            if (string.IsNullOrEmpty(model.Telefone))
                ModelState.AddModelError("Telefone", "Preencha o telefone!");

            //vertificando se os valores inseridos são válidos

            //nome

            if (model.Nome.Contains("!") || model.Nome.Contains("@") || model.Nome.Contains("#") || model.Nome.Contains("$") ||
                model.Nome.Contains("%") || model.Nome.Contains("¨") || model.Nome.Contains("&") || model.Nome.Contains("*") ||
                model.Nome.Contains("(") || model.Nome.Contains(")") || model.Nome.Contains("-") || model.Nome.Contains("_") ||
                model.Nome.Contains("1") || model.Nome.Contains("2") || model.Nome.Contains("3") || model.Nome.Contains("4") ||
                model.Nome.Contains("5") || model.Nome.Contains("6") || model.Nome.Contains("7") || model.Nome.Contains("8") ||
                model.Nome.Contains("9") || model.Nome.Contains("0") || model.Nome.Contains("+") || model.Nome.Contains("§") ||
                model.Nome.Contains("{") || model.Nome.Contains("}") || model.Nome.Contains("[") || model.Nome.Contains("]") ||
                model.Nome.Contains("<") || model.Nome.Contains(">") || model.Nome.Contains(",") || model.Nome.Contains(".") ||
                model.Nome.Contains(":") || model.Nome.Contains(";") || model.Nome.Contains("?") || model.Nome.Contains("*") ||
                model.Nome.Contains("£") || model.Nome.Contains("¢") || model.Nome.Contains("¬") || model.Nome.Contains("^") ||
                model.Nome.Contains("~")
                )
                ModelState.AddModelError("Nome", "O nome possui caracteres inválidos");

            //email

            string textEmail = model.Email;
            bool valorEmail = textEmail.Contains("@") && textEmail.Contains(".com");
            if (valorEmail == false)
                ModelState.AddModelError("Email", "O email inserido não é válido!");

            //senha

            if (GetForcaDaSenha(model.Senha) == ForcaDaSenha.Inaceitavel)
                ModelState.AddModelError("Senha", "A senha é muito fraca! Acrescente letras maiúsculas, minúsculas, números e símbolos");

            //telefone

            model.Telefone = model.Telefone.Replace("-", "").Replace(" ", "");

            try
            {
                string tel = model.Telefone;
                long telConvertido = long.Parse(tel);
            }
            catch
            {
                ModelState.AddModelError("Telefone", "O telefone inserido é inválido");
            }

            if (model.Telefone.Length > 11)
                ModelState.AddModelError("Telefone", "O telefone inserido é inválido");

            else if (model.Telefone.Length == 9 || model.Telefone.Length == 8)
                ModelState.AddModelError("Telefone", "Adicione o telefone com o DDD");

            else if (model.Telefone.Length < 8)
                ModelState.AddModelError("Telefone", "O telefone inserido é inválido");
        }
        public int geraPontosSenha(string senha)
        {
            if (senha == null) return 0;
            int pontosPorTamanho = GetPontoPorTamanho(senha);
            int pontosPorMinusculas = GetPontoPorMinusculas(senha);
            int pontosPorMaiusculas = GetPontoPorMaiusculas(senha);
            int pontosPorDigitos = GetPontoPorDigitos(senha);
            int pontosPorSimbolos = GetPontoPorSimbolos(senha);
            int pontosPorRepeticao = GetPontoPorRepeticao(senha);
            return pontosPorTamanho + pontosPorMinusculas + pontosPorMaiusculas + pontosPorDigitos + pontosPorSimbolos - pontosPorRepeticao;
        }

        private int GetPontoPorTamanho(string senha)
        {
            return Math.Min(10, senha.Length) * 6;
        }

        private int GetPontoPorMinusculas(string senha)
        {
            int rawplacar = senha.Length - Regex.Replace(senha, "[a-z]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private int GetPontoPorMaiusculas(string senha)
        {
            int rawplacar = senha.Length - Regex.Replace(senha, "[A-Z]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private int GetPontoPorDigitos(string senha)
        {
            int rawplacar = senha.Length - Regex.Replace(senha, "[0-9]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private int GetPontoPorSimbolos(string senha)
        {
            int rawplacar = Regex.Replace(senha, "[a-zA-Z0-9]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private int GetPontoPorRepeticao(string senha)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(\w)*.*\1");
            bool repete = regex.IsMatch(senha);
            if (repete)
            {
                return 30;
            }
            else
            {
                return 0;
            }
        }

        public ForcaDaSenha GetForcaDaSenha(string senha)
        {
            int placar = geraPontosSenha(senha);

            if (placar < 50)
                return ForcaDaSenha.Inaceitavel;
            else if (placar < 60)
                return ForcaDaSenha.Fraca;
            else if (placar < 80)
                return ForcaDaSenha.Aceitavel;
            else if (placar < 100)
                return ForcaDaSenha.Forte;
            else
                return ForcaDaSenha.Segura;
        }

        public enum ForcaDaSenha
        {
            Inaceitavel,
            Fraca,
            Aceitavel,
            Forte,
            Segura
        }




        public override IActionResult Delete(int id)
        {
            base.Delete(id);

            if(ViewBag.Excecao == null)
            {
                return RedirectToAction("LogOff", "login");
            }
            return RedirectToAction("friends");
        }






        public override void OnActionExecuting(ActionExecutingContext context)
        {
            AtribuiViewBags();

            CriaListaAmigos();
        }


        public void CriaListaAmigos()
        {
            try
            {
                int userId = HelperControllers.ObtemUserId(HttpContext.Session);
                ViewBag.Amigos = (DAO as UsuarioDAO).ListaAmigos(userId, false);
                ViewBag.Pedidos = (DAO as UsuarioDAO).ListaAmigos(userId, true);
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


        public IActionResult BuscaUsuarios(string txtUsuario, string txtMidia)
        {
            List<UsuarioViewModel> lista = new List<UsuarioViewModel>();

            try
            {
                if (string.IsNullOrEmpty(txtUsuario)&& string.IsNullOrEmpty(txtMidia))
                    lista = (DAO as UsuarioDAO).Listagem();
                else
                    lista = (DAO as UsuarioDAO).AchaFriends(txtUsuario, txtMidia);
            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error.Message;
                //return Json(new
                //{
                //    erro = "Ocorreu um erro: " + error
                //});
            }


            return PartialView("pvBusca", lista);
        }

        public IActionResult Friends()
        {
            return View("friends");
        }

        public IActionResult Buscar()
        {


            List<UsuarioViewModel> u = new List<UsuarioViewModel>();

            try
            {
                u = (DAO as UsuarioDAO).Listagem();
            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error.Message;
                //return Json(new
                //{
                //    erro = "Ocorreu um erro: " + error
                //});
            }

            return View("buscar", u);
        }


        public IActionResult RemoveAmizade(int friendId)
        {

            try
            {
                int userId = HelperControllers.ObtemUserId(HttpContext.Session);

                (DAO as UsuarioDAO).RemoverAmizade(userId, friendId);
            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error.Message;
                //return Json(new
                //{
                //    erro = "Ocorreu um erro: " + error
                //});
            }


            return RedirectToAction("friends");
        }


        public IActionResult ConviteAmizade(int friendId)
        {

            try
            {
                int userId = HelperControllers.ObtemUserId(HttpContext.Session);

                (DAO as UsuarioDAO).ConviteAmizade(userId, friendId);
            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error.Message;
                //return Json(new
                //{
                //    erro = "Ocorreu um erro: " + error
                //});
            }


            return RedirectToAction("friends");

        }

        public IActionResult ConcretizaAmizade(int friendId)
        {
            try
            {
                int userId = HelperControllers.ObtemUserId(HttpContext.Session);

                (DAO as UsuarioDAO).ConcretizaAmizade(userId, friendId);
            }
            catch (Exception error)
            {
                ViewBag.Excecao = "Ocorreu um erro: " + error.Message;
                //return Json(new
                //{
                //    erro = "Ocorreu um erro: " + error
                //});
            }

            return RedirectToAction("friends");

        }

        
    }
}
