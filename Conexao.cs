using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Trabalho_Final
{
    public class Conexao
    {
        private string conexao = "server=localhost;database=biblioteca;user=root;password=@Aa12345678;";

        public MySqlConnection AbrirConexao()
        {
            MySqlConnection conn = new MySqlConnection(conexao);
            conn.Open();
            return conn;
        }


    }
}
