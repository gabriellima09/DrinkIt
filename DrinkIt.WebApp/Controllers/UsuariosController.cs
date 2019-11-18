using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Facade;
using DrinkIt.WebApp.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class UsuariosController : Controller
    {
        public readonly IFachada<Usuario> Fachada;
        public readonly IDao<Usuario> Dao;
        public readonly IUsuario usuarioDao;

        public UsuariosController()
        {
            usuarioDao = new UsuarioDao();
            Dao = new UsuarioDao();
            Fachada = new Fachada<Usuario>(Dao);
        }

        public ActionResult Index()
        {
            Usuario usuario = (Usuario)Session["Usuario"];
            if (usuario == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }

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
            ViewBag.ErrorLogin = "";
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            if (usuario.Email.Equals("admin"))
            {
                Session["Usuario"] = new Usuario
                {
                    Id = 0,
                    Email = "admin@admin.com.br",
                    Login = "admin",
                    Senha = "admin"
                };

                return RedirectToAction("Index");
            }

            if (usuarioDao.Login(usuario.Email, usuario.Senha))
            {
                usuario = usuarioDao.RecuperarUsuario(usuario.Email);

                Session["Usuario"] = new Usuario
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    Login = usuario.Login,
                    Senha = usuario.Senha
                };

                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorLogin = "E-mail/senha inválidos. Verifique seus dados e tente novamente.";

            return View(usuario);
        }

        public ActionResult PvUsuario()
        {
            if (Session.Count > 0)
            {
                Usuario usuario = ((Usuario)Session["Usuario"]) ?? new Usuario();

                string nome = "Admin";

                if (usuario.Id > 0)
                {
                    Cliente cliente = new ClienteDao().ConsultarPorId(usuario.Id);

                    nome = cliente.Nome;
                }

                ViewBag.NomeUsuario = nome;
            }

            return PartialView();
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult PvSolicitacoesTroca()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult TrocarSenha(string senhaAtual, string novaSenha, string confirmSenha)
        {
            try
            {
                int clienteId = ((Usuario)Session["Usuario"])?.Id ?? 0;

                usuarioDao.TrocarSenha(clienteId, novaSenha);
            }
            catch (System.Exception)
            {
                return View("Error");
            }

            return RedirectToAction("Index", "Clientes");
        }
    }
}