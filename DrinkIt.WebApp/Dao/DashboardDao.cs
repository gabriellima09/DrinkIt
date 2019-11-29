using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkIt.WebApp.Dao
{
    public class DashboardDao
    {
        private StringBuilder Sql = new StringBuilder();

        public Dashboard GetDashboardVendas(string inicio, string fim)
        {
            Dashboard dashboard = new Dashboard()
            {
                DataAlcoolico = new List<string>(),
                DataNaoAlcoolico = new List<string>(),
                ValoresAlcoolicos = new List<int>(),
                ValoresNaoAlcoolicos = new List<int>(),
                Top = new List<string>(),
                Qtd = new List<int>()
            };

            Sql.Clear();

            Sql.Append("select ");
            Sql.Append("count(p.id) Quantidade, ");
            Sql.Append("CONVERT(VARCHAR,DataCadastro,103) Data ");
            Sql.Append("from pedidos p ");
            Sql.Append("inner ");
            Sql.Append("join pedidositens pi on pi.pedidoid = p.id ");
            Sql.Append("inner ");
            Sql.Append("join bebidas b on b.id = pi.bebidaid ");
            Sql.Append("where Alcoolico = 1 ");
            Sql.Append("and(DataCadastro between '" + inicio + "' and '" + fim + "') ");
            Sql.Append("group by DataCadastro ");
            Sql.Append("order by ");
            Sql.Append("DataCadastro ");

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    dashboard.ValoresAlcoolicos.Add(Convert.ToInt32(reader["Quantidade"]));
                    dashboard.DataAlcoolico.Add(Convert.ToString(reader["Data"]));
                }
            }

            Sql.Clear();

            Sql.Append("select ");
            Sql.Append("count(p.id) Quantidade, ");
            Sql.Append("CONVERT(VARCHAR,DataCadastro,103) Data ");
            Sql.Append("from pedidos p ");
            Sql.Append("inner ");
            Sql.Append("join pedidositens pi on pi.pedidoid = p.id ");
            Sql.Append("inner ");
            Sql.Append("join bebidas b on b.id = pi.bebidaid ");
            Sql.Append("where Alcoolico = 0 ");
            Sql.Append("and(DataCadastro between '"+ inicio +"' and '" + fim +"') ");
            Sql.Append("group by DataCadastro ");
            Sql.Append("order by ");
            Sql.Append("DataCadastro ");

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    dashboard.ValoresNaoAlcoolicos.Add(Convert.ToInt32(reader["Quantidade"]));
                    dashboard.DataNaoAlcoolico.Add(Convert.ToString(reader["Data"]));
                }
            }

            Sql.Clear();

            Sql.Append("select distinct top 3 ");
            Sql.Append("tb.descricao, ");
            Sql.Append("count(p.id) as qtd ");
            Sql.Append("from pedidos p ");
            Sql.Append("inner ");
            Sql.Append("join pedidositens pt ");
            Sql.Append("on pt.PedidoId = p.id ");
            Sql.Append("inner ");
            Sql.Append("join bebidas b ");
            Sql.Append("on b.id = pt.BebidaId ");
            Sql.Append("inner ");
            Sql.Append("join TipoBebida tb ");
            Sql.Append("on tb.id = b.TipoBebida ");
            Sql.Append("group by tb.Descricao ");
            Sql.Append("order by 2 desc ");


            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    dashboard.Top.Add(Convert.ToString(reader["descricao"]));
                    dashboard.Qtd.Add(Convert.ToInt32(reader["qtd"]));
                }
            }

            if(dashboard.Qtd.Count >= 3)
            {
                dashboard.Qtd1 = dashboard.Qtd[0];
                dashboard.Qtd2 = dashboard.Qtd[1];
                dashboard.Qtd3 = dashboard.Qtd[2];

                dashboard.Top1 = dashboard.Top[0];
                dashboard.Top2 = dashboard.Top[1];
                dashboard.Top3 = dashboard.Top[2];
            }
            

            return dashboard;
        }

    }
}