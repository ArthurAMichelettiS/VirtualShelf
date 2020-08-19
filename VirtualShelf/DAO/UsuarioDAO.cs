using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VirtualShelf.Models;


namespace VirtualShelf.DAO
{
    public class UsuarioDAO : PadraoDAO<UsuarioViewModel>
    {
        protected override SqlParameter[] CriaParametros(UsuarioViewModel model)
        {

            SqlParameter[] parametros = new SqlParameter[7];
            parametros[0] = new SqlParameter("id", model.Id);
            parametros[1] = new SqlParameter("nome", model.Nome);
            parametros[2] = new SqlParameter("login", model.Login);
            parametros[3] = new SqlParameter("senha", model.Senha);
            parametros[4] = new SqlParameter("email", model.Email);
            parametros[5] = new SqlParameter("telefone", model.Telefone);
            parametros[6] = new SqlParameter("ehPrivado", model.EhPrivado);

            return parametros;
        }

        protected override UsuarioViewModel MontaModel(DataRow registro)
        {
            UsuarioViewModel l = new UsuarioViewModel
            {
                Id = Convert.ToInt32(registro["id"]),
                Nome = registro["nome"].ToString(),
                Login = registro["login"].ToString(),
                Senha = registro["senha"].ToString(),
                Email = registro["email"].ToString(),
                Telefone = registro["telefone"].ToString(),
                EhPrivado = Convert.ToBoolean(registro["ehPrivado"])
            };

            return l;
        }

        protected override void SetTabela()
        {
            Tabela = "usuarios";
        }

        public virtual UsuarioViewModel ValidaLogin(string login, string senha)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("login", login),
                new SqlParameter("senha", senha)
            };
            var tabela = HelperDAO.ExecutaProcSelect("spValidaLogin", p);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }

        public List<UsuarioViewModel> AchaFriends(string txtBusca, string txtMidia)
        {
            var p = new SqlParameter[]
           {
                new SqlParameter("usuarios", txtBusca),
                new SqlParameter("midias", txtMidia),
           };

            if (string.IsNullOrEmpty(txtBusca))
                p[0].Value = DBNull.Value;

            if (string.IsNullOrEmpty(txtMidia))
                p[1].Value = DBNull.Value;

            var tabela = HelperDAO.ExecutaProcSelect("spBuscarUser", p);
            List<UsuarioViewModel> lista = new List<UsuarioViewModel>();
            foreach (DataRow registro in tabela.Rows)
            {
                lista.Add(MontaModel(registro));
            }
            return lista;
        }


        public List<UsuarioViewModel> ListaAmigos(int userId, bool pedidoAmizade)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id", userId),
                new SqlParameter("pedido", pedidoAmizade),
            };

            var tabela = HelperDAO.ExecutaProcSelect("spListaFriends", p);
            List<UsuarioViewModel> lista = new List<UsuarioViewModel>();
            foreach (DataRow registro in tabela.Rows)
            {
                lista.Add(MontaModel(registro));
            }
            return lista;
        }


        public void RemoverAmizade(int userId, int friendId)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("usuarioId", userId),
                new SqlParameter("friendId", friendId)
            };
            HelperDAO.ExecutaProcSelect("spCancelaAmizade", p);
        }


        public void ConviteAmizade(int userId, int friendId)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("usuarioId", userId),
                new SqlParameter("friendId", friendId)
            };
            HelperDAO.ExecutaProcSelect("spSolicitaAmizade", p);
        }


        public void ConcretizaAmizade(int userId, int friendId)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("usuarioId", userId),
                new SqlParameter("friendId", friendId)
            };
            HelperDAO.ExecutaProcSelect("spConcretizaAmizade", p);
        }


    }
}

/*
 * CREATE TABLE [dbo].[usuarios](
	[id] [int] primary key IDENTITY(1,1) NOT NULL,
	[login] [varchar](50) NULL,
	[senha] [varchar](50) NULL,
	[nome] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[telefone] [varchar](11) NULL,
	[ehPrivado] [bit] NULL
)
 */
