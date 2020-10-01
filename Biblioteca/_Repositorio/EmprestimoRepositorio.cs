using Biblioteca._Infra;
using Biblioteca._Repositorio.Core;
using Biblioteca.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca._Repositorio
{
    public class EmprestimoRepositorio : IEmprestimoRepositorio
    {
        private IDB DB;
        public EmprestimoRepositorio(IDB _dB)
        {
            DB = _dB;
        }
        public void Adicionar(Emprestimo item)
        {
            using (var con = DB.GetConnection())
            {
                var query = " INSERT INTO Emprestimo	     " +
                                    "            (Id		 " +
                                    "            ,Aluno	 " +
                                    "            ,DataEmprestimo	 " +
                                    "            ,Recebido	 " +
                                    "            ,IdLivro	 " +
                                    "            ,Status)" +
                                    "      VALUES				 " +
                                    "            (@Id         " +
                                    "            ,@Aluno     " +
                                    "            ,@DataEmprestimo 	 " +
                                    "            ,@Recebido 	 " +
                                    "            ,@IdLivro " +
                                    "            ,@Status)";
                con.Execute(query, new { 
                    item.Id, 
                    item.Aluno, 
                    item.DataEmprestimo,
                    item.Recebido, 
                    IdLivro = item.Livro.Id, 
                    item.Status });
            }
        }

        public void Editar(Emprestimo item)
        {
            using (var con = DB.GetConnection())
            {
                var query = "update Emprestimo set Nome=@Nome, Imagem=@Imagem, Edicao=@Edicao, Autor=@Autor, Quantidade=@Quantidade, Categoria=@Categoria, where Id=@Id";
                con.Execute(query, new { item });
            }
        }

        public void Recebido(Guid id)
        {
            using (var con = DB.GetConnection())
            {
                var query = "update Emprestimo set DataDevolucao=@DataDevolucao, Recebido=1 where Id=@Id";
                con.Execute(query, new { id, DataDevolucao = DateTime.Now });
            }
        }

        public Emprestimo BuscarPorId(Guid Id)
        {
            using (var con = DB.GetConnection())
            {
                var query = "select * from Emprestimo where Id=@Id";
                return con.QueryFirstOrDefault<Emprestimo>(query, new { Id });
            }
        }

        public IEnumerable<Emprestimo> ListarEmprestimo()
        {
            using (var con = DB.GetConnection())
            {
                var query = "select * from Emprestimo AS e inner join Livro AS l ON(e.IdLivro = l.Id) where l.Status = 1";
                return con.Query<Emprestimo, Livro, Emprestimo>(query, (e, l) => { e.Livro = l; return e; }, splitOn: "Id");
            }
        }

        public void Remover(Guid Id)
        {
            using (var con = DB.GetConnection())
            {
                var query = "update Emprestimo set Status = 0 where Id=@Id";
                con.Execute(query, new { Id });
            }
        }
    }
}
