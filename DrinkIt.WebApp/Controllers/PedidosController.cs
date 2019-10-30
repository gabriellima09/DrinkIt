using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Facade;
using DrinkIt.WebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedidos
        public ActionResult PvPedido()
        {
            int idCliente = ((Usuario)Session["Usuario"])?.Id ?? 0;

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
            Usuario usuario = ((Usuario)Session["Usuario"]);

            var listaEnderecos = new EnderecoDao().ConsultarPorCliente(usuario.Id);
            var listaCartoes = new CartaoDao().ConsultarPorCliente(usuario.Id);

            List<SelectListItem> ddlEnderecos = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Selecione um endereço...",
                    Value = "0"
                }
            };
            List<SelectListItem> ddlCartoes = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Selecione um cartão...",
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

            //Usuario usuario = (Usuario)Session["Usuario"];

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

        [HttpPost]
        public ActionResult AtualizarQuantidadeBebida(int idBebida, int quantidade)
        {
            Usuario usuario = ((Usuario)Session["Usuario"]);

            foreach (var item in usuario.Carrinho.Bebidas)
            {
                if (item.Id == idBebida)
                {
                    item.Quantidade = quantidade;
                }
            }

            Session["Usuario"] = usuario;

            return RedirectToAction("Checkout");
        }

        public ActionResult FinalizarPedido(Pedido pedido)
        {
            try
            {
                pedido.IdCliente = ((Usuario)Session["Usuario"])?.Id ?? 0;
                pedido.Bebidas = ((Usuario)Session["Usuario"])?.Carrinho.Bebidas ?? new List<Bebida>();

                new PedidoDao().Cadastrar(pedido);

                new ProcedimentoTrocaStatus().Entregue(new PedidoDao().ObterUltimoIdInserido());

                foreach (var item in pedido.Bebidas)
                {
                    new EstoqueDao().Baixa(item.Id, item.Quantidade);
                }
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