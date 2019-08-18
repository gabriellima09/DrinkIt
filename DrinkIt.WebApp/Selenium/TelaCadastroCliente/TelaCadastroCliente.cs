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
            _driver.SetText(By.Name("DataNascimento"), cliente.DataNascimento.ToString("dd/MM/yyyy"));
            _driver.SetText(By.Name("Genero"), cliente.Genero.ToString());
            _driver.SetText(By.Name("Telefone"), cliente.Telefone.ToString());
            _driver.SetText(By.Name("CEP"), cliente.CEP.ToString());
            _driver.SetText(By.Name("Endereco"), cliente.Endereco.ToString());
            _driver.SetText(By.Name("Complemento"), cliente.Complemento.ToString());
            _driver.SetText(By.Name("Bairro"), cliente.Bairro.ToString());
            _driver.SetText(By.Name("Cidade"), cliente.Cidade.ToString());
            _driver.SetText(By.Name("Estado"), cliente.Estado.ToString());
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