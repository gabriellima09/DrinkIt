using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Dao
{
    public class PedidoDao : IDao<Pedido>
    {
        private StringBuilder Sql = new StringBuilder();

        public void Alterar(Pedido entidade)
        {
            throw new System.NotImplementedException();
        }

        public void Cadastrar(Pedido entidade)
        {
            Sql.Clear();

            Sql.Append("INSERT INTO PEDIDOS (");
            Sql.Append("ClienteId, ");
            Sql.Append("DataCadastro, ");
            Sql.Append("IdStatus, ");
            Sql.Append("DataUltimaAtualizacao, ");
            Sql.Append("IdCupom, ");
            Sql.Append("IdEnderecoEntrega, ");
            Sql.Append("IdCartao1, ");
            Sql.Append("IdCartao2, ");
            Sql.Append("ValorTotal, ");
            Sql.Append("Desconto, ");
            Sql.Append("Frete ");
            Sql.Append(")");
            Sql.Append("VALUES (");
            Sql.Append(entidade.IdCliente + ", ");
            Sql.Append("'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
            Sql.Append(0 + ", ");
            Sql.Append("'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
            Sql.Append(entidade.IdCupom + ", ");
            Sql.Append(entidade.IdEnderecoEntrega + ", ");
            Sql.Append(entidade.IdCartao1 + ", ");
            Sql.Append(entidade.IdCartao2 + ", ");
            Sql.Append(entidade.ValorTotal.ToString(new CultureInfo("en-US")) + ", ");
            Sql.Append(entidade.Desconto.ToString(new CultureInfo("en-US")) + ", ");
            Sql.Append(entidade.Frete.ToString(new CultureInfo("en-US")));
            Sql.Append(");");

            DbContext.ExecuteQuery(Sql.ToString());

            int lastInsertId = ObterUltimoIdInserido();

            Sql.Clear();

            foreach (var item in entidade.Bebidas)
            {
                Sql.Append("INSERT INTO PEDIDOSITENS (");
                Sql.Append("BebidaId, ");
                Sql.Append("PedidoId ");
                Sql.Append(")");
                Sql.Append("VALUES (");
                Sql.Append(item.Id + ", ");
                Sql.Append(lastInsertId);
                Sql.Append(");");

                DbContext.ExecuteQuery(Sql.ToString());
                Sql.Clear();
            }
        }

        public Pedido ConsultarPorId(int id)
        {
            Pedido pedido = new Pedido();

            Sql.Append("SELECT * FROM PEDIDOS WHERE Id = " + id);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    pedido = ObterEntidadeReader(reader);
                }
            }

            Sql.Clear();
            
            return pedido;
        }

        public List<Pedido> ConsultarPorCliente(int idCliente)
        {
            List<Pedido> pedidos = new List<Pedido>();

            Sql.Append("SELECT * FROM PEDIDOS WHERE ClienteId = " + idCliente);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    pedidos.Add(ObterEntidadeReader(reader));
                }
            }

            return pedidos;
        }


        public List<Pedido> ConsultarTodos()
        {
            List<Pedido> pedidos = new List<Pedido>();

            Sql.Append("SELECT * FROM PEDIDOS");

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    pedidos.Add(ObterEntidadeReader(reader));
                }
            }

            return pedidos;
        }

        public void Excluir(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Bebida> RetornarBebidasPedido(int pedidoId)
        {
            Sql.Clear();

            List<int> listIdBebidas = new List<int>();
            List<Bebida> bebidas = new List<Bebida>();

            Sql.Append("SELECT * FROM PEDIDOSITENS WHERE PedidoId = " + pedidoId);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                int i = 0;
                while (reader.Read())
                {
                    listIdBebidas.Add(reader.GetInt32(i));
                    i++;
                }
            }

            foreach (var id in listIdBebidas)
            {
                bebidas.Add(new BebidaDao().ConsultarPorId(id));
            }

            return bebidas;
        }

        public int ObterUltimoIdInserido()
        {
            int ID = 0;
            Sql.Clear();
            Sql.Append("SELECT MAX(ID) FROM PEDIDOS;");
            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    ID = reader.GetInt32(0);
                }
            }

            return ID;

        }

        public Pedido ObterEntidadeReader(IDataReader reader)
        {
            Pedido pedido = new Pedido
            {
                Cupom = new Cupom(),

                Id = Convert.ToInt32(reader["Id"]),
                IdCliente = Convert.ToInt32(reader["ClienteId"]),
                IdCartao1 = Convert.ToInt32(reader["IdCartao1"]),
                IdCartao2 = Convert.ToInt32(reader["IdCartao2"]),
                IdCupom = Convert.ToInt32(reader["IdCupom"]),
                IdEnderecoEntrega = Convert.ToInt32(reader["IdEnderecoEntrega"]),
                DataCadastro = Convert.ToDateTime(reader["DataCadastro"]),
                DataUltimaAtualizacao = Convert.ToDateTime(reader["DataUltimaAtualizacao"]),
                //Status = Convert.ToString(reader["Status"]),
                Frete = Convert.ToDecimal(reader["Frete"]),
                Desconto = Convert.ToDecimal(reader["Desconto"]),
                ValorTotal = Convert.ToDecimal(reader["ValorTotal"])
            };

            pedido.Cliente = new ClienteDao().ConsultarPorId(pedido.IdCliente);
            pedido.EnderecoEntrega = new EnderecoDao().ConsultarPorId(pedido.IdEnderecoEntrega);
            pedido.Cartao1 = new CartaoDao().ConsultarPorId(pedido.IdCartao1);
            pedido.Cartao2 = new CartaoDao().ConsultarPorId(pedido.IdCartao2);
            pedido.Cupom = new CupomDao().ConsultarPorId(pedido.IdCupom);
            pedido.Bebidas = RetornarBebidasPedido(pedido.Id);

            return pedido;
        }
    }
}