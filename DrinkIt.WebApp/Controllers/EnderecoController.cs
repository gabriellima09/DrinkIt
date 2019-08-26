using DrinkIt.WebApp.Models;
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

        public ActionResult PvEndereco(List<Endereco> enderecos)
        {
            if (enderecos == null)
            {
                enderecos = new List<Endereco>();
            }

            return PartialView(enderecos);
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

        [HttpPost]
        public ActionResult AdicionarEndereco(Cliente cliente)
        {
            if (cliente.Enderecos == null)
            {
                cliente.Enderecos = new List<Endereco>();
            }

            cliente.Enderecos.Add(cliente.Endereco);

            if (cliente.Enderecos.Count == 1)
            {
                cliente.Enderecos.ElementAt(0).Cobranca = true;
                cliente.Enderecos.ElementAt(0).Entrega = true;
            }

            for (int i = 0; i < cliente.Enderecos.Count; i++)
            {
                cliente.Enderecos.ElementAt(i).Id = i + 1;
            }

            cliente.Endereco = new Endereco();

            return PartialView("PvEndereco", cliente.Enderecos);
        }

        [HttpPost]
        public ActionResult RemoverEndereco(Cliente cliente)
        {
            cliente.Enderecos.RemoveAll(x => x.Remover != 0);

            for (int i = 0; i < cliente.Enderecos.Count; i++)
            {
                cliente.Enderecos.ElementAt(i).Id = i + 1;
            }

            return PartialView("PvEndereco", cliente.Enderecos);
        }
    }
}
