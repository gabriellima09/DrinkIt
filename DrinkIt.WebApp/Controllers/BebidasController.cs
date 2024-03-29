﻿using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Facade;
using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class BebidasController : Controller
    {
        private readonly int TIMEOUT = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutItensCarrinho"]);
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
            Bebida bebida = Fachada.ConsultarPorId(id);

            Usuario usuario = (Usuario)Session["Usuario"] ?? new Usuario();

            if (usuario.Carrinho != null
                && usuario.Carrinho.Bebidas != null
                && usuario.Carrinho.Bebidas.Any())
            {
                bebida.Quantidade -= usuario.Carrinho.Bebidas
                                        .Where(x => x.Id == bebida.Id)
                                        .Sum(x => x.Quantidade);
            }

            return View(bebida);
        }

        // GET: Bebidas/Create
        public ActionResult Create()
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items = (List<SelectListItem>)bebidaDao.GetTiposBebida();
            ViewBag.ListaTiposBebida = Items;
            return View();
        }

        // POST: Bebidas/Create
        [HttpPost]
        public ActionResult Create(Bebida bebida, List<string> LstIngrediente, HttpPostedFileBase ArquivoImagem)
        {
            try
            { 
                if(ArquivoImagem != null)
                {
                    var originalFilename = Path.GetFileName(ArquivoImagem.FileName);
                    string fileId = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";

                    var path = Path.Combine(Server.MapPath("~/Images/"), fileId);
                    ArquivoImagem.SaveAs(path);

                    bebida.CaminhoImagem = fileId;
                }
                else
                {
                    bebida.CaminhoImagem = "drink.jpg";
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

                Resultado resultado = Fachada.Cadastrar(bebida);

                if (resultado.Sucesso)
                {
                    return RedirectToAction("Index", "Usuarios");
                }
                else
                {
                    ViewBag.Erro = resultado.MensagensErro;
                    List<SelectListItem> Items = new List<SelectListItem>();
                    Items = (List<SelectListItem>)bebidaDao.GetTiposBebida();
                    ViewBag.ListaTiposBebida = Items;
                    return View(bebida);
                }

                
            }
            catch (Exception ex)
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

            List<SelectListItem> Items = new List<SelectListItem>();
            Items = (List<SelectListItem>)bebidaDao.GetTiposBebida();

            Bebida bebida = new Bebida();
            bebida = Fachada.ConsultarPorId(id);

            foreach(var item in Items)
            {
                if(item.Value.Equals(bebida.TipoBebida.Id.ToString()))
                {
                    item.Selected = true;
                }
            }

            ViewBag.ListaTiposBebida = Items;

            return View(bebida);
        }

        // POST: Bebidas/Edit/5
        [HttpPost]
        public ActionResult Edit(Bebida bebida, List<string> LstIngrediente, HttpPostedFileBase ArquivoImagem)
        {
            if (ArquivoImagem != null)
            {
                var originalFilename = Path.GetFileName(ArquivoImagem.FileName);
                string fileId = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";

                var path = Path.Combine(Server.MapPath("~/Images/"), fileId);
                ArquivoImagem.SaveAs(path);

                bebida.CaminhoImagem = fileId;
            }


            bebida.Ingredientes = new List<Ingrediente>();

            foreach (var item in LstIngrediente)
            {
                Ingrediente ing = new Ingrediente();
                ing.Descricao = item;
                bebida.Ingredientes.Add(ing);
            }

            //bebida.TipoBebida = new TipoBebida();
            try
            {
                // TODO: Add update logic here
                Fachada.Alterar(bebida);
                return RedirectToAction("Index", "Usuarios");
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        public ActionResult TrocarStatus(int id, int statusAtual, string motivo)
        {
            try
            {
                // TODO: Add update logic here
                Fachada.Excluir(id);
                if (statusAtual == 1)//Inativando?
                {
                    bebidaDao.GravarMotivoInativacao(id, motivo, "Manual", "Inativacao");
                }
                else //Ativando?
                {
                    bebidaDao.GravarMotivoInativacao(id, motivo, "Manual", "Ativacao");
                }
                return RedirectToAction("Index", "Usuarios");
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        public ActionResult PvDashBebidas(int idGas = 0, int idTeor = 0, int idValor = 0, int idTipo = 0, string textoBusca = "")
        {
            
            if (idGas == 0 && idTeor == 0 && idValor == 0 && idTipo == 0 && textoBusca.Trim().Equals(""))
            {//RESULTADO INICIAL: COMPLETO
                return PartialView(new BebidaDao().ConsultarDashBebidas());
            }
            else
            {//TO-DO: BUSCA POR TEXTO/COMBOS
                return PartialView(bebidaDao.ConsultarComFiltro(idGas, idTeor, idValor, idTipo, textoBusca));
            }
        }

        public ActionResult PvCarousel()
        {
            return PartialView();
        }

        public ActionResult PvHistoricoBebidas()
        {
            List<HistoricoBebida> historico = new List<HistoricoBebida>();
            historico = new BebidaDao().ConsultarHistoricoBebidas();
            return PartialView(historico);
        }
    }
}
