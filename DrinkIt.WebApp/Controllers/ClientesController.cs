using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Facade;
using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IDao<Cliente> Dao;
        private readonly IFachada<Cliente> Fachada;

        public ClientesController()
        {
            Fachada = new Fachada<Cliente>(Dao);
        }

        // GET: Clientes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PvCliente()
        {
            return PartialView(Fachada.ConsultarTodos());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int id)
        {
            return View(Fachada.ConsultarPorId(id));
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            try
            {
                Fachada.Cadastrar(cliente);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Fachada.ConsultarPorId(id));
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            try
            {
                Fachada.Alterar(cliente);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult TrocarStatus(int id)
        {
            return new EmptyResult();
        }
    }
}
