using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Trabalho_Final.DAO
{
    public class EmprestimoDAO
    {
        private Conexao conexao = new Conexao();
        private UsuarioDAO usuarioDAO = new UsuarioDAO();
        private LivroDAO livroDAO = new LivroDAO();


        public void RealizarEmprestimo(int idUsuario, int idLivro)
        {
            Pessoa usuario = usuarioDAO.BuscarPorId(idUsuario);
            Livro livro = livroDAO.BuscarPorId(idLivro);

            if (usuario == null)
            {
                Console.WriteLine("Usuário não encontrado!");
                return;
            }

            if (livro == null)
            {
                Console.WriteLine("Livro não encontrado!");
                return;
            }

            if (!livro.Disponivel)
            {
                Console.WriteLine("Livro indisponível.");
                return;
            }

            DateTime dataEmprestimo = DateTime.Now;
            DateTime dataPrevista =
                dataEmprestimo.AddDays(usuario.CalcularPrazoDevolucao());

            using (MySqlConnection conn = conexao.AbrirConexao())
            {
                string sql = @"INSERT INTO emprestimos
                               (id_usuario,id_livro,data_emprestimo,data_prevista)
                               VALUES
                               (@usuario,@livro,@emprestimo,@prevista)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@usuario", idUsuario);
                cmd.Parameters.AddWithValue("@livro", idLivro);
                cmd.Parameters.AddWithValue("@emprestimo", dataEmprestimo);
                cmd.Parameters.AddWithValue("@prevista", dataPrevista);

                cmd.ExecuteNonQuery();
            }

            livroDAO.AlterarDisponibilidade(idLivro, false);

            Console.WriteLine("Empréstimo realizado com sucesso!");
        }

        public void RegistrarDevolucao(int idEmprestimo)
        {
            using (MySqlConnection conn = conexao.AbrirConexao())
            {
                string buscar = "SELECT id_livro FROM emprestimos WHERE id=@id";

                MySqlCommand cmdBuscar =
                    new MySqlCommand(buscar, conn);

                cmdBuscar.Parameters.AddWithValue("@id", idEmprestimo);

                object resultado = cmdBuscar.ExecuteScalar();

                if (resultado == null)
                {
                    Console.WriteLine("Empréstimo não encontrado!");
                    return;
                }

                int idLivro = Convert.ToInt32(resultado);

                string sql = @"UPDATE emprestimos
                               SET data_devolucao=@data
                               WHERE id=@id";

                MySqlCommand cmd =
                    new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@data", DateTime.Now);
                cmd.Parameters.AddWithValue("@id", idEmprestimo);

                cmd.ExecuteNonQuery();

                livroDAO.AlterarDisponibilidade(idLivro, true);

                Console.WriteLine("Livro devolvido com sucesso!");
            }
        }


        public List<Emprestimo> Listar()
        {
            List<Emprestimo> lista = new List<Emprestimo>();

            using (MySqlConnection conn = conexao.AbrirConexao())
            {
                string sql = @"SELECT * FROM emprestimos";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Emprestimo emp = new Emprestimo();

                    emp.Id = Convert.ToInt32(reader["id"]);

                    emp.Usuario =
                        usuarioDAO.BuscarPorId(
                        Convert.ToInt32(reader["id_usuario"]));

                    emp.Livro =
                        livroDAO.BuscarPorId(
                        Convert.ToInt32(reader["id_livro"]));

                    emp.DataEmprestimo =
                        Convert.ToDateTime(reader["data_emprestimo"]);

                    emp.DataPrevista =
                        Convert.ToDateTime(reader["data_prevista"]);

                    if (reader["data_devolucao"] != DBNull.Value)
                        emp.DataDevolucao =
                            Convert.ToDateTime(reader["data_devolucao"]);

                    lista.Add(emp);
                }
            }

            return lista;
        }


        public List<Emprestimo> ListarEmAberto()
        {
            List<Emprestimo> lista = new List<Emprestimo>();

            using (MySqlConnection conn = conexao.AbrirConexao())
            {
                string sql =
                    "SELECT * FROM emprestimos WHERE data_devolucao IS NULL";

                MySqlCommand cmd =
                    new MySqlCommand(sql, conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Emprestimo emp = new Emprestimo();

                    emp.Id = Convert.ToInt32(reader["id"]);

                    emp.Usuario =
                        usuarioDAO.BuscarPorId(
                        Convert.ToInt32(reader["id_usuario"]));

                    emp.Livro =
                        livroDAO.BuscarPorId(
                        Convert.ToInt32(reader["id_livro"]));

                    emp.DataEmprestimo =
                        Convert.ToDateTime(reader["data_emprestimo"]);

                    emp.DataPrevista =
                        Convert.ToDateTime(reader["data_prevista"]);

                    lista.Add(emp);
                }
            }

            return lista;
        }

        internal IEnumerable<Emprestimo> ListarDevolvidos()
        {
            throw new NotImplementedException();
        }
    }
}