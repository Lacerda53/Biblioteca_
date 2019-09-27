using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Livro
    {
        public Livro()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo Título precisa ser preenchido")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }
        public string Imagem { get; set; }
        public string Sinopse { get; set; }
        [Required(ErrorMessage = "O campo Autor precisa ser preenchido")]
        public string Autor { get; set; }
        [Required(ErrorMessage = "O campo Editora precisa ser preenchido")]
        public string Editora { get; set; }
        [Required(ErrorMessage = "O campo Unidade precisa ser preenchido")]
        public int Unidade { get; set; }
        [Required(ErrorMessage = "O campo Gênero precisa ser preenchido")]
        [Display(Name = "Gênero")]
        public string Genero { get; set; }
    }
}
