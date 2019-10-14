using DrinkIt.WebApp.Models;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class CarrinhoController : Controller
    {
        // GET: Carrinho
        public ActionResult Index()
        {
            Usuario usuario = (Usuario)Session["Usuario"] ?? new Usuario();

            return View(usuario.Carrinho);
        }

        [HttpPost]
        public ActionResult AdicionarBebidaSessao(string bebida)
        {
            Bebida novaBebida = JsonConvert.DeserializeObject<Bebida>(bebida);

            novaBebida.Quantidade = 1;

            Usuario usuario = (Usuario)Session["Usuario"] ?? new Usuario();

            usuario.Carrinho.Bebidas.Add(novaBebida);

            Session["Usuario"] = usuario;

            return View("Index", usuario.Carrinho);
        }

        [HttpPost]
        public ActionResult RemoverBebidaSessao(int idBebida)
        {
            Usuario usuario = (Usuario)Session["Usuario"] ?? new Usuario();

            usuario.Carrinho.Bebidas.RemoveAll(x => x.Id.Equals(idBebida));

            Session["Usuario"] = usuario;

            return View("Index", usuario.Carrinho);
        }
    }
}