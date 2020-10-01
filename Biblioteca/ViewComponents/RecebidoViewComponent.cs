using Biblioteca._Repositorio.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.ViewComponents
{
    [ViewComponent(Name ="Recebido")]
    public class RecebidoViewComponent : ViewComponent
    {
        private IEmprestimoRepositorio _EstoqueRep;

        public RecebidoViewComponent(IEmprestimoRepositorio estoqueRep)
        {
            _EstoqueRep = estoqueRep;
        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync(Guid Id)
        {
            ViewBag.IdSelected = Id;
            var item = _EstoqueRep.BuscarPorId(Id);
            return View(item);
        }
    }
}
