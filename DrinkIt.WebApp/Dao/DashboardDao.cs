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
                ValoresNaoAlcoolicos = new List<int>()
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

            return dashboard;
        }

    }
}