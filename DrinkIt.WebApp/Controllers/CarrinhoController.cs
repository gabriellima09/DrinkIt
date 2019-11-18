using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class CarrinhoController : Controller
    {
        // GET: Carrinho
        public ActionResult Index(int i = 0)
        {
            Dictionary<int, int> EstoqueBebidas = new Dictionary<int, int>();
            EstoqueDao estoquedao = new EstoqueDao();
            Usuario usuario = (Usuario)Session["Usuario"] ?? new Usuario();
            foreach(var item in usuario.Carrinho.Bebidas)
            {
                int estoqueAtual = 0;
                estoqueAtual = estoquedao.ConsultarEstoquePorId(item.Id);
                EstoqueBebidas[item.Id] = estoqueAtual;
                if(item.Quantidade > estoqueAtual)
                {
                    item.Quantidade = estoqueAtual;
                }
            }
            ViewBag.Estoque = EstoqueBebidas;
            if(i != 0)
            {
                ViewBag.MensagemHistorico = "Ops! O histórico foi atualizado agora mesmo! Por favor, revise seu pedido antes de prosseguir."
;           }
            else
            {
                ViewBag.MensagemHistorico = "";
            }
                    
            return View(usuario.Carrinho);
        }

        [HttpPost]
        public ActionResult AdicionarBebidaSessao(string bebida)
        {
            Dictionary<int, int> EstoqueBebidas = new Dictionary<int, int>();
            EstoqueDao estoquedao = new EstoqueDao();
            Bebida novaBebida = JsonConvert.DeserializeObject<Bebida>(bebida);

            novaBebida.Quantidade = 1;

            Usuario usuario = (Usuario)Session["Usuario"] ?? new Usuario();

            if(!usuario.Carrinho.Bebidas.Exists(x => x.Id == novaBebida.Id))
            {
                usuario.Carrinho.Bebidas.Add(novaBebida);
            }

            foreach (var item in usuario.Carrinho.Bebidas)
            {
                EstoqueBebidas[item.Id] = estoquedao.ConsultarEstoquePorId(item.Id);
            }
            ViewBag.Estoque = EstoqueBebidas;

            Session["Usuario"] = usuario;

            return View("Index", usuario.Carrinho);
        }

        [HttpPost]
        public ActionResult RemoverBebidaSessao(int idBebida)
        {
            Dictionary<int, int> EstoqueBebidas = new Dictionary<int, int>();
            EstoqueDao estoquedao = new EstoqueDao();
            Usuario usuario = (Usuario)Session["Usuario"] ?? new Usuario();

            usuario.Carrinho.Bebidas.RemoveAll(x => x.Id.Equals(idBebida));

            foreach (var item in usuario.Carrinho.Bebidas)
            {
                EstoqueBebidas[item.Id] = estoquedao.ConsultarEstoquePorId(item.Id);
            }
            ViewBag.Estoque = EstoqueBebidas;

            Session["Usuario"] = usuario;

            return View("Index", usuario.Carrinho);
        }
    }
}