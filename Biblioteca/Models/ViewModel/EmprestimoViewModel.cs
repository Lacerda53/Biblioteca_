using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Models.ViewModel
{
    public class EmprestimoViewModel
    {
        public EmprestimoViewModel()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Aluno { get; set; }
        public Guid IdLivro { get; set; }
    }
}
