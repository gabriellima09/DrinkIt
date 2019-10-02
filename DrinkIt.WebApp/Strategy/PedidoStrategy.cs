using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Strategy
{
    public class PedidoStrategy : IStrategy
    {
        public bool Processar(EntidadeDominio entidade)
        {
            return true;
        }
    }
}