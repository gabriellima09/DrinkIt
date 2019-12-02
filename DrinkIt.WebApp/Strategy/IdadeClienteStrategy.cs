using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Strategy
{
    public class IdadeClienteStrategy : IStrategy
    {
        public string Processar(EntidadeDominio entidade)
        {
            Cliente cliente = (Cliente)entidade;

            return Validar(cliente) ? string.Empty : "Você deve ser maior de idade para criar uma conta!";
        }

        private bool Validar(Cliente cliente)
        {
            return cliente.DataNascimento <= DateTime.Now.AddYears(-18);
        }
    }
}