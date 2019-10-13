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
    public class TipoBebidaController : Controller
    {

        private readonly IDao<TipoBebida> Dao;
        private readonly IFachada<TipoBebida> Fachada;
        private readonly ITipoBebida tipoBebidaDao;

        public TipoBebidaController()
        {
            Dao = new TipoBebidaDao();
            Fachada = new Fachada<TipoBebida>(Dao);
            tipoBebidaDao = new TipoBebidaDao();

        }

        // GET: TipoBebida
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PvTipoBebida()
        {
            return PartialView(Fachada.ConsultarTodos());
        }

        // GET: TipoBebida/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TipoBebida/Create
        public ActionResult Create()
        {
            List<SelectListItem> listaGrupos = new List<SelectListItem>();
            listaGrupos = (List<SelectListItem>)tipoBebidaDao.GetGruposPrecificacao();
            ViewBag.listaGrupos = listaGrupos;
            return View();
        }

        // POST: TipoBebida/Create
        [HttpPost]
        public ActionResult Create(TipoBebida tipo)
        {
            try
            {
                // TODO: Add insert logic here
                Fachada.Cadastrar(tipo);
                return RedirectToAction("Index", "Usuarios");
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoBebida/Edit/5
        public ActionResult Edit(int id)
        {
            TipoBebida tipo = new TipoBebida();

            tipo = Fachada.ConsultarPorId(id);

            List<SelectListItem> listaGrupos = new List<SelectListItem>();
            listaGrupos = (List<SelectListItem>)tipoBebidaDao.GetGruposPrecificacao();

            foreach(var item in listaGrupos)
            {
                if(item.Value.Equals(tipo.IdGrupoPrecificacao.ToString()))
                {
                    item.Selected = true;
                }
            }

            ViewBag.listaGrupos = listaGrupos;
            return View(tipo);
        }

        // POST: TipoBebida/Edit/5
        [HttpPost]
        public ActionResult Edit(TipoBebida tipo)
        {
            try
            {
                // TODO: Add update logic here

                Fachada.Alterar(tipo);
                return RedirectToAction("Index", "Usuarios");
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoBebida/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TipoBebida/Delete/5
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
