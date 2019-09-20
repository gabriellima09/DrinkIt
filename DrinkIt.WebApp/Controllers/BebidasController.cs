using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Facade;
using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class BebidasController : Controller
    {

        private readonly IDao<Bebida> Dao;
        private readonly IFachada<Bebida> Fachada;
        private readonly IBebida bebidaDao;

        public BebidasController()
        {
            Dao = new BebidaDao();
            Fachada = new Fachada<Bebida>(Dao);
            bebidaDao = new BebidaDao();
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
                //Access the File using the Name of HTML INPUT File.
                HttpPostedFileBase postedFile = Request.Files["CaminhoImagem"];

                //Check if File is available.
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    //Save the File.
                    string filePath = Server.MapPath("~/Images/") + Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    bebida.CaminhoImagem = postedFile.FileName;
                }

                
                bebida.Ingredientes = new List<Ingrediente>();
                if (LstIngrediente != null && LstIngrediente.Count > 0)
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
            catch(Exception ex)
            {
                return View("Error");
            }
        }

        // GET: Bebidas/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
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

            foreach (var item in LstIngrediente)
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
            catch (Exception ex)
            {
                return View();
            }
        }


        public ActionResult TrocarStatus(int id, int statusAtual, string motivo = null)
        {
            try
            {
                // TODO: Add update logic here
                Fachada.Excluir(id);
                if(statusAtual == 1)//Inativando?
                {
                    bebidaDao.GravarMotivoInativacao(id, motivo);
                }                
                return RedirectToAction("Index", "Usuarios");
            }
            catch (Exception ex)
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
