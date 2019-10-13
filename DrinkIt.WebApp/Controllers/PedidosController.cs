using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Models;
using Newtonsoft.Json;
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
            int idCliente = ((Usuario)Session["Usuario"]).Id;

            List<Pedido> pedidos = new List<Pedido>();

            if (idCliente == 0)
            {
                pedidos = new PedidoDao().ConsultarTodos();
            }
            else
            {
                pedidos = new PedidoDao().ConsultarPorCliente(idCliente);
            }

            return PartialView(pedidos);
        }

        public ActionResult Checkout()
        {
            var listaEnderecos = new EnderecoDao().ConsultarTodos();
            var listaCartoes = new CartaoDao().ConsultarTodos();

            List<SelectListItem> ddlEnderecos = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Selecione uma endereço ...",
                    Value = "0"
                }
            };
            List<SelectListItem> ddlCartoes = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Selecione uma cartão ...",
                    Value = "0"
                }
            };

            listaEnderecos.ForEach(x => ddlEnderecos.Add(new SelectListItem
            {
                Text = string.Concat(x.Descricao, " - ", x.Logradouro, ", ", x.Numero, " ", x.Complemento),
                Value = x.Id.ToString(),
                Selected = x.Entrega
            }));
            listaCartoes.ForEach(x => ddlCartoes.Add(new SelectListItem
            {
                Text = string.Concat(x.Bandeira, " - final ", x.Numero.Substring(x.Numero.Length - 4)),
                Value = x.Id.ToString(),
                Selected = x.Preferencial
            }));

            ViewBag.Enderecos = ddlEnderecos;
            ViewBag.Cartoes = ddlCartoes;

            Usuario usuario = (Usuario)Session["Usuario"];

            Cliente cliente = new Cliente
            {
                Carrinho = usuario?.Carrinho ?? new Carrinho()
            };

            Pedido pedido = new Pedido
            {
                Status = "Finalizado", //trocar os status
                Cliente = cliente,
                Bebidas = ((Usuario)Session["Usuario"])?.Carrinho?.Bebidas ?? new List<Bebida>()
            };

            return View(pedido);
        }

        public ActionResult FinalizarPedido(Pedido pedido)
        {
            try
            {
                pedido.IdCliente = ((Usuario)Session["Usuario"])?.Id ?? 0;
                pedido.Bebidas = ((Usuario)Session["Usuario"])?.Carrinho.Bebidas ?? new List<Bebida>();

                if (pedido.NovoCartao != null)
                {
                    new CartaoDao().Cadastrar(pedido.NovoCartao);
                }

                if (pedido.NovoEndereco != null)
                {
                    new EnderecoDao().Cadastrar(pedido.NovoEndereco);
                }

                new PedidoDao().Cadastrar(pedido);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index", "Usuarios");
        }

        public ActionResult Details(int id)
        {
            Pedido pedido = new Pedido
            {
                DataCadastro = DateTime.Now,
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
                        }
                    }
            };

            return View(pedido);
        }

        [HttpPost]
        public ActionResult CalcularFretePedido(string pedido)
        {
            Pedido novoPedido = JsonConvert.DeserializeObject<Pedido>(pedido);

            decimal total = 0;

            novoPedido.Bebidas.ForEach(x => total += SimularFrete(x));

            var resultado = new
            {
                frete = total
            };

            return Json(resultado);
        }
        
        [HttpPost]
        public decimal SimularFrete(Bebida bebida)
        {
            decimal frete = Convert.ToDecimal((new Random().NextDouble() * (double)(bebida.Valor / 3)).ToString("0.##"));

            return frete;
        }

        public ActionResult SimularFreteDetails(string bebidaFrete)
        {
            Bebida novaBebida = JsonConvert.DeserializeObject<Bebida>(bebidaFrete);

            return Json(SimularFrete(novaBebida), JsonRequestBehavior.AllowGet);
        }
    }
}