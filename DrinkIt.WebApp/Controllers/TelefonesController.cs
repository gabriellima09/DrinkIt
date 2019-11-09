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
    public class TelefonesController : Controller
    {
        private readonly IDao<Telefone> Dao;
        private readonly IFachada<Telefone> Fachada;
        private readonly ITelefone telefoneDao;

        public TelefonesController()
        {
            Dao = new TelefoneDao();
            Fachada = new Fachada<Telefone>(Dao);
            telefoneDao = new TelefoneDao();
        }

        // GET: Telefones
        public ActionResult Index()
        {
            return View();
        }

        // GET: Telefones/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Telefones/Create
        public ActionResult Create()
        {
            ViewBag.ErroTelefone = "";
            return View();
        }

        // POST: Telefones/Create
        [HttpPost]
        public ActionResult Create(Telefone telefone)
        {
            try
            {
                // TODO: Add insert logic here
                if(telefone.DDD == 0 || telefone.Numero == null)
                {
                    ViewBag.ErroTelefone = "Dados inválidos. Insira o DDD e o número corretamente.";
                    return View(telefone);
                }

                telefone.Id = ((Usuario)Session["Usuario"])?.Id ?? 0;

                Resultado resultado = Fachada.Cadastrar(telefone);

                return RedirectToAction("Index", "Clientes");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Telefones/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Telefones/Edit/5
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

        // GET: Telefones/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Telefones/Delete/5
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

        public ActionResult PvTelefone()
        {
            try
            {
                int idCliente = ((Usuario)Session["Usuario"]).Id;

                return PartialView(telefoneDao.ConsultarPorCliente(idCliente));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
