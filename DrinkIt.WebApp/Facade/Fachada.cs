using System.Collections.Generic;
using System.Linq;
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
        private Resultado Resultado;

        public Fachada(IDao<T> dao)
        {
            _dao = dao;
            _strategies = StrategyFactory<T>.GetStrategies();
        }

        public Resultado Alterar(T entidade)
        {
            Resultado = new Resultado();

            List<string> list = ProcessarStrategies(entidade);

            if (!list.Any())
            {
                _dao.Alterar(entidade);

                Resultado.Sucesso = true;
            }
            else
            {
                Resultado.Sucesso = false;
                Resultado.MensagensErro = list;
            }

            return Resultado;
        }

        public Resultado Cadastrar(T entidade)
        {
            Resultado = new Resultado();

            List<string> list = ProcessarStrategies(entidade);

            if (!list.Any())
            {
                _dao.Cadastrar(entidade);

                Resultado.Sucesso = true;
            }
            else
            {
                Resultado.Sucesso = false;
                Resultado.MensagensErro = list;
            }

            return Resultado;
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

        public List<string> ProcessarStrategies(EntidadeDominio entidade)
        {
            List<string> list = new List<string>();

            foreach (IStrategy strategy in _strategies)
            {
                string retorno = strategy.Processar(entidade);

                if (!string.IsNullOrWhiteSpace(retorno))
                {
                    list.Add(retorno);
                }
            }

            return list;
        }
    }
}