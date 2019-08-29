using System;
using System.Linq;
using OpenQA.Selenium;
using NUnit.Framework;
using System.Threading;

namespace UITestProject
{
    public class Asserts : PageBase
    {
        public Asserts(IWebDriver driver) : base(driver)
        {

        }

        /// <summary>
        ///  Подтверждение перехода на страницу по заголовку
        /// </summary>
        public void AssertFoundTitlePage(string pageTitle, string titleExpected)
        {
            Assert.True(pageTitle.ToLower().StartsWith(titleExpected.ToLower()));
        }

        /// <summary>
        ///  Подтверждение перехода по заданному url
        /// </summary>
        public void AssertFoundUrl(string expectedUrl)
        {
//            WaitUntilUrlContains(expectedUrl);
            Assert.That(driver.Url, Does.Contain(expectedUrl).IgnoreCase);
        }
        /// <summary>
        ///  Проверка наличия текста по подстроке
        /// </summary>
        public void AssertFoundField(By id, string searchValue)
        {
            Assert.True(driver.FindElement(id).Text.Contains(searchValue));
        }




        /// <summary>
        /// Проверка правильности заполнения поля
        /// </summary>
        public void AssertTypedField(By locator, string searchValue, bool toUpperCase = false)
        {
            searchValue = searchValue ?? ""; // если null присваиваем строку-""
            if (toUpperCase && !String.IsNullOrEmpty(searchValue))
                searchValue = searchValue.ToUpper();
            var element = driver.FindElement(locator);

//            var value = string.IsNullOrEmpty(element.Text) ? GetElementValue(locator) : element.Text;

//            Assert.AreEqual(value, searchValue, "В поле '{0}' ожидалось значение: '{1}', получено: '{2}'", locator, searchValue, value);
        }

        /// <summary>
        ///  Проверка правильности отображения текста на странице
        /// </summary>
        public void AssertMessageField(string searchValue)
        {
//            Assert.True(driver.FindByTag("body").Text.Contains(searchValue));
        }

        /// <summary>
        ///  Проверка правильности заполнения поля
        /// </summary>
        /// <param name="id"></param>
        /// <param name="searchValue"></param>
        public void AssertSelectedDropdown(By locator, string searchValue)
        {
            var dropdown = driver.FindElement(locator);
            var it = dropdown.FindElements(By.TagName("option")).FirstOrDefault(item => item.Text == searchValue);
            Assert.IsNotNull(it, "Список не содержит ожидаемое значение: " + searchValue);
            Assert.True(it.Selected, "В списке не выбрано ожидаемое значение: " + searchValue);
        }




        /// <summary>
        ///  Проверка что элемент отсутствует на странице
        /// </summary>
        /// <param name="idSubstr"></param>
        /// <param name="element"></param>
        public void AssertElementNotExist<T>(T locator, IWebElement element = null)
        {
            try
            {
                if (locator.GetType().Equals(typeof(By)))
                {
                    By by = locator as By;
                    if (element != null)
                        element.FindElement(by);
                    else
                        driver.FindElement(by);
                }
                else if (locator.GetType().Equals(typeof(string)))
                {
                    string substr = locator as string;
                    // проверить что элемента нет

/*
                    if (element != null)
                        element.FindByIdSubstring(substr);
                    else
                        driver.FindByIdSubstring(substr);
*/
                }
            }
            catch (NoSuchElementException ex)
            {
                Assert.True(ex.Message.Contains("no such element"));
            }
        }



    }
}
