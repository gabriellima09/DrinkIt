using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Facade;
using DrinkIt.WebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly IDao<Endereco> Dao;
        private readonly IFachada<Endereco> Fachada;

        public EnderecoController()
        {
            Dao = new EnderecoDao();
            Fachada = new Fachada<Endereco>(Dao);
        }

        // GET: Endereco
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PvEndereco(List<Endereco> enderecos)
        {
            if (enderecos == null)
            {
                enderecos = new List<Endereco>();
            }

            return PartialView(enderecos);
        }

        public ActionResult PvEnderecos()
        {
            return PartialView(Fachada.ConsultarTodos());
        }

        // GET: Endereco/Details/5
        public ActionResult Details(int id)
        {
            return View(Fachada.ConsultarPorId(id));
        }

        // GET: Endereco/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Endereco/Create
        [HttpPost]
        public ActionResult Create(Endereco endereco)
        {
            try
            {
                endereco.ClienteId = ((Usuario)Session["Usuario"])?.Id ?? 0;

                Fachada.Cadastrar(endereco);

                return RedirectToAction("Index", "Clientes");
            }
            catch
            {
                return View();
            }
        }

        // GET: Endereco/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Fachada.ConsultarPorId(id));
        }

        // POST: Endereco/Edit/5
        [HttpPost]
        public ActionResult Edit(Endereco endereco)
        {
            try
            {
                endereco.ClienteId = ((Usuario)Session["Usuario"])?.Id ?? 0;

                Fachada.Alterar(endereco);

                return RedirectToAction("Index", "Clientes");
            }
            catch
            {
                return View();
            }
        }

        // GET: Endereco/Delete/5
        public ActionResult Delete(int id)
        {
            return View(Fachada.ConsultarPorId(id));
        }

        // POST: Endereco/Delete/5
        [HttpPost]
        public ActionResult Delete(Endereco endereco)
        {
            try
            {
                Fachada.Excluir(endereco.Id);

                return RedirectToAction("Index", "Clientes");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult AdicionarEndereco(Cliente cliente)
        {
            if (cliente.Enderecos == null)
            {
                cliente.Enderecos = new List<Endereco>();
            }

            cliente.Enderecos.Add(cliente.Endereco);

            if (cliente.Enderecos.Count == 1)
            {
                cliente.Enderecos.ElementAt(0).Cobranca = true;
                cliente.Enderecos.ElementAt(0).Entrega = true;
            }

            for (int i = 0; i < cliente.Enderecos.Count; i++)
            {
                cliente.Enderecos.ElementAt(i).Id = i + 1;
            }

            cliente.Endereco = new Endereco();

            return PartialView("PvEndereco", cliente.Enderecos);
        }

        [HttpPost]
        public ActionResult RemoverEndereco(Cliente cliente)
        {
            cliente.Enderecos.RemoveAll(x => x.Remover != 0);

            for (int i = 0; i < cliente.Enderecos.Count; i++)
            {
                cliente.Enderecos.ElementAt(i).Id = i + 1;
            }

            return PartialView("PvEndereco", cliente.Enderecos);
        }
    }
}
