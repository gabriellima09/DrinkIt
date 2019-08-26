using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Facade;
using DrinkIt.WebApp.Models;
using DrinkIt.WebApp.Strategy;
using System;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IDao<Cliente> Dao;
        private readonly IFachada<Cliente> Fachada;
        private readonly IStrategy<Cliente> Strategy;

        public ClientesController()
        {
            Dao = new ClienteDao();
            Strategy = new ClienteStrategy();
            Fachada = new Fachada<Cliente>(Dao, Strategy);
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

                Usuario usuario = new Usuario
                {
                    Id = cliente.Id,
                    Email = cliente.Email,
                    Login = cliente.Login,
                    Senha = cliente.Senha
                };

                Session["Usuario"] = usuario;

                return RedirectToAction("Index");
            }
            catch (Exception ex)
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
