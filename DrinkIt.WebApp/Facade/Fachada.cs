using System.Collections.Generic;
using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Facade
{
    public class Fachada<T> : IFachada<T> where T : EntidadeDominio
    {
        private readonly IDao<T> _dao;

        public Fachada(IDao<T> dao)
        {
            _dao = dao;
        }

        public void Alterar(T entidade)
        {
            _dao.Alterar(entidade);
        }

        public void Cadastrar(T entidade)
        {
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
    }
}