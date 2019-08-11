using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedidos
        public ActionResult PvPedido()
        {
            List<Pedido> pedidos = new List<Pedido>
            {
                new Pedido
                {
                    Data = DateTime.Now,
                    Status = "Finalizado",
                    Id = 1,
                    Cliente = new Cliente
                    {
                        Id = 1,
                        Email = "teste@teste.com.br",
                        Login = "Teste",
                        Senha = "Teste",
                        Nome = "Usuário Teste"
                    },
                    Bebidas = new List<Bebida>
                    {
                        new Bebida
                        {
                            Id = 1,
                            Nome = "Crystal",
                            Descricao = "Água Mineral sem Gás",
                            Marca = "Crystal",
                            Valor = 1.99M,
                            Volume = "1.5L",
                            Peso = "1KG",
                            Sabor = "---",
                            Lote = "12321",
                            DataFabricacao = DateTime.Now,
                            DataValidade = DateTime.Now,
                            Fabricante = "Coca-Cola",
                            Embalagem = "Garrafa",
                            CodigoBarras = "662607004",
                            Alcoolico = false,
                            Teor = "0%",
                            Gaseificada = false,
                            ContemGluten = false,
                            Ingredientes = new List<Ingrediente>
                            {
                                new Ingrediente
                                {
                                    Descricao = "H2O"
                                }
                            },
                            DicaConservacao = "Beba água",
                            Status = "ATIVO"
                        }
                    }
                }
            };

            return PartialView(pedidos);
        }

        public ActionResult Details(int id)
        {
            Pedido pedido = new Pedido
            {
                Data = DateTime.Now,
                Status = "Finalizado",
                Id = 1,
                Cliente = new Cliente
                {
                    Id = 1,
                    Email = "teste@teste.com.br",
                    Login = "Teste",
                    Senha = "Teste",
                    Nome = "Usuário Teste"
                },
                Bebidas = new List<Bebida>
                    {
                        new Bebida
                        {
                            Id = 1,
                            Nome = "Crystal",
                            Descricao = "Água Mineral sem Gás",
                            Marca = "Crystal",
                            Valor = 1.99M,
                            Volume = "1.5L",
                            Peso = "1KG",
                            Sabor = "---",
                            Lote = "12321",
                            DataFabricacao = DateTime.Now,
                            DataValidade = DateTime.Now,
                            Fabricante = "Coca-Cola",
                            Embalagem = "Garrafa",
                            CodigoBarras = "662607004",
                            Alcoolico = false,
                            Teor = "0%",
                            Gaseificada = false,
                            ContemGluten = false,
                            Ingredientes = new List<Ingrediente>
                            {
                                new Ingrediente
                                {
                                    Descricao = "H2O"
                                }
                            },
                            DicaConservacao = "Beba água",
                            Status = "ATIVO"
                        }
                    }
            };

            return View(pedido);
        }
    }
}