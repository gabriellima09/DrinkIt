using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DrinkIt.WebApp.Selenium
{
    public class WebDriveFactory
    {
        public static IWebDriver CreateWebDriver(
            Browser browser, string pathDriver)
        {
            IWebDriver webDriver = null;

            switch (browser)
            {
                case Browser.Chrome:
                    webDriver = new ChromeDriver(pathDriver);

                    break;
            }

            return webDriver;
        }
    }
}