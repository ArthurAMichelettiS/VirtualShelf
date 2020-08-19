using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualShelf.Models
{
    public class UsuarioViewModel : PadraoViewModel
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public bool EhPrivado { get; set; }
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
