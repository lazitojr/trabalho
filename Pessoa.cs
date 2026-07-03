using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_Final
{
    public  abstract class Pessoa
    {
       
        public object Id { get; internal set; }
        public string name { get; set; }
        public string email { get; set; }
        public string Email { get; internal set; }
        public string Nome { get; internal set; }

        public abstract int CalcularPrazoDevolucao();


    }
}
