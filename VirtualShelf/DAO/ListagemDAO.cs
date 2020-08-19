using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VirtualShelf.Models;

namespace VirtualShelf.DAO
{
    public class ListagemDAO
    {

        private ListagemViewModel MontaListagem(DataRow registro)
        {
            ListagemViewModel a = new ListagemViewModel();
            a.Usuario = registro["nome"].ToString();
            a.Tipo = registro["tipo"].ToString();
            a.Titulo = registro["titulo"].ToString();
            a.Categoria = registro["categoria"].ToString();
            a.MidiaId = Convert.ToInt32(registro["mdId"].ToString());
            a.Status = registro["status"].ToString();
            return a;
        }

        public virtual List<ListagemViewModel> Listagem(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("idUsuario", id),
            };
            var tabela = HelperDAO.ExecutaProcSelect("spConsultaTodos", p);
            List<ListagemViewModel> lista = new List<ListagemViewModel>();
            foreach (DataRow registro in tabela.Rows)
            {

                lista.Add(MontaListagem(registro));
            }
            return lista;
        }

        public virtual List<ListagemViewModel> ListagemGeral(int idUsuario, int idCategoria=0, int idTipo=0, string txtMidia="")
        {

            var p = new SqlParameter[]
            {
                new SqlParameter("idUsuario", idUsuario),
                new SqlParameter("idCategoria", idCategoria),
                new SqlParameter("idTipo", idTipo),
                new SqlParameter("nome", txtMidia)
            };

            if (idCategoria == 0)
                p[1].Value = DBNull.Value;

            if (idTipo == 0)
                p[2].Value = DBNull.Value;

            if (string.IsNullOrEmpty(txtMidia))
                p[3].Value = DBNull.Value;

            var tabela = HelperDAO.ExecutaProcSelect("spConsultaComposta", p);
            List<ListagemViewModel> lista = new List<ListagemViewModel>();
            foreach (DataRow registro in tabela.Rows)
            {
                lista.Add(MontaListagem(registro));
            }
            return lista;
        }

        public void Insert(int userId, int midiaId, int tipoMidiaId, int statusMidiaId)
        {
            var p = new SqlParameter[]
           {
                new SqlParameter("usuarioId", userId),
                new SqlParameter("midiaId", midiaId),
                new SqlParameter("tipoMidiaId", tipoMidiaId),
                new SqlParameter("statusMidiaId", statusMidiaId)
           };
            HelperDAO.ExecutaProc("spInsereMidiaUsuario", p);
        }
    }
}
