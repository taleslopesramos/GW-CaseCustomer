using CaseCustumer.Domain.Models.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseCustumer.App.Services.Interfaces
{
    public interface IEnderecoService
    {
        public Task<Endereco?> AutoComplete(string CEP, string Complemento, int Numero);
    }
}
