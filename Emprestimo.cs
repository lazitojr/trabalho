using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_Final
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public Pessoa Usuario {  get; set; }
        public Livro Livro { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataPrevista { get; set; }
        public DateTime DataDevolcao { get; set; }
        public object DataDevolucao { get; internal set; }
    }  
}
