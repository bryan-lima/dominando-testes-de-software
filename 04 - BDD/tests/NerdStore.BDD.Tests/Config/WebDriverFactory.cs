using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace NerdStore.BDD.Tests.Config
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateWebDriver(Browser brower, string caminhoDriver, bool headless)
        {
            IWebDriver webDriver = null;

            switch (brower)
            {
                case Browser.Firefox:
                    var optionsFirefox = new FirefoxOptions();
                    
                    if (headless)
                        optionsFirefox.AddArgument("--headless");

                    webDriver = new FirefoxDriver(caminhoDriver, optionsFirefox);

                    break;
                case Browser.Chrome:
                    var options = new ChromeOptions();

                    if (headless)
                        options.AddArgument("--headless");

                    webDriver = new ChromeDriver(caminhoDriver, options);

                    break;
            }

            return webDriver;
        }
    }
}
