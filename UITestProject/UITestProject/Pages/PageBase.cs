using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
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
        public PageBase Search(string value)
        {
            var field = driver.FindElement(By.CssSelector("input[class='gLFyf gsfi'"));
            // можно так
            field.ClearAndType(value+"\n");

            // можно так
//            field.ClearAndType(value);
//            field.SendKeys(Keys.Enter);
            return this;
        }

        /// <summary>
        /// Проверка наличия на странице подписи о том, что ничего не найдено
        /// </summary>
        public bool NotFound()
        {
            return driver.FindElement(By.CssSelector("div#topstuff")).Text.Contains("ничего не найдено");
        }

        public List<Result> GetAllResults(List<string> filter)
        {
            var result = new List<Result>();
            var allDiv = driver.FindElements(By.CssSelector("div.rc"));
            foreach (var div in allDiv)
            {
                var name = div.FindElement(By.CssSelector("div.ellip")).Text;
                if (string.IsNullOrEmpty(name)) continue;
//                if (!filter.All(s => name.Contains(s))) continue;
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

        public List<Result> GetMatchResults(List<string> filter)
        {
            var allResult = GetAllResults(filter);
            var result = new List<Result>();
            foreach (var record in allResult)
            {
                if (!filter.All(s => record.Name.Contains(s))) continue;
                result.Add(record);
            }
            return result;
        }

//        public List<Result> GetContainsResults(List<string> filter)
//        {
//            var allResult = GetAllResults(filter);
//            var result = new List<Result>();
//            foreach (var record in allResult)
//            {
//                if (!filter.Any(s => record.Name.Contains(s))) continue;
//                result.Add(record);
//            }
//
//            return allResult.Where(x => (filter.Any(s => x.Name.Contains(s))));
////            return result;
//        }

        /*
        /// <summary>
        ///  Поиск по подстроке Id и клик
        /// </summary>
        /// <param name="idSubstr"></param>
        public void ButtonClick(string idSubstr)
        {
            driver.FindByIdSubstring(idSubstr).Click();
        }

        public void ButtonSubmit(string idSubstr)
        {
            driver.FindByIdSubstring(idSubstr).Submit();
        }

        /// <summary>
        /// Клик по элементу
        /// </summary>
        public void ClickElement(By locator)
        {
            driver.FindElement(locator).Click();
            Thread.Sleep(100);
        }

        /// <summary>
        ///  Клик по кнопке посредством передачи Enter
        /// </summary>
        /// <param name="idSubstr"></param>
        public void ButtonClickEnter(string idSubstr)
        {
            driver.FindByIdSubstring(idSubstr).SendKeys(Keys.Enter);
        }
        /// <summary>
        ///  Поиск по неполному id(подстроке) элемента
        /// </summary>
        /// <param name="idSubstr"></param>
        /// <returns></returns>
        public IWebElement GetElementByIdSubstr(string idSubstr)
        {
            return driver.FindByIdSubstring(idSubstr);
        }
        /// <summary>
        /// Очищает контент элемента и печатает текст в поле элемента
        /// </summary>
        /// <param name="idSubstr"></param>
        /// <param name="text"></param>
        public void TypeIntoElement(string idSubstr, string text)
        {
            driver.FindByIdSubstring(idSubstr).ClearAndType(text);
        }
        /// <summary>
        /// Вторичный вызов элемента при пререзагрузке(обновлении updatepanel) страницы
        /// </summary>
        /// <param name="idSubstr"></param>
        /// <param name="text"></param>
        public void TypeStaleElement(string idSubstr, string text)
        {
            IWebElement el = driver.FindByIdSubstring(idSubstr);
            el.Click();
            Thread.Sleep(1000);
            // для устранения stale
            IWebElement element = driver.FindByIdSubstring(idSubstr);
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);
            element.SendKeys(text);
        }

        /// <summary>
        /// Печать текста в элементе
        /// </summary>
        public void TypeIntoElement(By locator, string text)
        {
            driver.FindElement(locator).ClearAndType(text);
        }

        /// <summary>
        /// Получение значения value элемента
        /// </summary>
        public string GetElementValue(By locator) => driver.FindElement(locator).GetAttribute("value");

        public void LinkByTextClick(string searchText)
        {
            driver.FindElement(By.LinkText(searchText)).Click();
            // IWebElement el= driver.FindElement(By.LinkText(searchText));
            // // в IE работает только так:
            //if(driver.GetType().Name== "InternetExplorerDriver")
            // {
            //     IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            //     js.ExecuteScript("arguments[0].click();", el);
            //     return;
            // }
            // el.Click();
        }
        /// <summary>
        ///  Данные из поля настроек для ПАП Дата посещения, День
        /// </summary>
        public string GetSettingsPapFinishDay => GetElementValue(By.CssSelector("[id*='txtFinishDay']"));
        /// <summary>
        ///  Данные из поля настроек для ПАП Дата посещения, Месяц	
        /// </summary>
        public string GetSettingsPapFinishMonth => GetElementValue(By.CssSelector("[id*='txtFinishMonth']"));
        /// <summary>
        ///  Получить таблицу по Id
        /// </summary>
        /// <param name="idSubstr"></param>
        /// <returns></returns>
        public IWebElement GetTable(string idSubstr)
        {
            return driver.GetTable(idSubstr);
        }


        /// <summary>
        /// Поиск по тексту
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        /// Применение:   FindByText("span", "Ok").Click();
        public IWebElement FindByText(string tagName, string text)
        {
//            return driver.FindByText(tagName, text);
            return null;
        }

        public IWebElement FindByAttr(string attr, string value)
        {
//            return driver.FindByAttr(attr, value);
        }


        public IWebElement GetDropDownItem(string id, string dropDownItemTitle)
        {
            return driver.GetDropDownItem(id, dropDownItemTitle);
        }
        /// <summary>
        ///  Поиск по значению атрибута value для option
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dropDownItemTitle"></param>
        /// <returns></returns>
        public IWebElement GetDropDownOptionValue(string id, string dropDownOptionValue)
        {
            return driver.GetDropDownOptionValue(id, dropDownOptionValue);
        }
        /// <summary>
        /// Возвращает текущее значение вып. списка
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        public string GetDropDownOptionValue(By locator)
        {
            var dropdown = driver.FindElement(locator);
            return dropdown
                      .FindElements(By.TagName("option"))
                      .FirstOrDefault(item => item.Selected).GetAttribute("value");
        }

        /// <summary>
        /// Выбор в выпадающем списке необходимого элемента
        /// </summary>
        public void SelectDropDownItem(By locator, string dropDownItemTitle)
        {
            if (string.IsNullOrEmpty(dropDownItemTitle)) return;
            var dropdown = driver.FindElement(locator);
            dropdown.Click();
            var element = dropdown.FindElements(By.TagName("option")).FirstOrDefault(item => item.Text == dropDownItemTitle);
            if (element != null) element.Click();
            else throw new WebDriverException($"Указанный элемент: {dropDownItemTitle} отсутствует в списке");
        }

        /// <summary>
        ///    Возвращает значение(value) атрибута элемента
        /// </summary>
        /// <param name="idSubstr"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public string GetAttributeValue(string idSubstr, string attrName)
        {
            return driver.FindByIdSubstring(idSubstr).GetAttribute(attrName);
        }

        public IWebElement FindElementWithTimeout(string idSubstr, int timeoutInSeconds)
        {
            return driver.FindElementWithTimeout(idSubstr, timeoutInSeconds);
        }


        public IWebElement FindRowWithTimeout(int timeoutInSeconds, string idTableSubstr, string value)
        {
            return driver.FindRowWithTimeout(timeoutInSeconds, idTableSubstr, value);
        }

        /// <summary>
        ///  Наличие элемента на странице
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool WaitElementExisits(By locator, int timeout)
        {

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            try
            {
                IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
                return element.Displayed;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// Создает строку из рандомных символов
        /// </summary>
        /// <param name="size">длина строки</param>
        /// <param name="type">Тип рандомной строки. Русские символы = "rus"; английские символы = "eng"; числа = "nums"; русско-английские символы = "ruseng"; русско-английские символы и числа = "all"</param>
        /// <returns></returns>
        /// выносим рандом вне метода иначе будут одинаковые строки
        private readonly Random rng = new Random((int)DateTime.Now.Ticks);
        public string GetRandomString(int size, string type)
        {
            string chars = "абвгдежзийклмнопрстуфхцчшщъыьэюяАБВГДЕЖЗИЙКЛМНОПРСТУФЧЦЧШЩЪЫЬЭЮЯ";
            string nums = "0123456789";
            string charseng = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";

            char[] rnd = new char[size];
            string a = null;

            switch (type)
            {
                case "rus":
                    a = chars;
                    break;

                case "eng":
                    a = charseng;
                    break;

                case "nums":
                    a = nums;
                    break;
                case "ruseng":
                    a = chars + charseng;
                    break;
                case "all":
                    a = chars + charseng + nums;
                    break;
            }

            for (int i = 0; i < size; i++)
            {
                rnd[i] = a[rng.Next(a.Length)];
            }
            return new string(rnd);

        }

        public void Logout()
        {
            ButtonClick(CommonConst.ButtonExit);
        }

        #region Поиск с ожиданием

        /// <summary>
        ///  ExpectedConditions.ElementExists
        /// </summary>
        /// <param name="idSubstr"></param>
        /// <param name="timeout">по умолчанию 10 сек</param>
        public void WaitUntilElementExists(string idSubstr, int timeout = 10)
        {
            driver.WaitUntilElementExists(idSubstr, timeout);
        }
        /// <summary>
        /// Ожидание открытия страницы с url включающим искомую часть url строки
        /// ExpectedConditions.UrlContains
        /// </summary>
        public void WaitUntilUrlContains(string url)
        {
            driver.WaitUntilUrlContains(url);
        }


        /// <summary>
        /// ExpectedConditions.ElementIsVisible
        /// </summary>
        /// <param name="idSubstr"></param>
        /// <param name="timeout">по умолчанию 10 сек</param>
        public void WaitUntilElementVisible(string idSubstr, int timeout = 10)
        {
            driver.WaitUntilElementVisible(idSubstr, timeout);
        }
        /// <summary>
        /// ExpectedConditions.ElementToBeClickable
        /// </summary>
        /// <param name="idSubstr"></param>
        /// <param name="timeout"></param>
        public void WaitUntilElementClickable(string idSubstr, int timeout = 10)
        {
            driver.WaitUntilElementClickable(idSubstr, timeout);
        }
        public void WaitUntilElementClickable(IWebElement element, int timeout = 10)
        {
            driver.WaitUntilElementClickable(element, timeout);
        }
        /// <summary>
        ///  элемент, на который ссылались, удаляется из структуры DOM напр. при клике и редиректе
        /// </summary>
        /// <param name="idSubstr"></param>
        /// <param name="timeout"></param>
        public void WaitUntilClickForPageToLoad(string idSubstr, int timeout = 10)
        {
            driver.WaitAndClickForPageToLoad(idSubstr, timeout);
        }




        #endregion


        /// <summary>
        /// Получение ФИО по типу User или Person
        /// </summary>
        public static string GetPersonFio<T>(T person)
        {
            if (person.GetType().Equals(typeof(Person)))
            {
                Person p = person as Person;
                return (!string.IsNullOrEmpty(p.Lastname) ? p.Lastname : "") + (!string.IsNullOrEmpty(p.Firstname) ? " " + p.Firstname : "") +
                              (!string.IsNullOrEmpty(p.Middlename) ? " " + p.Middlename : "");
            }
            if (person.GetType().Equals(typeof(User)) || person.GetType().BaseType.Equals(typeof(User)))
            {
                User p = person as User;
                return p.Lastname + (!string.IsNullOrEmpty(p.Firstname) ? " " + p.Firstname : "") +
                               (!string.IsNullOrEmpty(p.Middlename) ? " " + p.Middlename : "");

            }

            throw new WebDriverException("Тип FIO не определён.");


        }

        /// <summary>
        /// Включение/выключение флага у CheckBox
        /// </summary>
        /// <param name="locator">локатор</param>
        /// <param name="value">true/false</param>
        public void CheckBoxSelect(By locator, bool value)
        {
            driver.FindElement(locator).CheckBoxSelect(value);
        }

        /// <summary>
        /// Возвращает количество строк в таблице
        /// </summary>
        /// <param name="locator">селектор</param>
        public int TableRowsCount(By locator)
        {
            IWebElement baseTable = driver.FindElement(locator);
            List<IWebElement> tableRows = baseTable.FindElements(By.CssSelector("tr[class*='odd'],tr[class*='even']")).ToList();
            return tableRows.Count;
        }

        /// <summary>
        /// Переключение на модальное окно
        /// </summary>
        public void SwitchToModalDialog()
        {
            Thread.Sleep(500);
            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("[id*='dialog-body-mod-dlg']")));
        }

        /// <summary>
        /// Переключение с модального окна обратно
        /// </summary>
        public void SwitchToDefault()
        {
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(500);
        }

        /// <summary>
        /// Метод получения id всех элементов на странице конкретного тега
        /// </summary>
        /// <param name="tagName">имя тега</param>
        /// <param name="attr">атрибут для получения</param>
        public void GetControlLocators(string tagName, string fileName, string attr = "id")
        {
            var javascriptDriver = (IJavaScriptExecutor)driver;

            var tags = driver.FindElements(By.TagName(tagName)).ToList();
            var tw = new StreamWriter(fileName);

            foreach (var tag in tags)
            {
                var str = tag.GetAttribute(attr);
                if (string.IsNullOrEmpty(str))
                {
                    var attributes = javascriptDriver.ExecuteScript("var items = {}; for (index = 0; index < arguments[0].attributes.length; ++index) { items[arguments[0].attributes[index].name] = arguments[0].attributes[index].value }; return items;", tag) as Dictionary<string, object>;
                    var s = new StringBuilder(); ;
                    s.Append(tagName);
                    foreach (var attribute in attributes)
                    {
                        s.Append($" {attribute.Key}='{attribute.Value}'");
                    }
                    str = s.ToString();
                }
                tw.WriteLine(str);
            }
            tw.Close();
        }

        /// <summary>
        /// Обрезает строковые значения в списке до определенной длины
        /// </summary>
        /// <param name="values">список строк</param>
        /// <param name="maxLength">допустимая длина</param>
        /// <returns></returns>
        public static List<string> CropLength(List<string> values, int maxLength)
        {
            var list = new List<string>();
            values.RemoveAll(x => x == null);
            foreach (var value in values)
            {
                if (value.Length > maxLength)
                {
                    list.Add(value.Substring(0, maxLength - 1));
                }
                else list.Add(value);
            }

            return list;
        }
        */
    }
    
}
