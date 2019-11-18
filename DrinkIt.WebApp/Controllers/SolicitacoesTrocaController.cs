using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Facade;
using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class SolicitacoesTrocaController : Controller
    {
        // GET: SolicitacoesTroca
        public ActionResult Index()
        {
            return View();
        }

        // GET: SolicitacoesTroca/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SolicitacoesTroca/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SolicitacoesTroca/Create
        [HttpPost]
        public ActionResult Create(string MotivoSolicitacao, int IdPedido)
        {
            try
            {
                SolicitacaoTrocaDao dao = new SolicitacaoTrocaDao();
                // TODO: Add insert logic here
                Usuario usuario = new Usuario();
                usuario = (Usuario)Session["Usuario"] ?? new Usuario();

                dao.Cadastrar(usuario.Id, IdPedido, MotivoSolicitacao);

                new ProcedimentoTrocaStatus().EmTroca(IdPedido);

                return RedirectToAction("Index", "Clientes");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: SolicitacoesTroca/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SolicitacoesTroca/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SolicitacoesTroca/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SolicitacoesTroca/Delete/5
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

        public ActionResult PvSolicitacoesTroca()
        {
            SolicitacaoTrocaDao dao = new SolicitacaoTrocaDao();

            return PartialView(dao.ConsultarTodos());
        }

        public ActionResult Reprovar(string MotivoReprova, int IdSolicitacao)
        {
            try
            {
                SolicitacaoTrocaDao dao = new SolicitacaoTrocaDao();
                dao.Reprovar(IdSolicitacao, MotivoReprova);

                int pedidoId = dao.ConsultarTodos().Find(x => x.Id == IdSolicitacao).IdPedido;

                new ProcedimentoTrocaStatus().TrocaNaoAutorizada(pedidoId);

                return RedirectToAction("Index", "Usuarios");
            }catch(Exception ex)
            {
                return View("Error");
            }
            
        }

        public ActionResult Aprovar(int IdSolicitacao)
        {
            try
            {
                SolicitacaoTrocaDao dao = new SolicitacaoTrocaDao();

                int pedidoId = dao.ConsultarTodos().Find(x => x.Id == IdSolicitacao).IdPedido;

                new ProcedimentoTrocaStatus().TrocaAutorizada(pedidoId);

                Pedido pedido = new PedidoDao().ConsultarPorId(pedidoId);

                foreach (var item in pedido.Bebidas)
                {
                    new EstoqueDao().Entrada(item.Id, item.Quantidade, "DEVOLUÇÃO", 0.00M, DateTime.Now);
                }

                new ProcedimentoTrocaStatus().Trocado(pedidoId);

                Cupom cupom = new Cupom()
                {
                    DataEmissao = DateTime.Now,
                    DataExpiracao = DateTime.Now.AddDays(7),
                    Descricao = string.Concat("TROCA", pedido.Id, DateTime.Now.ToString("yyyyMMddHHmmssfff")),
                    IdTipo = 3,
                    Status = true,
                    Valor = pedido.ValorTotal
                };

                new CupomDao().Cadastrar(cupom);

                int idCupom = new CupomDao().ObterUltimoIdInserido();

                dao.Aprovar(IdSolicitacao, idCupom);

                new CupomDao().InsereCupomParaCliente(idCupom, pedido.Cliente.Id);

                Notificacao notificacao = new Notificacao
                {
                    IdCliente = pedido.Cliente.Id,
                    Descricao = $"O cupom de troca {cupom.Descricao} foi gerado da aprovação da sua solicitação de troca do pedido #{pedido.Id}. Entre no menu ''Meus Cupons'' para visualizar."
                };

                new NotificacaoDao().Cadastrar(notificacao);

                return RedirectToAction("Index", "Usuarios");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }


    }
}
