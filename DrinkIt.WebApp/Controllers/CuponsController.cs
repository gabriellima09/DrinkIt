using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Facade;
using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class CuponsController : Controller
    {
        private readonly IDao<Cupom> Dao;
        private readonly IFachada<Cupom> Fachada;
        // GET: Cupons
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PvCupom()
        {
            List<Cupom> cupons = new List<Cupom>
            {
                new Cupom
                {
                    Id = 1,
                    DataEmissao = DateTime.Now,
                    Descricao = "CUPOM123",
                    Status = true
                },
                new Cupom
                {
                    Id = 2,
                    DataEmissao = DateTime.Now,
                    Descricao = "CUPOM456",
                    Status = true
                },
                new Cupom
                {
                    Id = 3,
                    DataEmissao = DateTime.Now,
                    Descricao = "CUPOM789",
                    Status = true
                }
            };

            return PartialView(cupons);
        }

        // GET: Cupons/Details/5
        public ActionResult Details(int id)
        {
            Cupom cupom = new Cupom
            {
                Id = 1,
                DataEmissao = DateTime.Now,
                Descricao = "CUPOM123",
                Status = true
            };

            return View();
        }

        // GET: Cupons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cupons/Create
        [HttpPost]
        public ActionResult Create(Cupom cupom)
        {
            try
            {
                // TODO: Add insert logic here
                Fachada.Cadastrar(cupom);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cupons/Edit/5
        public ActionResult Edit(int id)
        {
            Cupom cupom = new Cupom
            {
                Id = 1,
                DataEmissao = DateTime.Now,
                Descricao = "CUPOM123",
                Status = true
            };

            return View();
        }

        // POST: Cupons/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Cupom cupom)
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
            return new EmptyResult();
        }
    }
}
