using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Strategy
{
    public interface IStrategy
    {
        bool Processar(EntidadeDominio entidade);
    }
}
