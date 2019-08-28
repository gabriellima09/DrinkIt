using System.Collections.Generic;
using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Factory;
using DrinkIt.WebApp.Models;
using DrinkIt.WebApp.Strategy;

namespace DrinkIt.WebApp.Facade
{
    public class Fachada<T> : IFachada<T> where T : EntidadeDominio
    {
        private readonly IDao<T> _dao;
        private readonly IList<IStrategy> _strategies;

        public Fachada(IDao<T> dao)
        {
            _dao = dao;
            _strategies = StrategyFactory<T>.GetStrategies();
        }

        public void Alterar(T entidade)
        {
            ProcessarStrategies(entidade);

            _dao.Alterar(entidade);
        }

        public void Cadastrar(T entidade)
        {
            ProcessarStrategies(entidade);

            _dao.Cadastrar(entidade);
        }

        public T ConsultarPorId(int id)
        {
            return _dao.ConsultarPorId(id);
        }

        public List<T> ConsultarTodos()
        {
            return _dao.ConsultarTodos();
        }

        public void Excluir(int id)
        {
            _dao.Excluir(id);
        }

        public void ProcessarStrategies(EntidadeDominio entidade)
        {
            foreach (IStrategy strategy in _strategies)
            {
                strategy.Processar(entidade);
            }
        }
    }
}