using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Livro
    {
        public Livro()
        {
            Id = Guid.NewGuid();
            Status = true;
        }
        public Livro(Guid idLivro)
        {
            Id = idLivro;
        }
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo Nome precisa ser preenchido")]
        public string Nome { get; set; }
        public string Imagem { get; set; }
        [Required(ErrorMessage = "O campo Autor precisa ser preenchido")]
        public string Autor { get; set; }
        [Required(ErrorMessage = "O campo Quantidade precisa ser preenchido")]
        [Range(1,int.MaxValue, ErrorMessage = "O valor mínimo para a quantidade é {0} .")]
        public int Quantidade { get; set; }
        [Required(ErrorMessage = "O campo Categoria precisa ser preenchido")]
        public string Categoria { get; set; }
        [Display(Name ="Edição")]
        public int Edicao { get; set; }
        public bool Status { get; set; }
    }
}
