using CaseCustumer.App.Services.Interfaces;
using CaseCustumer.Domain.DTO;
using CaseCustumer.Domain.Models.Cliente;
using CaseCustumer.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaseCustumer.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private IClienteService _clienteService;
        private ClienteDbContext _clienteDb;
        public ClienteController(IClienteService clienteService, ClienteDbContext clienteDb)
        {
            _clienteService = clienteService;
            _clienteDb = clienteDb;
        }

        [HttpGet]
        public IEnumerable<Cliente> Get(int? id)
        {
            IEnumerable<Cliente> clientes;
            
            if (id == null)
            {
                clientes = _clienteDb.Clientes
                    .Include(c => c.Endereco)
                    .ToList();
            }else
            {
                clientes = _clienteDb.Clientes
                    .Include(c => c.Endereco)
                    .Where(c => c.Id == id);
            }

            return clientes;
        }

        [HttpPost]
        public async Task<string> Post(ClienteDTO cliente)
        {
            Cliente clienteModel = new(cliente);
            try
            {
                var errorMsg = await _clienteService.Create(clienteModel, cliente.Cep, cliente.Complemento, cliente.Numero);

                if (errorMsg != "")
                {
                    Response.StatusCode = 400;
                    return errorMsg;
                }

                return "Cliente cadastrado com sucesso";
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return ex.Message;
            }
        }
        [HttpDelete]
        public string Delete(int id)
        {
            var cliente = _clienteDb.Clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
            {
                Response.StatusCode = 404;
                return "Cliente inexistente";
            }

            try
            {
                _clienteDb.Clientes.Remove(cliente);
                _clienteDb.SaveChanges();
                return "Cliente apagado com sucesso";
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return ex.Message;
            }
        }

        [HttpPut]
        public string Update(int id, ClienteDTO cliente)
        {
            if(_clienteDb.Clientes.Find(id) == null)
            {
                Response.StatusCode = 404;
                return "Cliente não encontrado.";
            }

            try
            {
                var errorMsg = _clienteService.Update(id, cliente);

                if (errorMsg != "")
                {
                    Response.StatusCode = 400;
                    return errorMsg;
                }

                return "Cliente atualizado com sucesso";
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return ex.Message;
            }
        }
    }
}
