using Biblioteca._Repositorio.Core;
using Biblioteca.Models;
using Biblioteca.Models.ViewModel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace Biblioteca.Controllers
{
    public class EmprestimoController : Controller
    {
        private IEmprestimoRepositorio _EmprestRep;
        private ILivroRepositorio _LivroRep;

        public EmprestimoController(IEmprestimoRepositorio estoqueRep, ILivroRepositorio livroRep)
        {
            _EmprestRep = estoqueRep;
            _LivroRep = livroRep;
        }

        public ActionResult Index()
        {
            var item = _EmprestRep.ListarEmprestimo();
            ViewBag.CountEmprestimos = item.Count();
            ViewBag.CountNDevolvido = item.Where(x => x.Recebido == false).Count();
            return View(item);
        }

        public ActionResult ReplaceViewComponent(Guid id)
        {
            ViewBag.Livro = _EmprestRep.ListarEmprestimo().Where(x=>x.Id ==id).FirstOrDefault().Livro.Nome;
            return ViewComponent("Recebido", new { id });
        }
        public ActionResult Detalhes(Guid Id)
        {
            var item = _EmprestRep.BuscarPorId(Id);
            return View(item);
        }

        public ActionResult Cadastrar()
        {
            ViewBag.Livros = _LivroRep.ListarLivros().Select(l =>
            new SelectListItem()
            {
                Text = $"{l.Nome}, Autor: {l.Autor}, Ed.{l.Edicao} | Estoque: {l.Quantidade - _EmprestRep.ListarEmprestimo().Where(x => x.Livro.Id == l.Id && x.Recebido == false).Count()}",
                Value = l.Id.ToString()
            }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(EmprestimoViewModel item)
        {
            if (ModelState.IsValid)
            {
                var emprestimo = new Emprestimo(item.Id, item.Aluno, item.IdLivro);
                _EmprestRep.Adicionar(emprestimo);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Devolver(Guid Id)
        {
            if (ModelState.IsValid)
            {
                _EmprestRep.Recebido(Id);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                string errors = string.Empty;
                foreach (var erro in ModelState)
                {
                    errors += $"{erro}\n";
                }
                return View(errors);
            }
        }

        public ActionResult Deletar(Guid Id)
        {
            var item = _EmprestRep.BuscarPorId(Id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid Id)
        {
            if (ModelState.IsValid)
            {
                _EmprestRep.Remover(Id);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                string errors = string.Empty;
                foreach (var erro in ModelState)
                {
                    errors += $"{erro}\n";
                }
                return View(errors);
            }
        }
    }
}