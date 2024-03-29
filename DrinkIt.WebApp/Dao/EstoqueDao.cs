﻿using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public void Entrada(int IdBebida, int Qtde, string Fornecedor, decimal VlrCusto, DateTime DtEntrada)
        {
            try
            {
                Sql.Clear();
                decimal vlrCustoAtual = 0.0M;
                int QtdeAtual = 0;
                bool reativar = false;
                int statusAtual = 0;
                int categoriaAtual = 0;

                Sql.Append("SELECT Qtde FROM Estoque WHERE IdBebida = " + IdBebida + ";");//PEGA QTDE ATUAL
                using (var reader = DbContext.ExecuteReader(Sql.ToString()))
                {
                    if (reader.Read())
                    {
                        QtdeAtual = reader.GetInt32(0);
                    }
                }

                Sql.Clear();
                Sql.Append("SELECT Status, Categoria FROM Bebidas WHERE Id = " + IdBebida + ";");//PEGA QTDE ATUAL
                using (var reader = DbContext.ExecuteReader(Sql.ToString()))
                {
                    if (reader.Read())
                    {
                        statusAtual = Convert.ToInt32(reader["Status"]);
                        categoriaAtual = Convert.ToInt32(reader["Categoria"]);
                    }
                }

                Sql.Clear();
                if(QtdeAtual == 0 && statusAtual == 0 && categoriaAtual == 2)//Reativar um item inativo fora de mercado?
                {
                    reativar = true;
                }

                QtdeAtual += Qtde;//SOMA
                Sql.Append("UPDATE ESTOQUE SET QTDE = " + QtdeAtual + " WHERE IDBEBIDA = " + IdBebida + ";");//ATUALIZA
                DbContext.ExecuteQuery(Sql.ToString());

                Sql.Clear();//REGISTRA NO HISTÓRICO
                Sql.Append("INSERT INTO HistoricoEstoque VALUES (" + IdBebida + ", " + Qtde + ", '" + Fornecedor + "', " + VlrCusto.ToString(new CultureInfo("en-US")) + ", '" + DtEntrada.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                DbContext.ExecuteQuery(Sql.ToString());

                Sql.Clear();
                Sql.Append("SELECT Valor FROM BEBIDAS WHERE ID = " + IdBebida);
                using (var reader = DbContext.ExecuteReader(Sql.ToString()))
                {
                    if (reader.Read())
                    {
                        vlrCustoAtual = reader.GetDecimal(0);
                    }
                }

                if (VlrCusto > vlrCustoAtual)
                {
                    Sql.Clear();
                    Sql.Append("UPDATE BEBIDAS SET VALOR = " + VlrCusto.ToString(new CultureInfo("en-US")) + " WHERE ID = " + IdBebida);
                    DbContext.ExecuteQuery(Sql.ToString());
                }

                if (reativar)
                {
                    Sql.Clear();
                    Sql.Append("UPDATE BEBIDAS SET STATUS = 1, CATEGORIA = 1 WHERE ID = " + IdBebida);
                    DbContext.ExecuteQuery(Sql.ToString());

                    Sql.Clear();
                    new BebidaDao().GravarMotivoInativacao(IdBebida, "O estoque do item recebeu entrada.", "Automática", "Ativação");
                }


            }
            catch (Exception ex)
            {
                throw ex;
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
                Sql.Append("UPDATE BEBIDAS SET STATUS = 0, CATEGORIA = 2 WHERE ID = " + IdBebida + ";");//ATUALIZA
                DbContext.ExecuteQuery(Sql.ToString());

                new BebidaDao().GravarMotivoInativacao(IdBebida, "O estoque do item zerou.", "Automática", "Inativacao");
            }

            return true;
        }

        public int ConsultarEstoquePorId(int IdBebida)
        {
            Sql.Clear();
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

        public List<Estoque> ConsultarHistoricoEstoque()
        {
            List<Estoque> lista = new List<Estoque>();
            Sql.Clear();
            Sql.Append("SELECT B.Nome, H.* FROM HistoricoEstoque H JOIN BEBIDAS B ON B.Id = H.IdBebida ORDER BY DTENTRADA DESC");
            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    Estoque estoque = new Estoque
                    {
                        DescBebida = Convert.ToString(reader["Nome"]),
                        IdBebida = Convert.ToInt32(reader["IdBebida"]),
                        Qtde = Convert.ToInt32(reader["Qtde"]),
                        Fornecedor = Convert.ToString(reader["Fornecedor"]),
                        VlrCusto = Convert.ToDecimal(reader["VlrCusto"]),
                        DtEntrada = Convert.ToDateTime(reader["DtEntrada"])
                    };
                    lista.Add(estoque);
                }
            }

            return lista;
        }

        public bool VerificaItensDisponiveis(List<Bebida> bebidas)
        {
            bool OK = true;
            int QtdeAtual = 0;

            foreach(var bebida in bebidas)
            {
                Sql.Clear();
                Sql.Append("SELECT QTDE FROM ESTOQUE WHERE IdBebida = " + bebida.Id);
                using (var reader = DbContext.ExecuteReader(Sql.ToString()))
                {
                    if (reader.Read())
                    {
                        QtdeAtual = reader.GetInt32(0);
                    }
                }

                if (QtdeAtual < bebida.Quantidade)
                    OK = false;
            }            

            return OK;
        }
    }
}