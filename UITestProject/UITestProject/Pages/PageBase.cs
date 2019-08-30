using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using UITestProject.Common;

namespace UITestProject
{
    public class PageBase
    {
        protected IWebDriver driver;
        public PageBase(IWebDriver driver)
        {
            this.driver = driver;
        }

        /// <summary>
        /// Заполнение поля поиска и нажатие ввод
        /// </summary>
        /// <param name="value">искомое значение</param>
        /// <returns></returns>
        public List<Result> Search(string value)
        {
            // поле для поиска
            var field = driver.FindElement(By.CssSelector("input[class='gLFyf gsfi'"));
            // можно так заполнить и нажать ввод
            field.ClearAndType(value+"\n");
            // можно так
            //            field.ClearAndType(value);
            //            field.SendKeys(Keys.Enter);
            
            // если ничего не нашли прерываем тест
            if (NotFound()) { throw new WebDriverException($"По запросу {value} ничего не найдено"); }

            return GetAllResults(value); ;
        }

        /// <summary>
        /// Проверка наличия на странице подписи о том, что ничего не найдено
        /// </summary>
        private bool NotFound()
        {
            return driver.FindElement(By.CssSelector("div#topstuff")).Text.Contains("ничего не найдено");
        }

        /// <summary>
        /// Получение всех результатов поиска
        /// </summary>
        /// <param name="value">искомое значение</param>
        /// <returns></returns>
        public List<Result> GetAllResults(string value)
        {
            var filter = value.Split();
            var result = new List<Result>();
            var allDiv = driver.FindElements(By.CssSelector("div.rc"));
            foreach (var div in allDiv)
            {
                var name = div.FindElement(By.CssSelector("div.ellip")).Text;
                if (string.IsNullOrEmpty(name)) continue;
                if (!filter.All(s => name.Contains(s))) continue;
                var descSpan = div.FindElementSafe(By.CssSelector("span.st"));
                result.Add(new Result
                {
                    Name = name,
                    Link = new Uri(div.FindElement(By.CssSelector("a")).GetAttribute("href")),
                    Description = descSpan?.Text
                });
            }

            return result;
        }

        /// <summary>
        /// Клик по ссылке
        /// </summary>
        /// <param name="link">ссылка</param>
        /// <returns></returns>
        public PageBase LinkClick(Uri link)
        {
            driver.FindElement(By.CssSelector($"a[href='{link.AbsoluteUri}']")).Click();
            return this;
        }

        /// <summary>
        /// Клик по логу поисковика
        /// </summary>
        /// <returns></returns>
        public PageBase LogoClick()
        {
            driver.FindElement(By.Id("logo")).Click();
            return this;
        }

    }
    
}
