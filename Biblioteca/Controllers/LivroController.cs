﻿using Biblioteca._Repositorio.Core;
using Biblioteca.Models;
using Biblioteca.Models.ViewModel;
using Biblioteca.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Biblioteca.Controllers
{
    public class LivroController : BaseController
    {
        public ILivroRepositorio _LivroRep;
        public LivroController(ILivroRepositorio LivroRepositorio)
        {
            _LivroRep = LivroRepositorio;
        }
        public ActionResult Index()
        {
            var item = _LivroRep.ListarLivros();
            if (HttpExtensions.IsAjaxRequest(Request))
                return PartialView(item);
            else
                return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var novoNome = UploadService.MudarFileName(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Livros", novoNome);
            if (System.IO.File.Exists(path))
            {
                string errors = string.Empty;
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                foreach (ModelStateEntry modelState in ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        errors += error.ErrorMessage + "</br>";
                    }
                }

                return Json(new
                {
                    success = false,
                    response = errors
                });
            }
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var arquivos = new ArquivosViewModel(novoNome);
            Arquivos.AddArquivos(arquivos);
            return Json(new
            {
                success = true,
                response = "Upload efetuado com sucesso."
            });
        }

        public ActionResult Detalhes(Guid Id)
        {
            var item = _LivroRep.BuscarPorId(Id); 
            if (HttpExtensions.IsAjaxRequest(Request))
                return PartialView(item);
            return View(item);
        }

        public ActionResult Cadastrar()
        {
            if (HttpExtensions.IsAjaxRequest(Request))
                return PartialView();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Livro livro)
        {

            if (ModelState.IsValid)
            {
                var arquivos = Arquivos.ListaArquivos();
                foreach (var item in arquivos)
                {
                    livro.Imagem = item.Nome;
                }
                if (string.IsNullOrEmpty(livro.Imagem))
                {
                    ModelState.AddModelError("Image", "A imagem principal é necessaria");
                    if (HttpExtensions.IsAjaxRequest(Request))
                        return PartialView("Cadastrar", livro);
                    return View("Cadastrar", livro);
                }
                else
                {
                    _LivroRep.Adicionar(livro);
                    if (HttpExtensions.IsAjaxRequest(Request))
                        return PartialView("Index");
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Editar(Guid Id)
        {
            var item = _LivroRep.BuscarPorId(Id);
            if (HttpExtensions.IsAjaxRequest(Request))
                return PartialView(item);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Livro livro)
        {
            if (ModelState.IsValid)
            {
                _LivroRep.Editar(livro); 
                if (HttpExtensions.IsAjaxRequest(Request))
                    return PartialView("Index");
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
        public ActionResult Estoque(Guid Id)
        {
            var item = _LivroRep.BuscarPorId(Id);
            if (HttpExtensions.IsAjaxRequest(Request))
                return PartialView(item);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Estoque(Livro livro) //Sei que é errado controlar estoque dentro do controle de livros, só fiz pra deixar funcional, depois vou arrumar essa bagunça
        {
            if (ModelState.IsValid)
            {
                _LivroRep.Editar(livro);
                if (HttpExtensions.IsAjaxRequest(Request))
                    return PartialView("Index");
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
            var item = _LivroRep.BuscarPorId(Id);
            if (HttpExtensions.IsAjaxRequest(Request))
                return PartialView(item);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid Id)
        {
            if (ModelState.IsValid)
            {
                _LivroRep.Remover(Id);
                if (HttpExtensions.IsAjaxRequest(Request))
                    return PartialView("Index");
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