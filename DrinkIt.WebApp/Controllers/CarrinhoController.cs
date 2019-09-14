using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class CarrinhoController : Controller
    {
        // GET: Carrinho
        public ActionResult Index()
        {
            Carrinho carrinho = new Carrinho
            {
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
                    Teor = 0,
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
                    Status = true

                },
                new Bebida
                {
                    Id = 2,
                    Nome = "Soda",
                    Descricao = "Soda Limonada",
                    Marca = "Soda",
                    Valor = 1.99M,
                    Volume = "350ml",
                    Peso = "350mg",
                    Sabor = "Limão",
                    Lote = "12321",
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now,
                    Fabricante = "Nem sei",
                    Embalagem = "Lata",
                    CodigoBarras = "662607004",
                    Alcoolico = false,
                    Teor = 0,
                    Gaseificada = true,
                    ContemGluten = false,
                    Ingredientes = new List<Ingrediente>
                    {
                        new Ingrediente
                        {
                            Descricao = "Soda"
                        }
                    },
                    DicaConservacao = "Beba mais água",
                    Status = true

                },
                }
            };

            return View(carrinho);
        }
    }
}