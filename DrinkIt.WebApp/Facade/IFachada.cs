using DrinkIt.WebApp.Models;
using System.Collections.Generic;

namespace DrinkIt.WebApp.Facade
{
    public interface IFachada<T> where T : EntidadeDominio
    {
        Resultado Cadastrar(T entidade);
        Resultado Alterar(T entidade);
        void Excluir(int id);
        T ConsultarPorId(int id);
        List<T> ConsultarTodos();
        List<string> ProcessarStrategies(EntidadeDominio entidade);
    }
}
