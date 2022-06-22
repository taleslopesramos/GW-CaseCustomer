using CaseCustumer.Domain.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CaseCustumer.Domain.Models.Cliente
{
    public class Cliente : BaseEntity
    {
        [StringLength(200)]
        public string Nome { get; set; } = "";
        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DataNascimento { get; set; }
        [Display(Name = "Data de Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DataCadastro { get; set; }
        public Endereco Endereco { get; set; }
        public bool IsAtivo { get; set; } = true;

        [StringLength(20)]
        public string CPF { get; set; } = "";
        [StringLength(20)]
        public string RG { get; set; } = "";

        public Cliente(ClienteDTO cliente)
        {
            Nome = cliente.Nome;
            DataNascimento = cliente.DataNascimento;
            CPF = cliente.Cpf;
            RG = cliente.Rg;

            var cepNum = int.Parse(Regex.Replace(cliente.Cep.Trim(), "[^0-9,]", ""));

            Endereco = new()
            {
                CEP = cepNum,
                Complemento = cliente.Complemento,
                Numero = cliente.Numero,
            };
        }

        public Cliente()
        {
            Endereco = new();
        }

        public void Update(ClienteDTO cliente)
        {
            Nome = cliente.Nome != "" ? cliente.Nome : Nome;
            DataNascimento = cliente.DataNascimento != DateTime.MinValue ? cliente.DataNascimento : DataNascimento;
            CPF = cliente.Cpf != "" ? cliente.Cpf : CPF;
            RG = cliente.Rg != "" ? cliente.Rg : RG;
            int cepNum;

            try
            {
                cepNum = int.Parse(Regex.Replace(cliente.Cep.Trim(), "[^0-9,]", ""));
            }
            catch { cepNum = 0; }

            Endereco = new()
            {
                CEP = cepNum != 0 ? cepNum : Endereco.CEP,
                Complemento = cliente.Complemento != "" ? cliente.Complemento : Endereco.Complemento,
                Numero = cliente.Numero != 0 ? cliente.Numero : Endereco.Numero,
            };
        }

        public void LimparProps()
        {
            CPF = Regex.Replace(CPF.Trim(), "[^0-9,]", "");
            RG = Regex.Replace(RG.Trim().ToUpper(), "[^0-9A-Z]", "");
            Nome = Nome.Trim();
        }

        public string IsValido()
        {
            LimparProps();

            if (!IsCPFValido())
                return "CPF inválido";

            if (Nome.Length < 2)
                return "Nome deve ter no mínimo 2 caracteres";

            return Endereco.IsValido();
        }

        public bool IsCPFValido()
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            LimparProps();

            if (CPF.Length != 11)
                return false;
            tempCpf = CPF.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return CPF.EndsWith(digito);
        }
    }
}
