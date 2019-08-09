using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class ClientesController : Controller
    {
        // GET: Clientes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PvCliente()
        {
            List<Cliente> clientes = new List<Cliente>
            {
                new Cliente
                {
                    Id = 1,
                    Cpf = "123",
                    DataNascimento = DateTime.Now,
                    Email = "@",
                    Endereco = "Rua abc",
                    Genero = "masculino",
                    Telefone = "123456",
                    Login = "gabr",
                    Senha = "iel",
                    Nome = "Gabriel"
                },
                new Cliente
                {
                    Id = 1,
                    Cpf = "123",
                    DataNascimento = DateTime.Now,
                    Email = "@",
                    Endereco = "Rua abc",
                    Genero = "masculino",
                    Telefone = "123456",
                    Login = "gabr",
                    Senha = "iel",
                    Nome = "Gabriel"
                }
            };

            return PartialView(clientes);
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
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

        // GET: Clientes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Clientes/Edit/5
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

        public ActionResult TrocarStatus(int id)
        {
            //to do

            return RedirectToAction("Index");
        }
    }
}
