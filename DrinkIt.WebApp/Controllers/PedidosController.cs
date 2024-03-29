﻿using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Facade;
using DrinkIt.WebApp.Models;
using DrinkIt.WebApp.PurchaseApprove;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
                ViewBag.RankingCliente = new ClienteDao().GetRankingCliente(idCliente);
            }

            pedidos.Reverse();

            return PartialView(pedidos);
        }

        public ActionResult PedidosAprovarSaida()
        {
            List<Pedido> pedidos = new PedidoDao().ConsultarTodos()
                .Where(x => x.Status.OrderByDescending(y => y.Id).First().Id == 2)
                .ToList();

            return PartialView(pedidos);
        }

        public ActionResult Checkout()
        {
            Usuario usuario = (Usuario)Session["Usuario"];
            if (usuario == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }

            var listaEnderecos = new EnderecoDao().ConsultarPorCliente(usuario.Id);
            var listaCartoes = new CartaoDao().ConsultarPorCliente(usuario.Id);

            List<SelectListItem> ddlEnderecos = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Selecione um endereço",
                    Value = "0"
                }
            };
            List<SelectListItem> ddlCartoes = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Selecione um cartão",
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

            Cliente cliente = new Cliente
            {
                Carrinho = usuario?.Carrinho ?? new Carrinho()
            };

            Pedido pedido = new Pedido
            {
                Cliente = cliente,
                Bebidas = ((Usuario)Session["Usuario"])?.Carrinho?.Bebidas ?? new List<Bebida>()
            };

            //verifica se os itens ainda existem no estoque
            bool BebidasOK = new EstoqueDao().VerificaItensDisponiveis(pedido.Bebidas);

            if (!BebidasOK)
            {
                //retorna para o carrinho informando que o estoque foi atualizado
                return RedirectToAction("Index", "Carrinho", new { i = 1 });
            }

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

        public ActionResult FinalizarPedido(Pedido pedido)//idcartao1 e idenderecoentrega
        {
            Usuario usuario = (Usuario)Session["Usuario"] ?? new Usuario();
            try
            {
                
                pedido.IdCliente = usuario?.Id ?? 0;
                pedido.Bebidas = usuario?.Carrinho.Bebidas ?? new List<Bebida>();

                new PedidoDao().Cadastrar(pedido);

                int pedidoId = new PedidoDao().ObterUltimoIdInserido();

                new ProcedimentoTrocaStatus().EmProcessamento(pedidoId);

                Bandeira bandeira = new CartaoDao().ConsultarPorId(pedido.IdCartao1).Bandeira;

                if (new ValidadorCompra(bandeira).ValidarCompra())
                {
                    new ProcedimentoTrocaStatus().Aprovada(pedidoId);
                }
                else
                {
                    new ProcedimentoTrocaStatus().Reprovada(pedidoId);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }

            

            usuario.Carrinho.Bebidas.Clear();

            Session["Usuario"] = usuario;

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

        [HttpPost]
        public ActionResult ColocarEmTransito(int pedidoId)
        {
            Pedido pedido = new PedidoDao().ConsultarPorId(pedidoId);

            if (pedido != null && pedido?.Bebidas != null && pedido.Bebidas.Any())
            {
                new ProcedimentoTrocaStatus().EmTransito(pedidoId);

                foreach (var item in pedido.Bebidas)
                {
                    new EstoqueDao().Baixa(item.Id, item.Quantidade);
                }

                new ProcedimentoTrocaStatus().EmTransporte(pedidoId);
                new ProcedimentoTrocaStatus().Entregue(pedidoId);
                new ProcedimentoTrocaStatus().Finalizado(pedidoId);

                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                return View("Error", "Home");
            }
        }

        public ActionResult PvBebidasPedido(string bebidas)
        {
            List<Bebida> listaBebidas = JsonConvert.DeserializeObject<List<Bebida>>(bebidas);

            return PartialView(listaBebidas);
        }
    }
}