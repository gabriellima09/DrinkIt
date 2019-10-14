using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Strategy
{
    public interface IStrategy
    {
        string Processar(EntidadeDominio entidade);
    }
}
