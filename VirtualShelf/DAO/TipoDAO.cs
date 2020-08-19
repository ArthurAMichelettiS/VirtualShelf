using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VirtualShelf.Models;

namespace VirtualShelf.DAO
{
    public class TipoDAO : PadraoDAO<TipoViewModel>
    {
        protected override SqlParameter[] CriaParametros(TipoViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("id", model.Id);
            parametros[1] = new SqlParameter("nome", model.Nome);

            return parametros;
        }

        protected override TipoViewModel MontaModel(DataRow registro)
        {
            TipoViewModel f = new TipoViewModel
            {
                Id = Convert.ToInt32(registro["id"]),
                Nome = registro["descricao"].ToString(),
            };

            return f;
        }

        protected override void SetTabela()
        {
            Tabela = "tipoMidia";
        }
    }
}
