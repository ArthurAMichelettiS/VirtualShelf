using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VirtualShelf.Models;

namespace VirtualShelf.DAO
{
    public class GeneroDAO : PadraoDAO<GeneroViewModel>
    {
        protected override SqlParameter[] CriaParametros(GeneroViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("id", model.Id);
            parametros[1] = new SqlParameter("nome", model.Nome);

            return parametros;
        }

        protected override GeneroViewModel MontaModel(DataRow registro)
        {
            GeneroViewModel f = new GeneroViewModel
            {
                Id = Convert.ToInt32(registro["id"]),
                Nome = registro["nome"].ToString(),
            };

            return f;
        }

        protected override void SetTabela()
        {
            Tabela = "generos";
        }
    }
}
