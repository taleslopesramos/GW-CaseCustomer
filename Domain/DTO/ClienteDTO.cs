using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseCustumer.Domain.DTO
{
    public class ClienteDTO
    {
        public string Nome { get; set; } = "";
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; } = "";
        public string Rg { get; set; } = "";
        public string Cep { get; set; } = "";
        public string Complemento { get; set; } = "";
        public int Numero { get; set; } 
    }
}
