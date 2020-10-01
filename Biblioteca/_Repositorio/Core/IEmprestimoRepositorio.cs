using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca._Repositorio.Core
{
    public interface IEmprestimoRepositorio
    {
        void Adicionar(Emprestimo item);
        void Editar(Emprestimo item);
        void Recebido(Guid Id);
        void Remover(Guid Id);
        Emprestimo BuscarPorId(Guid Id);
        IEnumerable<Emprestimo> ListarEmprestimo();
    }
}
