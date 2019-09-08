using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Facade;
using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class BebidasController : Controller
    {

        private readonly IDao<Bebida> Dao;
        private readonly IFachada<Bebida> Fachada;

        public BebidasController()
        {
            Dao = new BebidaDao();
            Fachada = new Fachada<Bebida>(Dao);
        }

        // GET: Bebidas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PvBebida()
        {
            List<Bebida> bebidas = new List<Bebida>
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
                    Teor = "0%",
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
                    Status = "ATIVO"

                },
                new Bebida
                {
                    Id = 3,
                    Nome = "Café",
                    Descricao = "Café Preto",
                    Marca = "Pilão",
                    Valor = 1.99M,
                    Volume = "600ml",
                    Peso = "740mg",
                    Sabor = "Café",
                    Lote = "12421",
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now,
                    Fabricante = "Starbucks",
                    Embalagem = "Caixinha",
                    CodigoBarras = "662607004",
                    Alcoolico = false,
                    Teor = "0%",
                    Gaseificada = false,
                    ContemGluten = false,
                    Ingredientes = new List<Ingrediente>
                    {
                        new Ingrediente
                        {
                            Descricao = "Cafeína"
                        }
                    },
                    DicaConservacao = "Manter em local seco e arejado.",
                    Status = "ATIVO"
                }

            };

            return PartialView(Fachada.ConsultarTodos());
        }

        // GET: Bebidas/Details/5
        public ActionResult Details(int id)
        {
            Bebida bebida = new Bebida
            {
                Id = id,
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
                Status = "ATIVO",
                CaminhoImagem = "/Images/crystal.jpg"

            };
            return View(Fachada.ConsultarPorId(id));
        }

        // GET: Bebidas/Create
        public ActionResult Create()
        {            
            return View();
        }

        // POST: Bebidas/Create
        [HttpPost]
        public ActionResult Create(Bebida bebida, List<string> LstIngrediente)
        {
            try
            {
                bebida.Ingredientes = new List<Ingrediente>();
                if(LstIngrediente != null && LstIngrediente.Count > 0)
                {
                    foreach (var item in LstIngrediente)
                    {
                        Ingrediente i = new Ingrediente
                        {
                            Descricao = item
                        };

                        bebida.Ingredientes.Add(i);
                    }
                }
                bebida.TipoBebida = new TipoBebida
                {
                    Id = 1,
                    Descricao = "Refri"
                };

                bebida.CaminhoImagem = "caminho";

                Fachada.Cadastrar(bebida);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Bebidas/Edit/5
        public ActionResult Edit(int id)
        {
            Bebida bebida = new Bebida
            {
                Id = id,
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
            };


            return View(Fachada.ConsultarPorId(id));
        }

        // POST: Bebidas/Edit/5
        [HttpPost]
        public ActionResult Edit(Bebida bebida)
        {
            bebida.CaminhoImagem = "/Images/dolly.jpg";
            bebida.Ingredientes = new List<Ingrediente>();
            bebida.TipoBebida = new TipoBebida();
            try
            {
                // TODO: Add update logic here
                Fachada.Alterar(bebida);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }


        public ActionResult TrocarStatus(int id)
        {
            try
            {
                // TODO: Add update logic here
                Fachada.Excluir(id);
                return RedirectToAction("Index", "Usuarios", null);
            }
            catch(Exception ex)
            {
                return View();
            }
            //return new EmptyResult();
        }
        /*
        // GET: Bebidas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Bebidas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        */
        public ActionResult PvDashBebidas()
        {
            List<Bebida> bebidas = new List<Bebida>
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
                    Teor = "0%",
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
                    Status = "ATIVO"

                },

                new Bebida
                {
                    Id = 3,
                    Nome = "Café",
                    Descricao = "Café Preto",
                    Marca = "Pilão",
                    Valor = 1.99M,
                    Volume = "600ml",
                    Peso = "740mg",
                    Sabor = "Café",
                    Lote = "12421",
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now,
                    Fabricante = "Starbucks",
                    Embalagem = "Caixinha",
                    CodigoBarras = "662607004",
                    Alcoolico = false,
                    Teor = "0%",
                    Gaseificada = false,
                    ContemGluten = false,
                    Ingredientes = new List<Ingrediente>
                    {
                        new Ingrediente
                        {
                            Descricao = "Cafeína"
                        }
                    },
                    DicaConservacao = "Manter em local seco e arejado.",
                    Status = "ATIVO"

                }

            };

            return PartialView(bebidas);
        }
    }
}
