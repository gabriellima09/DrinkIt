using DrinkIt.WebApp.Models;
using DrinkIt.WebApp.Strategy;
using System;
using System.Collections.Generic;

namespace DrinkIt.WebApp.Factory
{
    public static class StrategyFactory<T>
    {
        private static IList<IStrategy> List;

        static StrategyFactory()
        {
            List = new List<IStrategy>();
        }

        public static IList<IStrategy> GetStrategies()
        {
            Type type = typeof(T);

            if (type == typeof(Cliente))
            {
                List = new List<IStrategy>
                {
                    new CpfStrategy()
                };
            }

            return List;
        }
    }
}