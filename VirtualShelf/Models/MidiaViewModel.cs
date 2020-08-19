using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualShelf.Models
{
    public class MidiaViewModel:PadraoViewModel
    {
        public string Nome { get; set; }
        public string Desenvolvedora { get; set; }
        public int Nota { get; set; }
        public DateTime Lancamento { get; set; }
        public string Descricao { get; set; }
        public int GeneroId { get; set; }
        public IFormFile Imagem { get; set; }
        public byte[] ImagemEmByte { get; set; }
        public string ImagemEmBase64
        {
            get
            {
                if (ImagemEmByte != null)
                    return Convert.ToBase64String(ImagemEmByte);
                else
                    return string.Empty;
            }
        }
        public int tipoMidiaId { get; set; }
    }
}
