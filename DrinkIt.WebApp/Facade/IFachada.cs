using DrinkIt.WebApp.Models;
using DrinkIt.WebApp.Strategy;
using System.Collections.Generic;

namespace DrinkIt.WebApp.Facade
{
    public interface IFachada<T> where T : EntidadeDominio
    {
        void Cadastrar(T entidade);
        void Alterar(T entidade);
        void Excluir(int id);
        T ConsultarPorId(int id);
        List<T> ConsultarTodos();
        void ProcessarStrategies(EntidadeDominio entidade);
    }
}
