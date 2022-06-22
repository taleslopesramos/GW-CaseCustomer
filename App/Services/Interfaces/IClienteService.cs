using CaseCustumer.Domain.DTO;
using CaseCustumer.Domain.Models.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseCustumer.App.Services.Interfaces
{
    public interface IClienteService
    {
        public Task<string> Create(Cliente cliente, string cep, string complemento, int numero);
        public string Update(int id, ClienteDTO cliente);
    }
}
