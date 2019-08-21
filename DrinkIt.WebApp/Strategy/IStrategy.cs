using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkIt.WebApp.Strategy
{
    public interface IStrategy<T>
    {
        bool Processar(T entidade);
    }
}
