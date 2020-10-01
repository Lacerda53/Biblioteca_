using Biblioteca._Infra;
using Biblioteca._Repositorio.Core;
using Biblioteca.Models;
using Dapper;
using System;
using System.Collections.Generic;

namespace Biblioteca._Repositorio
{
    public class LivroRepositorio : ILivroRepositorio
    {
        private IDB DB;
        public LivroRepositorio(IDB _dB)
        {
            DB = _dB;
        }
        public void Adicionar(Livro item)
        {
            using(var con = DB.GetConnection())
            {
                var query = " INSERT INTO Livro	     " +
                            "            (Id		 " +
                            "            ,Nome	 " +
                            "            ,Imagem	 " +
                            "            ,Autor		 " +
                            "            ,Edicao	 " +
                            "            ,Quantidade " +
                            "            ,Categoria	 " +
                            "            ,Status)" +
                            "      VALUES				 " +
                            "            (@Id         " +
                            "            ,@Nome     " +
                            "            ,@Imagem 	 " +
                            "            ,@Autor 	 " +
                            "            ,@Edicao 	 " +
                            "            ,@Quantidade" +
                            "            ,@Categoria " +
                            "            ,@Status)";
                con.Execute(query, new { item.Id, item.Nome, item.Imagem, item.Autor, item.Edicao, item.Quantidade, item.Categoria, item.Status });
            }
        }

        public void Editar(Livro item)
        {
            using(var con = DB.GetConnection())
            {
                var query = "update Livro set Nome=@Nome, Imagem=@Imagem, Edicao=@Edicao, Autor=@Autor, Quantidade=@Quantidade, Categoria=@Categoria, where Id=@Id";
                con.Execute(query, new { item } );
            }
        }

        public Livro BuscarPorId(Guid Id)
        {
            using (var con = DB.GetConnection())
            {
                var query = "select * from Livro where Id=@Id";
                return con.QueryFirstOrDefault<Livro>(query, new { Id });
            }
        }

        public IEnumerable<Livro> ListarLivros()
        {
            using (var con = DB.GetConnection())
            {
                var query = "select * from Livro where Status = 1";
                return con.Query<Livro>(query);
            }
        }

        public void Remover(Guid Id)
        {
            using (var con = DB.GetConnection())
            {
                var query = "update Livro set Status = 0 where Id=@Id";
                con.Execute(query, new { Id });
            }
        }
    }
}
