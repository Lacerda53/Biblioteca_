using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Models.ViewModel
{
    public class ArquivosViewModel
    {
        public ArquivosViewModel(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; set; }
    }
}
