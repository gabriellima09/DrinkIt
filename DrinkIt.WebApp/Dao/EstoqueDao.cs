using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DrinkIt.WebApp.Dao
{
    public class EstoqueDao
    {
        private StringBuilder Sql = new StringBuilder();

        public List<Estoque> ConsultarTodos()
        {
            List<Estoque> lista = new List<Estoque>();

            Sql.Append("SELECT BEB.Nome, EST.* FROM ESTOQUE EST JOIN BEBIDAS BEB ON EST.IDBEBIDA = BEB.ID;");

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    Estoque e = new Estoque
                    {
                        IdBebida = Convert.ToInt32(reader["IdBebida"]),
                        Qtde = Convert.ToInt32(reader["Qtde"]),
                        DescBebida = Convert.ToString(reader["Nome"])
                    };

                    lista.Add(e);
                }
            }

            return lista;
        }

        public void Entrada(int IdBebida, int Qtde)
        {
            bool flgHaRegistro = false;
            Sql.Append("SELECT * FROM ESTOQUE WHERE IdBebida = " + IdBebida);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    flgHaRegistro = true;
                }
            }

            Sql.Clear();

            if (flgHaRegistro)
            {//UPDATE, POIS JÁ EXISTE LÁ
                int QtdeAtual = 0;
                Sql.Append("SELECT Qtde FROM Estoque WHERE IdBebida =" + IdBebida + ";");//PEGA QTDE ATUAL
                using (var reader = DbContext.ExecuteReader(Sql.ToString()))
                {
                    if (reader.Read())
                    {
                        QtdeAtual = reader.GetInt32(0);
                    }
                }
                Sql.Clear();
                QtdeAtual += Qtde;//SOMA
                Sql.Append("UPDATE ESTOQUE SET QTDE = " + QtdeAtual + " WHERE IDBEBIDA = " + IdBebida + ";");//ATUALIZA
                DbContext.ExecuteQuery(Sql.ToString());
            }
            else
            {//NÃO EXISTE LÁ, ENTÃO INSERT
                Sql.Append("INSERT INTO ESTOQUE (IDBEBIDA, QTDE) VALUES (" + IdBebida + ", " + Qtde + ");");//ATUALIZA
                DbContext.ExecuteQuery(Sql.ToString());
            }
        }

        //TRUE = BAIXA COM SUCESSO / FALSE = INSUFICIENTE NO ESTOQUE PARA BAIXA ou ITEM NÃO EXISTE
        public bool Baixa(int IdBebida, int Qtde)
        {
            bool flgHaRegistro = false;
            Sql.Append("SELECT * FROM ESTOQUE WHERE IdBebida = " + IdBebida);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    flgHaRegistro = true;
                }
            }

            if (!flgHaRegistro)//Item não existe no estoque?
                return false;

            Sql.Clear();

            int QtdeAtual = 0;
            Sql.Append("SELECT Qtde FROM Estoque WHERE IdBebida =" + IdBebida + ";");//PEGA QTDE ATUAL
            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    QtdeAtual = reader.GetInt32(0);
                }
            }

            Sql.Clear();

            if (QtdeAtual < Qtde)//Tem menos no estoque do que pode tirar?
                return false;

            QtdeAtual -= Qtde;//Subtrai
            Sql.Append("UPDATE ESTOQUE SET QTDE = " + QtdeAtual + " WHERE IDBEBIDA = " + IdBebida + ";");//ATUALIZA
            DbContext.ExecuteQuery(Sql.ToString());

            if (QtdeAtual == 0)//CASO O ESTOQUE SEJA ESVAZIADO, INATIVA A BEBIDA
            {
                Sql.Clear();
                Sql.Append("UPDATE BEBIDAS SET STATUS = 0 WHERE ID = " + IdBebida + ";");//ATUALIZA
                DbContext.ExecuteQuery(Sql.ToString());
            }

            return true;
        }

        public int ConsultarEstoquePorId(int IdBebida)
        {
            int qtde = 0;
            Sql.Append("SELECT QTDE FROM ESTOQUE WHERE IDBEBIDA = " + IdBebida);
            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    qtde = reader.GetInt32(0);
                }
            }

            return qtde;
        }
    }
}