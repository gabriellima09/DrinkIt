using OpenQA.Selenium;
using System;
using System.Threading;

namespace DrinkIt.WebApp.Selenium
{
    public static class WebDriverExtensions
    {
        public static void LoadPage(this IWebDriver webDriver, TimeSpan timeToWait, string url)
        {
            Thread.Sleep(1000);
            webDriver.Manage().Timeouts().PageLoad = timeToWait;
            webDriver.Navigate().GoToUrl(url);
        }

        public static string GetText(this IWebDriver webDriver, By by)
        {
            Thread.Sleep(1000);
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            IWebElement webElement = webDriver.FindElement(by);
            return webElement.Text;
        }

        public static void SetText(this IWebDriver webDriver, By by, string text)
        {
            Thread.Sleep(1000);
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            IWebElement webElement = webDriver.FindElement(by);
            webElement.SendKeys(text);
        }

        public static void Submit(this IWebDriver webDriver, By by)
        {
            Thread.Sleep(1000);
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            IWebElement webElement = webDriver.FindElement(by);
            webElement.Click();
        }

        public static void Wait(this IWebDriver webDriver, double seconds)
        {
            Thread.Sleep(3000);
        }


        public static void Quit(this IWebDriver webDriver)
        {
            Thread.Sleep(1000);
            webDriver.Quit();
        }
    }
}