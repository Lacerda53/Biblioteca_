using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca._Repositorio.Core
{
    public interface ILivroRepositorio
    {
        void Adicionar(Livro item);
        void Editar(Livro item);
        void Remover(Guid Id);
        Livro BuscarPorId(Guid Id);
        IEnumerable<Livro> ListarLivros();
    }
}
