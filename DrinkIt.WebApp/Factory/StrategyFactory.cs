using DrinkIt.WebApp.Models;
using DrinkIt.WebApp.Strategy;
using System;
using System.Collections.Generic;

namespace DrinkIt.WebApp.Factory
{
    public static class StrategyFactory<T>
    {
        private static IList<IStrategy> List = new List<IStrategy>();        

        public static IList<IStrategy> GetStrategies()
        {
            Type type = typeof(T);

            if (type == typeof(Cliente))
            {
                List = new List<IStrategy>
                {
                    new ClienteStrategy(),
                    new CpfStrategy(),
                    new EnderecoStrategy(),
                    new CartaoStrategy()
                };
            }

            if (type == typeof(Endereco))
            {
                List = new List<IStrategy>
                {
                    new EnderecoStrategy()
                };
            }

            if (type == typeof(CartaoCredito))
            {
                List = new List<IStrategy>
                {
                    new CartaoStrategy()
                };
            }

            return List;
        }
    }
}