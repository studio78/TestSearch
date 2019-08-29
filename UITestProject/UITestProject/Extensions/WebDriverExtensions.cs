using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace UITestProject
{
    public static class WebDriverExtensions
    {

        public static void ClearLocalStorage(this IWebDriver driver)
        {
            driver.ExecuteJavaScript("localStorage.clear();");
        }
    }
}
