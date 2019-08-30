using OpenQA.Selenium;
using System.Linq;
using OpenQA.Selenium.Remote;


namespace UITestProject
{
    static class WebElementExtensions
    {

        /// <summary>
        ///  Очищает контент элемента и печатает текст в поле элемента
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IWebElement ClearAndType(this IWebElement elem, string text)
        {
            if (text != null)
            {
                elem.Clear();
                elem.SendKeys(text);
            }

            return elem;
        }

        public static IWebElement FindElementSafe(this IWebElement element, By by)
        {
            try
            {
                return element.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

    }
}
