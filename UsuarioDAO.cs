using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Trabalho_Final.DAO
{
    public class UsuarioDAO
    {
        private Conexao conexao = new Conexao();


        public void Inserir(Pessoa usuario)
        {
            using (MySqlConnection conn = conexao.AbrirConexao())
            {
                string sql = "INSERT INTO usuarios(nome,email,tipo) VALUES(@nome,@email,@tipo)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@email", usuario.Email);

                if (usuario is Aluno)
                    cmd.Parameters.AddWithValue("@tipo", "Aluno");
                else
                    cmd.Parameters.AddWithValue("@tipo", "Professor");

                cmd.ExecuteNonQuery();

                Console.WriteLine("Usuário cadastrado com sucesso!");
            }
        }


        public List<Pessoa> Listar()
        {
            List<Pessoa> lista = new List<Pessoa>();

            using (MySqlConnection conn = conexao.AbrirConexao())
            {
                string sql = "SELECT * FROM usuarios";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Pessoa usuario;

                    if (reader["tipo"].ToString() == "Aluno")
                        usuario = new Aluno();
                    else
                        usuario = new Professor();

                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.Nome = reader["nome"].ToString();
                    usuario.Email = reader["email"].ToString();

                    lista.Add(usuario);
                }
            }

            return lista;
        }


        public Pessoa BuscarPorId(int id)
        {
            using (MySqlConnection conn = conexao.AbrirConexao())
            {
                string sql = "SELECT * FROM usuarios WHERE id=@id";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Pessoa usuario;

                    if (reader["tipo"].ToString() == "Aluno")
                        usuario = new Aluno();
                    else
                        usuario = new Professor();

                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.Nome = reader["nome"].ToString();
                    usuario.Email = reader["email"].ToString();

                    return usuario;
                }
            }

            return null;
        }


        public void Atualizar(Pessoa usuario)
        {
            using (MySqlConnection conn = conexao.AbrirConexao())
            {
                string sql = @"UPDATE usuarios
                               SET nome=@nome,
                                   email=@email,
                                   tipo=@tipo
                               WHERE id=@id";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", usuario.Id);
                cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@email", usuario.Email);

                if (usuario is Aluno)
                    cmd.Parameters.AddWithValue("@tipo", "Aluno");
                else
                    cmd.Parameters.AddWithValue("@tipo", "Professor");

                cmd.ExecuteNonQuery();

                Console.WriteLine("Usuário atualizado!");
            }
        }


        public void Excluir(int id)
        {
            using (MySqlConnection conn = conexao.AbrirConexao())
            {
                string sql = "DELETE FROM usuarios WHERE id=@id";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

                Console.WriteLine("Usuário excluído!");
            }
        }

        internal void Excluir(Pessoa usuarioExcluir)
        {
            throw new NotImplementedException();
        }
    }
}