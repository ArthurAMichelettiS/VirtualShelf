using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualShelf.Models
{
    public class ListagemViewModel
    {
        public string Usuario { get; set; }
        public string Tipo { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public string Status { get; set; }
        public int MidiaId { get; set; }
    }
}
