using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Emprestimo
    {
        public Emprestimo() { }
        public Emprestimo(Guid id, string aluno, Guid idLivro)
        {
            Id = id;
            Aluno = aluno;
            Livro = new Livro(idLivro);
            Status = true;
            Recebido = false;
            DataEmprestimo = DateTime.Now;
        }
        public Guid Id { get; set; }
        public string Aluno { get; set; }
        public Livro Livro { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public bool Recebido { get; set; }
        public bool Status { get; set; }
    }
}
