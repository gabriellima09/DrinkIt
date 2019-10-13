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

                    if (pedido.IdCartao1 == 0)
                    {
                        pedido.IdCartao1 = new CartaoDao().ObterUltimoIdInserido();
                    }
                    else if (pedido.IdCartao2 == 0)
                    {
                        pedido.IdCartao2 = new CartaoDao().ObterUltimoIdInserido();
                    }
                }

                if (pedido.NovoEndereco != null && pedido.IdEnderecoEntrega == 0)
                {
                    new EnderecoDao().Cadastrar(pedido.NovoEndereco);
                    pedido.IdEnderecoEntrega = new EnderecoDao().ObterUltimoIdInserido();
                }

                new PedidoDao().Cadastrar(pedido);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index", "Clientes");
        }

        public ActionResult Details(int id)
        {
            return View(new PedidoDao().ConsultarPorId(id));
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