using CaseCustumer.Domain.DTO;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CaseCustumer.Domain.Models.Cliente
{
    public class Endereco : BaseEntity
    {
        public Cliente? Cliente { get; set; }
        public int ClienteId { get; set; }
        public int CEP { get; set; }

        [StringLength(500)]
        public string Logradouro { get; set; } = "";

        [Display(Name = "Número")]
        [StringLength(100)]
        public int Numero { get; set; }

        [StringLength(500)]
        public string Complemento { get; set; } = "";

        [StringLength(500)]
        public string Bairro { get; set; } = "";

        [Display(Name = "Município")]
        [StringLength(150)]
        public string Municipio { get; set; } = "";

        [Display(Name = "Estado")]
        [StringLength(2)]
        public string UF { get; set; } = "";

        public Endereco(EnderecoCepDTO end, int numero, string complemento)
        {
            Numero = numero;
            Complemento = complemento;
            CEP = int.Parse(Regex.Replace(end.cep.Trim(), "[^0-9,]", ""));
            UF = end.uf;
            Bairro = end.bairro;
            Municipio = end.localidade;
            Logradouro = end.logradouro;
        }

        public Endereco(){ }

        public string IsValido()
        {
            if (CEP.ToString().Length < 8)
                return "CEP deve conter no mínimo 8 dígitos";

            return "";
        }
    }
}