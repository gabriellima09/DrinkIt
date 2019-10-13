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

        public CuponsController()
        {
            Dao = new CupomDao();
            Fachada = new Fachada<Cupom>(Dao);
        }

        // GET: Cupons
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PvCupom()
        {
            try
            {
                int idCliente = ((Usuario)Session["Usuario"]).Id;

                List<Cupom> cupons = new List<Cupom>();

                if (idCliente == 0)
                {
                    cupons = Fachada.ConsultarTodos();
                }
                else
                {
                    cupons = new CupomDao().ConsultarPorCliente(idCliente);
                }

                return PartialView(cupons);
            }
            catch (Exception ex)
            {
                throw ex;
            }

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

                return RedirectToAction("Index", "Usuarios");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Cupons/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                return View(Fachada.ConsultarPorId(id));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        // POST: Cupons/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Cupom cupom)
        {
            try
            {
                // TODO: Add update logic here

                Fachada.Alterar(cupom);

                return RedirectToAction("Index", "Usuarios");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        public ActionResult TrocarStatus(int id)
        {
            try
            {
                Fachada.Excluir(id);
                return RedirectToAction("Index", "Usuarios");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpPost]
        public ActionResult ValidarCupom(string cupom)
        {
            Dictionary<bool, Cupom> dic = new CupomDao().ValidarCupom(cupom);

            var resultado = new
            {
                Resultado = dic.ContainsKey(true),
                Cupom = dic.Values
            };

            return Json(resultado);
        }
    }
}
