using Biblioteca.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Services
{
    public class Arquivos
    {
        private static List<ArquivosViewModel> _arquivos = new List<ArquivosViewModel>();

        public static void AddArquivos(ArquivosViewModel nomeArquivo)
        {
            _arquivos.Add(nomeArquivo);
        }

        public static IEnumerable<ArquivosViewModel> ListaArquivos()
        {
            return _arquivos;
        }

        public static void DeletarArquivo(ArquivosViewModel nomeArquivo)
        {

            _arquivos.Remove(nomeArquivo);
        }
    }
}
