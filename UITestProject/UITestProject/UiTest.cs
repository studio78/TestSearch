using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace UITestProject
{
    [TestFixture(Description = "Test")]
    public class UiTest : TestBase
    {

        [SetUp]
        public void Init()
        {
            Context.driver.Navigate().GoToUrl("http://www.google.com");
        }

        [Test]
        public void SearchByName()
        {
            var searchValue = "Газпром";
            // поиск по названию организации
            PageBase.Search(searchValue);
            if (PageBase.NotFound()) { throw new WebDriverException($"По запросу {searchValue} ничего не найдено");}

            var results = PageBase.GetMatchResults(new List<string> { searchValue} );
        }

        [TearDown]
        public void CleanUp()
        {
        }

    }
}
