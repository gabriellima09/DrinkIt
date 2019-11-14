using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class EstoqueController : Controller
    {
        // GET: Estoque
        public ActionResult Index()
        {
            return View();
        }

        // GET: Estoque/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Estoque/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estoque/Create
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

        // GET: Estoque/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Estoque/Edit/5
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

        // GET: Estoque/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Estoque/Delete/5
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

        public ActionResult PvEstoque()
        {
            try
            {
                EstoqueDao dao = new EstoqueDao();
                return PartialView(dao.ConsultarTodos());

            }catch(Exception ex)
            {
                return View("Error");
            }
            

        }

        public ActionResult Entrada(int IdBebida, int Qtde, string Fornecedor, decimal VlrCusto, DateTime DtEntrada)
        {            
            try
            {
                EstoqueDao dao = new EstoqueDao();
                dao.Entrada(IdBebida, Qtde, Fornecedor, VlrCusto, DtEntrada);
                return RedirectToAction("Index", "Usuarios");
            }
            catch(Exception ex)
            {
                return View("Erro");
            }
        
}

        public ActionResult Baixa(int IdBebida, int Qtde)
        {
            try
            {
                EstoqueDao dao = new EstoqueDao();
                dao.Baixa(IdBebida, Qtde);
                return RedirectToAction("Index", "Usuarios");

            }catch(Exception ex)
            {
                return View("Erro");
            }
        }

        public ActionResult PvHistoricoEstoque()
        {
            List<Estoque> lista = new List<Estoque>();
            EstoqueDao dao = new EstoqueDao();
            lista = dao.ConsultarHistoricoEstoque();

            return PartialView(lista);
        }
    }
}
