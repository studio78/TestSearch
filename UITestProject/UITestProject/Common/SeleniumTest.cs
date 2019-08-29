using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace UITestProject
{
    /// <summary>
    /// Базовый класс для всех тестов, использующих Selenium.
    /// Создает(при старте) и уничтожает(после завершения тестов) необходимый драйвер браузера.
    /// </summary>
    public class SeleniumTest
    {
        public IWebDriver driver;

        public SeleniumTest(string typeDriver)
        {
            switch (typeDriver)
            {
                case nameof(ChromeDriver):
                    driver = new ChromeDriver();
                    break;
                case nameof(FirefoxDriver):
                    driver = new FirefoxDriver();
                    break;
                case nameof(InternetExplorerDriver):

                    driver = new InternetExplorerDriver(new InternetExplorerOptions()
                    {
                        IgnoreZoomLevel = true,
                        EnablePersistentHover = true,
                        EnableNativeEvents = false,
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                        UnhandledPromptBehavior = UnhandledPromptBehavior.Accept
                    });
                    break;
                case nameof(EdgeDriver):
                    {
                        var options = new EdgeOptions();
                        options.AddAdditionalCapability("IgnoreZoomLevel", false);
                        driver = new EdgeDriver(options);
                        break;
                    }
                default:
                    throw new NotSupportedException($"Driver ${typeDriver} not supported");
            }
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
        }

        public void Quit()
        {
            driver.ClearLocalStorage();
            driver.Quit();
        }
    }
}