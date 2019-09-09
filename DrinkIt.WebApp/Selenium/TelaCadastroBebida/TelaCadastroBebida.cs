using DrinkIt.WebApp.Models;
using OpenQA.Selenium;
using System;
using System.Configuration;

namespace DrinkIt.WebApp.Selenium
{
    public class TelaCadastroBebida
    {
        private readonly Browser _browser;
        private IWebDriver _driver;

        public TelaCadastroBebida(Browser browser)
        {
            _browser = browser;

            string caminhoDriver = ConfigurationManager.AppSettings["Selenium"];

            _driver = WebDriveFactory.CreateWebDriver(_browser, caminhoDriver);
        }

        public void CarregarPagina()
        {
            _driver.LoadPage(TimeSpan.FromSeconds(5), ConfigurationManager.AppSettings["UrlTelaCadastroBebida"]);
        }

        public void PreencherDadosBebida(Bebida bebida)
        {
            _driver.SetText(By.Name("Nome"), bebida.Nome.ToString());
            _driver.SetText(By.Name("Descricao"), bebida.Descricao.ToString());
            _driver.SetText(By.Name("Marca"), bebida.Marca.ToString());
            _driver.SetText(By.Name("Valor"), bebida.Valor.ToString());
            _driver.SetText(By.Name("Volume"), bebida.Volume.ToString());
            _driver.SetText(By.Name("Peso"), bebida.Peso.ToString());
            _driver.SetText(By.Name("Sabor"), bebida.Sabor.ToString());
            _driver.SetText(By.Name("Lote"), bebida.Lote.ToString());
            _driver.SetText(By.Name("DataFabricacao"), bebida.DataFabricacao.ToString("dd/MM/yyyy"));
            _driver.SetText(By.Name("DataValidade"), bebida.DataValidade.ToString("dd/MM/yyyy"));            
            _driver.SetText(By.Name("Fabricante"), bebida.Fabricante.ToString());
            _driver.SetText(By.Name("Embalagem"), bebida.Embalagem.ToString());
            _driver.SetText(By.Name("CodigoBarras"), bebida.CodigoBarras.ToString());
            _driver.SetText(By.Name("Teor"), bebida.Teor.ToString());
            _driver.SetText(By.Name("DicaConservacao"), bebida.DicaConservacao.ToString());
            _driver.SetText(By.Name("Status"), bebida.Status.ToString());
            _driver.SetText(By.Name("LstIngrediente"), bebida.Status.ToString());
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