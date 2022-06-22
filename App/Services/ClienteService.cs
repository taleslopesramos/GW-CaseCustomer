using CaseCustumer.App.Services.Interfaces;
using CaseCustumer.Domain.DTO;
using CaseCustumer.Domain.Models.Cliente;
using CaseCustumer.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseCustumer.App.Services
{
    public class ClienteService : IClienteService
    {
        private ClienteDbContext _context { get; set; }
        private IEnderecoService _enderecoService { get; set; }
        public ClienteService(ClienteDbContext context, IEnderecoService enderecoService)
        {
            _context = context;
            _enderecoService = enderecoService;
        }
        public async Task<string> Create(Cliente cliente, string cep, string complemento, int numero)
        {
            cliente.DataCadastro = DateTime.Now;
            cliente.LimparProps();

            var clienteRepetido = _context
                .Clientes
                .FirstOrDefault(c => c.CPF == cliente.CPF);

            if (clienteRepetido != null)
                return "Já existe um Cliente cadastrado com esse CPF!";

            var endereco = await _enderecoService.AutoComplete(cep, complemento, numero);
            if (endereco == null)
                return "O CEP informado não existe";

            cliente.Endereco = endereco;

            var error = cliente.IsValido();

            if (error != "")
                return error;

            try
            {
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Update(int id, ClienteDTO cliente)
        {
            var clienteModel = _context.Clientes
                .Include(c => c.Endereco)
                .FirstOrDefault(c => c.Id == id);

            if (clienteModel == null)
                return "Cliente não encontrado";

            try
            {
                clienteModel.Update(cliente);
                _context.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
