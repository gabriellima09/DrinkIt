﻿using DrinkIt.WebApp.Models;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class UsuariosController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return RedirectToAction("Create", "Clientes");
        }


        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            return View(usuario);
        }

        // GET: Usuarios
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            if (usuario.Login.Equals("admin"))
            {
                Session["Usuario"] = new Usuario
                {
                    Id = 1,
                    Email = "admin@admin.com.br",
                    Login = "admin",
                    Senha = "admin"
                };

                return View("Index");
            }

            Session["Usuario"] = new Usuario
            {
                Id = 1,
                Email = "teste@teste.com.br",
                Login = "Teste",
                Senha = "Teste"
            };

            return RedirectToAction("Index", "Clientes");
        }

        public ActionResult PvUsuario()
        {
            if (Session.Count > 0)
            {
                ViewBag.NomeUsuario = ((Usuario)Session["Usuario"]).Login;
            }

            return PartialView();
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Configuracoes()
        {
            return View();
        }
    }
}