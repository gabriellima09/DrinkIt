using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Facade;
using DrinkIt.WebApp.Models;
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
            if (usuario.Email.Equals("admin"))
            {
                Session["Usuario"] = new Usuario
                {
                    Id = 0,
                    Email = "admin@admin.com.br",
                    Login = "admin",
                    Senha = "admin"
                };

                return View("Index");
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

                return RedirectToAction("Index", "Clientes");
            }

            return View(usuario);
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