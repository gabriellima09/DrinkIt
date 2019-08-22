using DrinkIt.WebApp.Models;
using System.Collections.Generic;
using System.Data;

namespace DrinkIt.WebApp.Dao
{
    public interface IDao<T> where T : EntidadeDominio
    {
        void Cadastrar(T entidade);
        void Alterar(T entidade);
        void Excluir(int id);
        T ConsultarPorId(int id);
        List<T> ConsultarTodos();
        T ObterEntidadeReader(IDataReader reader);
    }
}
