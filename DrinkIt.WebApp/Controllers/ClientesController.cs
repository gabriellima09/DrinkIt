using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Facade;
using DrinkIt.WebApp.Models;
using DrinkIt.WebApp.Strategy;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IDao<Cliente> Dao;
        private readonly IFachada<Cliente> Fachada;

        public ClientesController()
        {
            Dao = new ClienteDao();
            Fachada = new Fachada<Cliente>(Dao);
        }

        // GET: Clientes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PvCliente()
        {
            return PartialView(Fachada.ConsultarTodos());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int id)
        {
            return View(Fachada.ConsultarPorId(id));
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        public ActionResult Create(Cliente cliente, string confirmSenha, List<int> LstDDD, List<string> LstTelefone, List<int> TiposTelefone)
        {
            try
            {
                //Validando a confirmação de senha
                if(cliente.Senha != confirmSenha)
                {
                    ViewBag.MsgErroConfirmarSenha = "Senha não confirmada corretamente.";
                    return View();
                }
                else
                {
                    ViewBag.MsgErroConfirmarSenha = "";
                }

                bool checkTelefones = true;
                cliente.Telefones = new List<Telefone>();

                for(int i = 0; i < LstDDD.Count; i++)
                {
                    if (LstDDD[i] == 0)
                        checkTelefones = false;

                    if (LstTelefone[i] == null || LstTelefone[i] == "")
                        checkTelefones = false;

                    if (TiposTelefone[i] == 0)
                        checkTelefones = false;
                }
                
                if (checkTelefones)
                {
                    for(int i = 0; i < LstDDD.Count; i++)
                    {
                        Telefone telefone = new Telefone
                        {
                            DDD = LstDDD[i],
                            Numero = LstTelefone[i],
                            IdTipo = TiposTelefone[i]
                        };

                        cliente.Telefones.Add(telefone);
                    }
                }
                else
                {
                    ViewBag.ErroTelefone = "Cadastro de telefones inválido. Insira os dados corretamente.";
                    return View();
                }


                Fachada.Cadastrar(cliente);

                Usuario usuario = new Usuario
                {
                    Id = cliente.Id,
                    Email = cliente.Email,
                    Login = cliente.Login,
                    Senha = cliente.Senha
                };

                return RedirectToAction("Login", "Usuarios", new { usuario });
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Fachada.ConsultarPorId(id));
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            try
            {
                Fachada.Alterar(cliente);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View(Fachada.ConsultarPorId(id));
        }


        [HttpPost]
        public ActionResult Delete(Cliente cliente)
        {
            Fachada.Excluir(cliente.Id);

            return RedirectToAction("Index", "Usuarios");
        }

        public ActionResult TrocarStatus(int id)
        {
            try
            {
                // TODO: Add update logic here
                Fachada.Excluir(id);

                return RedirectToAction("Index", "Usuarios");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
