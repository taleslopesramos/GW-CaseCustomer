using CaseCustumer.App.Services.Interfaces;
using CaseCustumer.Domain.DTO;
using CaseCustumer.Domain.Models.Cliente;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CaseCustumer.App.Services
{
    public class EnderecoService : IEnderecoService
    {
        public async Task<Endereco?> AutoComplete(string CEP, string Complemento, int Numero)
        {
            CEP = Regex.Replace(CEP.Trim(), "[^0-9,]", "");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://viacep.com.br/ws/");
            
            var res = await client.GetAsync(CEP + "/json");
            
            if (!res.IsSuccessStatusCode)
                return null;

            var enderecoDTO = JsonConvert.DeserializeObject<EnderecoCepDTO>(res.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            if (enderecoDTO == null)
                return null;

            return new Endereco(enderecoDTO, Numero, Complemento);
        }
    }
}
