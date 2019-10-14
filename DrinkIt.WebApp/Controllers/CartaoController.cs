using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Facade;
using DrinkIt.WebApp.Models;
using System;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class CartaoController : Controller
    {
        private readonly IDao<CartaoCredito> Dao;
        private readonly IFachada<CartaoCredito> Fachada;

        public CartaoController()
        {
            Dao = new CartaoDao();
            Fachada = new Fachada<CartaoCredito>(Dao);
        }

        // GET: Cartao
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PvCartoes()
        {
            return PartialView(Fachada.ConsultarTodos());
        }

        // GET: Cartao/Details/5
        public ActionResult Details(int id)
        {
            return View(Fachada.ConsultarPorId(id));
        }

        // GET: Cartao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cartao/Create
        [HttpPost]
        public ActionResult Create(CartaoCredito cartao)
        {
            try
            {
                cartao.ClienteId = ((Usuario)Session["Usuario"])?.Id ?? 0;

                Resultado resultado = Fachada.Cadastrar(cartao);

                if (resultado.Sucesso)
                {
                    return RedirectToAction("Index", "Clientes");
                }
                else
                {
                    ViewBag.Erro = resultado.MensagensErro;
                    return View(cartao);
                }
            }
            catch
            {
                return RedirectToAction("Erro", "Home");
            }
        }

        // GET: Cartao/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Fachada.ConsultarPorId(id));
        }

        // POST: Cartao/Edit/5
        [HttpPost]
        public ActionResult Edit(CartaoCredito cartao)
        {
            try
            {
                cartao.ClienteId = ((Usuario)Session["Usuario"])?.Id ?? 0;

                Resultado resultado = Fachada.Alterar(cartao);

                if (resultado.Sucesso)
                {
                    return RedirectToAction("Index", "Clientes");
                }
                else
                {
                    ViewBag.Erro = resultado.MensagensErro;
                    return View(cartao);
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Erro", "Home");
            }
        }

        // GET: Cartao/Delete/5
        public ActionResult Delete(int id)
        {
            return View(Fachada.ConsultarPorId(id));
        }

        // POST: Cartao/Delete/5
        [HttpPost]
        public ActionResult Delete(CartaoCredito cartao)
        {
            try
            {
                Fachada.Excluir(cartao.Id);

                return RedirectToAction("Index", "Clientes");
            }
            catch
            {
                return View();
            }
        }
    }
}
