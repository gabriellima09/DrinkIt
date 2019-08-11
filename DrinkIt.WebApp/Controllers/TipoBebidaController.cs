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
        // GET: TipoBebida
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PvTipoBebida()
        {
            List<TipoBebida> lista = new List<TipoBebida>
            {
                new TipoBebida
                {
                    Descricao = "Água"
                },

                new TipoBebida
                {
                    Descricao = "Suco"
                },

                new TipoBebida
                {
                    Descricao = "Leite"
                },

                new TipoBebida
                {
                    Descricao = "Refrigerante"
                },

                new TipoBebida
                {
                    Descricao = "Cerveja"
                },

                new TipoBebida
                {
                    Descricao = "Chá"
                },

                new TipoBebida
                {
                    Descricao = "Café"
                }

            };

            return PartialView(lista);
        }

        // GET: TipoBebida/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TipoBebida/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoBebida/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoBebida/Edit/5
        public ActionResult Edit(int id)
        {
            TipoBebida tipo = new TipoBebida
            {
                Descricao = "Água"
            };

            return View(tipo);
        }

        // POST: TipoBebida/Edit/5
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
