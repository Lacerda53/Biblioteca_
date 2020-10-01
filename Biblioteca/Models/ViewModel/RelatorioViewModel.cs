using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Models.ViewModel
{
    public class RelatorioViewModel
    {
        [Required(ErrorMessage ="Informe o mês/ano do seu relatório", AllowEmptyStrings = false)]
        public DateTime DataRelatorio { get; set; }
    }
}
