using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Services
{
    public class UploadService
    {
        public static string MudarFileName(string fileName)
        {
            string novo_nome = string.Empty;
            string[] nome_original = fileName.Split('.');
            string extensaoA = nome_original[nome_original.Length - 1];
            nome_original[nome_original.Length - 1] = DateTime.Now.ToBinary().ToString();
            for (int cont = 0; cont < nome_original.Length; cont++)
            {
                novo_nome += nome_original[cont] + ".";
            }
            return novo_nome += extensaoA;
        }
    }
}
