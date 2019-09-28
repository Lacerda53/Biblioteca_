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
                            "            ,Titulo	 " +
                            "            ,Imagem	 " +
                            "            ,Sinopse	 " +
                            "            ,Autor		 " +
                            "            ,Editora	 " +
                            "            ,Unidade	 " +
                            "            ,Genero	 " +
                            "            ,Estoque	 " +
                            "            ,Habilitado)" +
                            "      VALUES				 " +
                            "            (@Id         " +
                            "            ,@Titulo     " +
                            "            ,@Imagem 	 " +
                            "            ,@Sinopse 	 " +
                            "            ,@Autor 	 " +
                            "            ,@Editora 	 " +
                            "            ,@Unidade 	 " +
                            "            ,@Genero 	 " +
                            "            ,@Unidade 	 " +
                            "            ,@Habilitado)";
                con.Execute(query, new { item.Id, item.Titulo, item.Imagem, item.Sinopse, item.Autor, item.Editora, item.Unidade, item.Estoque, item.Genero, item.Habilitado});
            }
        }

        public void Editar(Livro item)
        {
            using(var con = DB.GetConnection())
            {
                var query = "update Livro set Titulo=@Titulo, Sinopse=@Sinopse, Autor=@Autor, Editora=@Editora, Estoque=@Estoque, Unidade=@Unidade, Genero=@Genero where Id=@Id";
                con.Execute(query, item );
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
                var query = "select * from Livro where Habilitado = 1";
                return con.Query<Livro>(query);
            }
        }

        public void Remover(Guid Id)
        {
            using (var con = DB.GetConnection())
            {
                var query = "update Livro set Habilitado = 0 where Id=@Id";
                con.Execute(query, new { Id });
            }
        }
    }
}
