using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class EnderecoController : Controller
    {
        // GET: Endereco
        public ActionResult Index()
        {
            return View();
        }

        // GET: Endereco/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Endereco/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Endereco/Create
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

        // GET: Endereco/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Endereco/Edit/5
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

        // GET: Endereco/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Endereco/Delete/5
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
