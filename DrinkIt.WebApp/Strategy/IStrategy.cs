namespace DrinkIt.WebApp.Strategy
{
    public interface IStrategy<T>
    {
        bool Processar(T entidade);
    }
}
