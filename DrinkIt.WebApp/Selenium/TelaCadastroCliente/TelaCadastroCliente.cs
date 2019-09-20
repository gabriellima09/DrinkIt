using DrinkIt.WebApp.Models;
using OpenQA.Selenium;
using System;
using System.Configuration;

namespace DrinkIt.WebApp.Selenium
{
    public class TelaCadastroCliente
    {
        private readonly Browser _browser;
        private IWebDriver _driver;

        public TelaCadastroCliente(Browser browser)
        {
            _browser = browser;

            string caminhoDriver = ConfigurationManager.AppSettings["Selenium"];

            _driver = WebDriveFactory.CreateWebDriver(_browser, caminhoDriver);
        }

        public void CarregarPagina()
        {
            _driver.LoadPage(TimeSpan.FromSeconds(5), ConfigurationManager.AppSettings["UrlTelaCadastroCliente"]);
        }

        public void PreencherDadosCliente(Cliente cliente)
        {
            _driver.SetText(By.Name("Email"), cliente.Email.ToString());
            _driver.SetText(By.Name("Login"), cliente.Login.ToString());
            _driver.SetText(By.Name("Senha"), cliente.Senha.ToString());
            _driver.SetText(By.Name("Nome"), cliente.Nome.ToString());
            _driver.SetText(By.Name("Cpf"), cliente.Cpf.ToString());
            _driver.SetText(By.Name("DataNascimento"), cliente.DataNascimento.ToString("dd-MM-yyyy"));
            _driver.SetText(By.Name("Genero"), cliente.Genero.ToString());
            _driver.SetText(By.Name("Endereco.Descricao"), cliente.Endereco.Descricao.ToString());
            _driver.SetText(By.Name("Endereco.CEP"), cliente.Endereco.CEP.ToString());
            _driver.SetText(By.Name("Endereco.Logradouro"), cliente.Endereco.Logradouro.ToString());
            _driver.SetText(By.Name("Endereco.Numero"), cliente.Endereco.Numero.ToString());
            _driver.SetText(By.Name("Endereco.Complemento"), cliente.Endereco.Complemento.ToString());
            _driver.SetText(By.Name("Endereco.Bairro"), cliente.Endereco.Bairro.ToString());
            _driver.SetText(By.Name("Endereco.Cidade"), cliente.Endereco.Cidade.ToString());
            _driver.SetText(By.Name("Endereco.Estado"), cliente.Endereco.Estado.ToString());
            _driver.SetText(By.Name("Cartao.Numero"), cliente.Cartao.Numero.ToString());
            _driver.SetText(By.Name("Cartao.NomeTitular"), cliente.Cartao.NomeTitular.ToString());
            _driver.SetText(By.Name("Cartao.Bandeira"), cliente.Cartao.Bandeira.ToString());
            _driver.SetText(By.Name("Cartao.CodigoSeguranca"), cliente.Cartao.CodigoSeguranca.ToString());
        }

        public void ProcessarCadastro()
        {
            _driver.Submit(By.Id("btnCadastrar"));
        }

        public void Esperar(double segundos)
        {
            _driver.Wait(segundos);
        }


        public void Fechar()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}