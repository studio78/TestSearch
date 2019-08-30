using System;
using OpenQA.Selenium;
using NUnit.Framework;

namespace UITestProject
{
    public class Asserts : PageBase
    {
        public Asserts(IWebDriver driver) : base(driver)
        {

        }

        /// <summary>
        /// Проверка текста подсказки для поля поиска
        /// </summary>
        /// <param name="tooltipExpected">ожидаемый текст подсказки</param>
        /// <param name="strCopm">параметр регистрозависимого поиска</param>
        public void AssertTooltipSearchField(string tooltipExpected, StringComparison strCopm = StringComparison.InvariantCultureIgnoreCase)
        {
            var tooltipText = driver.FindElement(By.CssSelector("input[class='gLFyf gsfi'")).GetAttribute("title");
            Assert.True(tooltipText.Equals(tooltipExpected, strCopm), $"Ожидаемая подсказка {tooltipExpected} не соответствует полученной {tooltipText}");
        }

        /// <summary>
        /// Проверка отсутствия области с результатами
        /// </summary>
        public void AssertEmptyResult()
        {
            if (driver.FindElementSafe(By.CssSelector("div#rcnt"))!=null) { throw new WebDriverException("На странице отображается область с результатами поиска, а не должна");}
        }

    }
}
