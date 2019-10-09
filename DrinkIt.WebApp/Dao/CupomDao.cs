using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace DrinkIt.WebApp.Dao
{
    public class CupomDao : IDao<Cupom>
    {
        private StringBuilder Sql = new StringBuilder();

        public void Alterar(Cupom entidade)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Cupom entidade)
        {
            
        }

        public Cupom ConsultarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Cupom> ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public Cupom ObterEntidadeReader(IDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}