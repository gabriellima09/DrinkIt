using DrinkIt.WebApp.Models;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class CarrinhoController : Controller
    {
        // GET: Carrinho
        public ActionResult Index()
        {
            Usuario usuario = (Usuario)Session["Usuario"];

            return View(usuario.Carrinho);
        }

        [HttpPost]
        public ActionResult AdicionarBebidaSessao(Bebida bebida)
        {
            Usuario usuario = (Usuario)Session["Usuario"];

            usuario.Carrinho.Bebidas.Add(bebida);

            return View("Index", usuario.Carrinho);
        }

        [HttpPost]
        public ActionResult RemoverBebidaSessao(int idBebida)
        {
            Usuario usuario = (Usuario)Session["Usuario"];

            usuario.Carrinho.Bebidas.RemoveAll(x => x.Id.Equals(idBebida));

            return View("Index", usuario.Carrinho);
        }
    }
}