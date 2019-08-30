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

        /// <summary>
        /// Подсветка элемента на странице (для отладки тестов)
        /// </summary>
        public static IWebElement Highlight(this IWebElement context, string color = "red")
        {
            var rc = (RemoteWebElement)context;
            var driver = (IJavaScriptExecutor)rc.WrappedDriver;
            var script = @"arguments[0].style.cssText = ""border-width: 2px; border-style: solid; border-color: " + color + @"""; ";
            driver.ExecuteScript(script, rc);
            return context;
        }

        /// <summary>
        /// Включение/выключение флага у CheckBox
        /// </summary>
        /// <param name="context">checkbox</param>
        /// <param name="value">true/false</param>
        public static IWebElement CheckBoxSelect(this IWebElement context, bool value)
        {
            if ((value && !context.Selected) || (!value && context.Selected))
            {
                context.Click();
            }
            return context;
        }

        /// <summary>
        /// Выбор в выпадающем списке необходимого элемента
        /// </summary>
        public static IWebElement SelectDropDownItem(this IWebElement context, string value)
        {
            context.Click();
            var element = context.FindElements(By.TagName("option")).FirstOrDefault(item => item.Text == value);
            element.Click();

            return context;
        }
    }
}
