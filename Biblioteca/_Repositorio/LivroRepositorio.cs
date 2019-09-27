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
                var query = "insert into Livro (Titulo, Imagem, Sinopse, Autor, Editora, Unidade, Genero, Habilitado) values (@Titulo, @Imagem, @Sinopse, @Autor, @Editora, @Unidade, @Genero, 1)";
                con.Execute(query, new { item });
            }
        }

        public void Editar(Livro item)
        {
            using(var con = DB.GetConnection())
            {
                var query = "update Livro set Titulo=@Titulo, Imagem=@Imagem, Sinopse=@Sinopse, Autor=@Autor, Editora=@Editora, Unidade=@Unidade, Genero=@Genero";
                con.Execute(query, new { item });
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
