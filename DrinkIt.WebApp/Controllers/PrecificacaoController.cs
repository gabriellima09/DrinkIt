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
    public class PrecificacaoController : Controller
    {
        private readonly IDao<GrupoPrecificacao> Dao;
        private readonly IFachada<GrupoPrecificacao> Fachada;

        public PrecificacaoController()
        {
            Dao = new PrecificacaoDao();
            Fachada = new Fachada<GrupoPrecificacao>(Dao);
        }

        // GET: Precificacao
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PvGrupoPrecificacao()
        {
            return PartialView(Fachada.ConsultarTodos()); 
        }

        // GET: Precificacao/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Precificacao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Precificacao/Create
        [HttpPost]
        public ActionResult Create(GrupoPrecificacao precificacao)
        {
            try
            {
                // TODO: Add insert logic here

                Fachada.Cadastrar(precificacao);

                return RedirectToAction("Index", "Usuarios");
            }
            catch
            {
                return View();
            }
        }

        // GET: Precificacao/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Fachada.ConsultarPorId(id));
        }

        // POST: Precificacao/Edit/5
        [HttpPost]
        public ActionResult Edit(GrupoPrecificacao grupo)
        {
            try
            {
                // TODO: Add update logic here
                Fachada.Alterar(grupo);
                return RedirectToAction("Index", "Usuarios");
            }
            catch
            {
                return View();
            }
        }

        // GET: Precificacao/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Precificacao/Delete/5
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
    }
}
