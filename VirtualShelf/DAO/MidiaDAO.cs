using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VirtualShelf.Models;

namespace VirtualShelf.DAO
{
    public class MidiaDAO : PadraoDAO<MidiaViewModel>
    {
        protected override SqlParameter[] CriaParametros(MidiaViewModel model)
        {
            object imgByte = model.ImagemEmByte;
            if (imgByte == null)
            {
                imgByte = DBNull.Value;
            }

            SqlParameter[] parametros = new SqlParameter[8];
            parametros[0] = new SqlParameter("id", model.Id);
            parametros[1] = new SqlParameter("nome", model.Nome);
            parametros[2] = new SqlParameter("produtora", model.Desenvolvedora);
            parametros[3] = new SqlParameter("tipoMidiaId", model.tipoMidiaId);
            parametros[4] = new SqlParameter("lancamento", model.Lancamento);
            parametros[5] = new SqlParameter("descricao", model.Descricao);
            parametros[6] = new SqlParameter("generoId", model.GeneroId);
            parametros[7] = new SqlParameter("imagem", imgByte);

            return parametros;
        }

        protected override MidiaViewModel MontaModel(DataRow registro)
        {
            MidiaViewModel j = new MidiaViewModel
            {
                Id = Convert.ToInt32(registro["id"]),
                Nome = registro["nome"].ToString(),
                Desenvolvedora = registro["autor"].ToString(),
                tipoMidiaId = Convert.ToInt32(registro["tipoMidiaId"]),
                Lancamento = Convert.ToDateTime(registro["lancamento"]),
                Descricao = registro["descricao"].ToString(),
                GeneroId = Convert.ToInt32(registro["generoId"])
            };
            if (registro["imagem"] != DBNull.Value)
            {
                j.ImagemEmByte = registro["imagem"] as byte[];
            }

            return j;
        }

        protected override void SetTabela()
        {
            Tabela = "midias";
        }



        /// <summary>
        /// spListaMidias
        /// </summary>
        /// <param name="tipoMidiaId">1 a 3 qual midia</param>
        /// <returns>lista de um dos tipos</returns>
        public virtual List<MidiaViewModel> ListaMidias(int tipoMidiaId, int userId)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("tipoMidiaId", tipoMidiaId),
                new SqlParameter("userId", userId),
            };
            var tabela = HelperDAO.ExecutaProcSelect("spListaMidiaPeloTipo", p);
            List<MidiaViewModel> lista = new List<MidiaViewModel>();
            foreach (DataRow registro in tabela.Rows)
            {
                lista.Add(MontaModel(registro));
            }
            return lista;
        }

        public virtual MidiaViewModel MidiaTop(int tipoMidiaId)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("tipoMidiaId", tipoMidiaId),
            };
            var tabela = HelperDAO.ExecutaProcSelect("spListaTop", p);

            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }


        public void InsereMidiaUsuario(int usuarioId, int midiaId, int statusMidiaId)
        {

            var p = new SqlParameter[]
            {
                new SqlParameter("usuarioId", usuarioId),
                new SqlParameter("midiaId", midiaId),
                new SqlParameter("statusMidiaId", statusMidiaId)
            };

            HelperDAO.ExecutaProc("spInsereMidiaUsuario", p);
        }


        public void RemoveMidiaUsuario(int usuarioId, int midiaId)
        {

            var p = new SqlParameter[]
            {
                new SqlParameter("usuarioId", usuarioId),
                new SqlParameter("midiaId", midiaId),
            };

            HelperDAO.ExecutaProc("spRemoveMidiaUsuario", p);
        }


    }
}
