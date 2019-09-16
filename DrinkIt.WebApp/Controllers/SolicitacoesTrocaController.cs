using DrinkIt.WebApp.Dao;
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
                usuario = (Usuario)Session["Usuario"];

                dao.Cadastrar(usuario.Id, IdPedido, MotivoSolicitacao);

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



    }
}
