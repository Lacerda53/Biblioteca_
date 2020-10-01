using Biblioteca._Repositorio.Core;
using Biblioteca.Models.ViewModel;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace Biblioteca.Controllers
{
    public class RelatorioController : Controller
    {
        private IEmprestimoRepositorio _EmprestRep;

        public RelatorioController(IEmprestimoRepositorio emprestRep)
        {
            _EmprestRep = emprestRep;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DownloadExcelNaoDevolvido()
        {
            var list = _EmprestRep.ListarEmprestimo().Where(x => x.Recebido == false).ToArray();
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = $"Relatorio Emprestimos nao devolvidos {DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("EMPRESTIMOS NÃO DEVOLVIDOS");
                    worksheet.Cell("B1").Value = "LISTA DE EMPRESTIMOS NÂO DEVOLVIDOS";
                    worksheet.Cell(2, 2).Value = "LIVRO";
                    worksheet.Cell(2, 3).Value = "ALUNO";
                    worksheet.Cell(2, 4).Value = "DATA EMPRESTIMO";
                    worksheet.Row(2).Style.Font.Bold = true;
                    worksheet.Row(2).Style.Font.FontColor = XLColor.White;
                    var rngHeaders = worksheet.Range("B2:D2");
                    rngHeaders.Style.Font.Bold = true;
                    rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    rngHeaders.Style.Fill.BackgroundColor = XLColor.BluePigment;
                    worksheet.Columns(2, 4).AdjustToContents();
                    worksheet.Columns(2, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;


                    for (int index = 1; index <= list.Count(); index++)
                    {
                        worksheet.Cell(index + 2, 2).Value = list[index - 1].Livro.Nome;
                        worksheet.Cell(index + 2, 3).Value = list[index - 1].Aluno;
                        worksheet.Cell(index + 2, 4).Value = list[index - 1].DataEmprestimo.ToString("dd/MM/yyyy");
                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [HttpPost]
        public IActionResult DownloadExcelEmprestimo(RelatorioViewModel item)
        {
            if (ModelState.IsValid)
            {
                var list = _EmprestRep.ListarEmprestimo().Where(x => x.DataEmprestimo.ToString("MM/yyyy") == item.DataRelatorio.ToString("MM/yyyy")).ToArray();
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = $"Relatorio Emprestimos {item.DataRelatorio.ToString("MMMM-yyyy")}.xlsx";
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        IXLWorksheet worksheet =
                        workbook.Worksheets.Add("EMPRESTIMOS");
                        worksheet.Cell("B1").Value = $"LISTA DE EMPRESTIMOS DO MÊS {item.DataRelatorio.ToString("MMMM/yyyy")}";
                        worksheet.Cell(2, 2).Value = "LIVRO";
                        worksheet.Cell(2, 3).Value = "ALUNO";
                        worksheet.Cell(2, 4).Value = "DATA EMPRESTIMO";
                        worksheet.Cell(2, 5).Value = "DATA DEVOLUÇÃO";
                        worksheet.Row(2).Style.Font.Bold = true;
                        worksheet.Row(2).Style.Font.FontColor = XLColor.White;
                        var rngHeaders = worksheet.Range("B2:E2");
                        rngHeaders.Style.Font.Bold = true;
                        rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        rngHeaders.Style.Fill.BackgroundColor = XLColor.BluePigment;
                        worksheet.Columns(2, 5).AdjustToContents();
                        worksheet.Columns(2, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;


                        for (int index = 1; index <= list.Count(); index++)
                        {
                            worksheet.Cell(index + 2, 2).Value = list[index - 1].Livro.Nome;
                            worksheet.Cell(index + 2, 3).Value = list[index - 1].Aluno;
                            worksheet.Cell(index + 2, 4).Value = list[index - 1].DataEmprestimo.ToString("dd/MM/yyyy");
                            if (list[index - 1].DataDevolucao.ToString("dd/MM/yyyy") == "01/01/0001")
                            {
                                worksheet.Cell(index + 2, 5).Value = "Não foi devolvido";
                            }
                            else
                            {
                                worksheet.Cell(index + 2, 5).Value = list[index - 1].DataDevolucao.ToString("dd/MM/yyyy");
                            }
                        }
                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();
                            return File(content, contentType, fileName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return View(ex);
                }
            }
            else
            {
                return View(nameof(Index), item);
            }
        }
    }
}