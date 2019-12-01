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
            try
            {
                decimal valorTotal = (entidade.ValorTotal + entidade.Frete - entidade.Cupom.Valor);
                if (valorTotal < 0)
                    valorTotal = 0;

                Sql.Clear();

                Sql.Append("INSERT INTO PEDIDOS (");
                Sql.Append("ClienteId, ");
                Sql.Append("DataCadastro, ");
                Sql.Append("IdCupom, ");
                Sql.Append("IdEnderecoEntrega, ");
                Sql.Append("IdCartao1, ");
                Sql.Append("IdCartao2, ");
                Sql.Append("ValorCartao1, ");
                Sql.Append("ValorCartao2, ");
                Sql.Append("ValorTotal, ");
                Sql.Append("Desconto, ");
                Sql.Append("Frete ");
                Sql.Append(")");
                Sql.Append("VALUES (");
                Sql.Append(entidade.IdCliente + ", ");
                Sql.Append("'" + DateTime.Now.ToString("yyyy-MM-dd") + "', ");
                //Sql.Append("'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
                Sql.Append(entidade.IdCupom + ", ");
                Sql.Append(entidade.IdEnderecoEntrega + ", ");
                Sql.Append(entidade.IdCartao1 + ", ");
                Sql.Append(entidade.IdCartao2 + ", ");
                Sql.Append(entidade.ValorCartao1.ToString(new CultureInfo("en-US")) + ", ");
                Sql.Append(entidade.ValorCartao2.ToString(new CultureInfo("en-US")) + ", ");
                Sql.Append(valorTotal.ToString(new CultureInfo("en-US")) + ", ");
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
                    Sql.Append("Quantidade, ");
                    Sql.Append("PedidoId ");
                    Sql.Append(")");
                    Sql.Append("VALUES (");
                    Sql.Append(item.Id + ", ");
                    Sql.Append(item.Quantidade + ", ");
                    Sql.Append(lastInsertId);
                    Sql.Append(");");

                    DbContext.ExecuteQuery(Sql.ToString());
                    Sql.Clear();
                }

                if(entidade.Cupom.Valor > entidade.ValorTotal)
                {
                    decimal valorCupom = entidade.Cupom.Valor - entidade.ValorTotal;
                    string descricaoCupom = "TROCO" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

                    //Gerando novo cupom:
                    Sql.Append("INSERT INTO CUPONS(DESCRICAO, IDTIPO, DTCRIACAO, DTEXPIRACAO, ATIVO, VALOR) ");
                    Sql.Append("VALUES ('");
                    Sql.Append(descricaoCupom);
                    Sql.Append("', 3, '");
                    Sql.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '");
                    Sql.Append(DateTime.Now.AddDays(30).ToString("yyyy-MM-dd HH:mm:ss") + "', ");
                    Sql.Append("1, ");
                    Sql.Append(valorCupom.ToString(new CultureInfo("en-US")));
                    Sql.Append(");");

                    DbContext.ExecuteQuery(Sql.ToString());
                    Sql.Clear();


                    //Pegando ID do cupom recém-criado:                
                    int IdCupom = 0;
                    Sql.Clear();
                    Sql.Append("SELECT MAX(ID) FROM CUPONS;");
                    using (var reader = DbContext.ExecuteReader(Sql.ToString()))
                    {
                        if (reader.Read())
                        {
                            IdCupom = reader.GetInt32(0);
                        }
                    }


                    //Registrando o cupom no cliente do pedido:
                    Sql.Clear();
                    Sql.Append("INSERT INTO CUPONSCLIENTE VALUES (");
                    Sql.Append(entidade.Cliente.Id +", ");
                    Sql.Append(IdCupom + ");");
                    DbContext.ExecuteQuery(Sql.ToString());
                    Sql.Clear();

                }

            }
            catch(Exception ex)
            {
                throw ex;
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

        public Pedido ConsultarPorIdSolicitacaoItens(int pedidoId)
        {
            try
            {
                Pedido pedido = new Pedido
                {
                    Id = pedidoId,
                    Bebidas = new List<Bebida>(),
                    Cliente = new Cliente(),
                    ValorTotal = 0
                };
                int idSolicitacao = 0;

                Sql.Append("select Id, IdCliente from SolicitacoesTroca WHERE IDPEDIDO = " + pedidoId);

                using (var reader = DbContext.ExecuteReader(Sql.ToString()))
                {
                    if (reader.Read())
                    {
                        idSolicitacao = Convert.ToInt32(reader["Id"]);
                        pedido.Cliente.Id = Convert.ToInt32(reader["IdCliente"]);
                    }
                }

                Sql.Clear();

                Sql.Append("SELECT S.IdBebida, S.Qtde, B.Valor, P.MargemLucro FROM SOLICITACOESITENS S ");
                Sql.Append("JOIN BEBIDAS B ON S.IDBEBIDA = B.Id ");
                Sql.Append("JOIN TipoBebida T ON T.Id = B.TipoBebida ");
                Sql.Append("JOIN Precificacao P ON T.IdPrecificacao = P.IdGrupo ");
                Sql.Append("WHERE IDSOLICITACAO = " + idSolicitacao);
                using (var reader = DbContext.ExecuteReader(Sql.ToString()))
                {
                    while (reader.Read())
                    {
                        Bebida b = new Bebida();
                        b.Id = Convert.ToInt32(reader["IdBebida"]);
                        b.Quantidade = Convert.ToInt32(reader["Qtde"]);
                        b.Valor = Convert.ToDecimal(reader["Valor"]);
                        b.MargemLucro = Convert.ToDecimal(reader["MargemLucro"]);
                        pedido.Bebidas.Add(b);
                    }
                }

                foreach(var item in pedido.Bebidas)
                {
                    item.ValorVenda = Math.Round(item.Valor + (item.MargemLucro / 100), 2);
                    pedido.ValorTotal += item.ValorVenda * item.Quantidade;
                }

                return pedido;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
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

            Sql.Append("SELECT BebidaId FROM PEDIDOSITENS WHERE PedidoId = " + pedidoId);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                int i = 0;
                while (reader.Read())
                {
                    listIdBebidas.Add(Convert.ToInt32(reader["BebidaId"]));
                    i++;
                }
            }

            foreach (var id in listIdBebidas)
            {
                Bebida bebida = new BebidaDao().ConsultarPorId(id);
                bebida.Quantidade = GetQuantidade(id, pedidoId);

                bebidas.Add(bebida);
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
                Id = Convert.ToInt32(reader["Id"]),
                IdCliente = Convert.ToInt32(reader["ClienteId"]),
                IdCartao1 = Convert.ToInt32(reader["IdCartao1"]),
                IdCartao2 = Convert.ToInt32(reader["IdCartao2"]),
                ValorCartao1 = Convert.ToDecimal(reader["ValorCartao1"]),
                ValorCartao2 = Convert.ToDecimal(reader["ValorCartao2"]),
                IdCupom = Convert.ToInt32(reader["IdCupom"]),
                IdEnderecoEntrega = Convert.ToInt32(reader["IdEnderecoEntrega"]),
                DataCadastro = Convert.ToDateTime(reader["DataCadastro"]),
                Status = GetStatus(Convert.ToInt32(reader["Id"])),
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

        public List<Status> GetStatus(int pedidoId)
        {
            List<Status> listStatus = new List<Status>();

            Sql.Clear();

            Sql.Append("select * from PedidosHistorico p inner join Pedidosstatus ps on ps.Id = p.IdStatus where p.IdPedido = " + pedidoId + " order by p.id desc");

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    Status status = new Status();

                    status.Id = Convert.ToInt32(reader["IdStatus"]);
                    status.Descricao = Convert.ToString(reader["Descricao"]);
                    status.DataAtualizacao = Convert.ToDateTime(reader["DataAtualizacao"]);

                    listStatus.Add(status);
                }
            }

            return listStatus;
        }

        public int GetQuantidade(int bebidaId, int pedidoId)
        {
            int quantidade = 0;

            Sql.Clear();

            Sql.Append("SELECT QUANTIDADE from PedidosItens pis INNER JOIN PEDIDOS P ON pis.PEDIDOID = P.ID inner join bebidas b on pis.BebidaId = b.id where pis.BebidaId = " + bebidaId);
            Sql.Append(" and p.id = " + pedidoId);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    quantidade = Convert.ToInt32(reader["Quantidade"]);
                }
            }

            return quantidade;
        }

    }
}