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

        public static IWebElement FindElementSafe(this IWebDriver driver, By by)
        {
            try
            {
                return driver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
        //        public static bool ExistElement(this IWebDriver driver, By locator)
        //        {
        //            driver.FindElements(locator).
        //            return element;
        //        }
    }
}
