using DrinkIt.WebApp.Models;
using System;

namespace DrinkIt.WebApp.Strategy
{
    public class CartaoStrategy : IStrategy
    {
        public string Processar(EntidadeDominio entidade)
        {
            CartaoCredito cartao = (CartaoCredito)entidade;

            return Validar(cartao) ? string.Empty : "Cartão Inválido!";
        }

        public bool Validar(CartaoCredito cartao)
        {
            return ValidadorAuxiliar.ValidarPropriedadeVazia(cartao.NomeTitular)
                && (cartao.MesValidade > 0 && cartao.MesValidade < 13)
                && (cartao.AnoValidade >= DateTime.Now.Year)
                && (cartao.CodigoSeguranca > 0)
                && (cartao.Bandeira == Bandeira.Visa || cartao.Bandeira == Bandeira.Mastercard)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(cartao.Numero);
        }
    }
}