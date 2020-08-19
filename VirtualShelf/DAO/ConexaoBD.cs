using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShelf.DAO
{
    public static class ConexaoBD
    {        
        public static SqlConnection GetConexao()
        {
            string strCon = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=VirtualShelf;user id=sa; password=123456";
            SqlConnection conexao = new SqlConnection(strCon);
            conexao.Open();
            return conexao;
        }
    }
}
