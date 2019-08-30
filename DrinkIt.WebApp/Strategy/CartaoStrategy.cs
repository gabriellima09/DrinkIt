using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Strategy
{
    public class CartaoStrategy : IStrategy
    {
        public bool Processar(EntidadeDominio entidade)
        {
            CartaoCredito cartao = (CartaoCredito)entidade;

            return ValidadorAuxiliar.ValidarPropriedadeVazia(cartao.Bandeira)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(cartao.NomeTitular)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(cartao.Numero)
                && cartao.CodigoSeguranca > 99;
        }
    }
}