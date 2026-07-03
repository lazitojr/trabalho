using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Trabalho_Final
{
    public class LivroDAO
    {
        private Conexao conexao = new Conexao();


        public void Inserir(Livro livro)
        {
            using (MySqlConnection conn = conexao.AbrirConexao())
            {
                string sql = @"INSERT INTO livros
                              (titulo,autor,ano,disponivel)
                              VALUES
                              (@titulo,@autor,@ano,@disponivel)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@titulo", livro.Titulo);
                cmd.Parameters.AddWithValue("@autor", livro.Autor);
                cmd.Parameters.AddWithValue("@ano", livro.Ano);
                cmd.Parameters.AddWithValue("@disponivel", livro.Disponivel);

                cmd.ExecuteNonQuery();

                Console.WriteLine("Livro cadastrado com sucesso!");
            }
        }


        public List<Livro> Listar()
        {
            List<Livro> lista = new List<Livro>();

            using (MySqlConnection conn = conexao.AbrirConexao())
            {
                string sql = "SELECT * FROM livros";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Livro livro = new Livro();

                    livro.Id = Convert.ToInt32(reader["id"]);
                    livro.Titulo = reader["titulo"].ToString();
                    livro.Autor = reader["autor"].ToString();
                    livro.Ano = Convert.ToInt32(reader["ano"]);
                    livro.Disponivel = Convert.ToBoolean(reader["disponivel"]);

                    lista.Add(livro);
                }
            }

            return lista;
        }

        public Livro BuscarPorId(int id)
        {
            using (MySqlConnection conn = conexao.AbrirConexao())
            {
                string sql = "SELECT * FROM livros WHERE id=@id";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Livro livro = new Livro();

                    livro.Id = Convert.ToInt32(reader["id"]);
                    livro.Titulo = reader["titulo"].ToString();
                    livro.Autor = reader["autor"].ToString();
                    livro.Ano = Convert.ToInt32(reader["ano"]);
                    livro.Disponivel = Convert.ToBoolean(reader["disponivel"]);

                    return livro;
                }
            }

            return null;
        }


        public List<Livro> BuscarPorTitulo(string titulo)
        {
            List<Livro> lista = new List<Livro>();

            using (MySqlConnection conn = conexao.AbrirConexao())
            {
                string sql = "SELECT * FROM livros WHERE titulo LIKE @titulo";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@titulo", "%" + titulo + "%");

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Livro livro = new Livro();

                    livro.Id = Convert.ToInt32(reader["id"]);
                    livro.Titulo = reader["titulo"].ToString();
                    livro.Autor = reader["autor"].ToString();
                    livro.Ano = Convert.ToInt32(reader["ano"]);
                    livro.Disponivel = Convert.ToBoolean(reader["disponivel"]);

                    lista.Add(livro);
                }
            }

            return lista;
        }



        public void Atualizar(Livro livro)
        {
            using (MySqlConnection conn = conexao.AbrirConexao())
            {
                string sql = @"UPDATE livros
                               SET titulo=@titulo,
                                   autor=@autor,
                                   ano=@ano,
                                   disponivel=@disponivel
                               WHERE id=@id";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", livro.Id);
                cmd.Parameters.AddWithValue("@titulo", livro.Titulo);
                cmd.Parameters.AddWithValue("@autor", livro.Autor);
                cmd.Parameters.AddWithValue("@ano", livro.Ano);
                cmd.Parameters.AddWithValue("@disponivel", livro.Disponivel);

                cmd.ExecuteNonQuery();

                Console.WriteLine("Livro atualizado!");
            }
        }


        public void Excluir(int id)
        {
            using (MySqlConnection conn = conexao.AbrirConexao())
            {
                string sql = "DELETE FROM livros WHERE id=@id";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

                Console.WriteLine("Livro excluído!");
            }
        }


        public void AlterarDisponibilidade(int idLivro, bool disponivel)
        {
            using (MySqlConnection conn = conexao.AbrirConexao())
            {
                string sql = @"UPDATE livros
                               SET disponivel=@disponivel
                               WHERE id=@id";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", idLivro);
                cmd.Parameters.AddWithValue("@disponivel", disponivel);

                cmd.ExecuteNonQuery();
            }
        }

        internal void Excluir(Livro livroExcluir)
        {
            throw new NotImplementedException();
        }
    }
}