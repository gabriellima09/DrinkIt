using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Facade;
using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class BebidasController : Controller
    {

        private readonly IDao<Bebida> Dao;
        private readonly IFachada<Bebida> Fachada;

        public BebidasController()
        {
            Dao = new BebidaDao();
            Fachada = new Fachada<Bebida>(Dao);
        }

        // GET: Bebidas
        public ActionResult Index()
        {
            return View();//teste
        }

        public ActionResult PvBebida()
        {
            
            return PartialView(Fachada.ConsultarTodos());
        }

        // GET: Bebidas/Details/5
        public ActionResult Details(int id)
        {
            return View(Fachada.ConsultarPorId(id));
        }

        // GET: Bebidas/Create
        public ActionResult Create()
        {            
            return View();
        }

        // POST: Bebidas/Create
        [HttpPost]
        public ActionResult Create(Bebida bebida, List<string> LstIngrediente)
        {
            try
            {
                int count = Request.Files.Count;
                var teste = Request.Files[0];
                bebida.Ingredientes = new List<Ingrediente>();
                if(LstIngrediente != null && LstIngrediente.Count > 0)
                {
                    foreach (var item in LstIngrediente)
                    {
                        Ingrediente i = new Ingrediente
                        {
                            Descricao = item
                        };

                        bebida.Ingredientes.Add(i);
                    }
                }
                
                Fachada.Cadastrar(bebida);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Bebidas/Edit/5
        public ActionResult Edit(int id)
        {
            if(id == 0)
            {
                return View("Error");
            }

            return View(Fachada.ConsultarPorId(id));
        }

        // POST: Bebidas/Edit/5
        [HttpPost]
        public ActionResult Edit(Bebida bebida, List<string> LstIngrediente)
        {
            bebida.Ingredientes = new List<Ingrediente>();

            foreach(var item in LstIngrediente)
            {
                Ingrediente ing = new Ingrediente();
                ing.Descricao = item;
                bebida.Ingredientes.Add(ing);
            }
            
            bebida.TipoBebida = new TipoBebida();
            try
            {
                // TODO: Add update logic here
                Fachada.Alterar(bebida);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }


        public ActionResult TrocarStatus(int id, string motivo = null)
        {
            try
            {
                BebidaDao dao = new BebidaDao();
                // TODO: Add update logic here
                Fachada.Excluir(id);
                dao.GravarMotivoInativacao(id, motivo); //aqui precisaria ser uma strategy...
                return RedirectToAction("Index", "Usuarios", null);
            }
            catch(Exception ex)
            {
                return View();
            }
           
        }
       
        public ActionResult PvDashBebidas()
        {
            return PartialView(Fachada.ConsultarTodos());
        }
    }
}
