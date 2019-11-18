using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Strategy
{
    public class SenhaStrategy : IStrategy
    {
        public string Processar(EntidadeDominio entidade)
        {
            Cliente cliente = (Cliente)entidade;

            return Validar(cliente.Senha) ? string.Empty : "A senha precisa ter ao menos 8 dígitos, caracteres maiúsculos e minúsculos e símbolos.";
        }

        private bool Validar(string senha)
        {
            if (senha.Length < 8 || !senha.Any(c => char.IsUpper(c)) || !senha.Any(c => char.IsLower(c))
                    || Regex.Replace(senha, "[a-zA-Z0-9]", "").Length == 0)//Senha Inválida?
            {
                return false;
            }
            return true;
        }
    }
}