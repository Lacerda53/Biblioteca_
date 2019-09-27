using Biblioteca.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Services
{
    public class Arquivos
    {
        public static List<ArquivosViewModel> _arquivos = new List<ArquivosViewModel>();

        public static void AddArquivos(ArquivosViewModel nomeArquivo)
        {
            _arquivos.Add(nomeArquivo);
        }


        //public static ArquivosViewModel BuscarArquivo(string nomeArquivo, string chave, string usuario)
        //{
        //    string nomeOriginal = String.Empty;
        //    foreach (var item in ListaNomeArquivos(usuario))
        //    {


        //        List<string> nomeAtual = item.Nome.Split('.').ToList();
        //        nomeAtual.RemoveAt(nomeAtual.Count - 2);
        //        nomeOriginal = string.Join(".", nomeAtual);

        //        if (nomeOriginal == nomeArquivo && chave == item.Chave && item.Usuario == usuario)
        //            return item;

        //    }
        //    return null;
        //}
        //public static bool BuscarArquivo(string chave, string usuario)
        //{
        //    string nomeOriginal = String.Empty;
        //    var arquivos = ListaNomeArquivos(usuario);
        //    foreach (var item in arquivos)
        //    {

        //        if (item.Chave == chave)
        //            return true;

        //    }
        //    return false;
        //}

        public static void DeletarArquivo(ArquivosViewModel nomeArquivo)
        {

            _arquivos.Remove(nomeArquivo);
        }
    }
}
