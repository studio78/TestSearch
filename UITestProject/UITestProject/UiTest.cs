using NUnit.Framework;

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

        [Test, Description("Проверка получения результатов поиска по названию и ФИО")]
        public void Search()
        {
            // поиск по названию организации
            var results = PageBase.Search("Газпром");
            // поиск по ФИО
            var results2 = PageBase.Search("Менделеев Дмитрий Иванович");

            // можно в результатах, поискать ссылку на Вики и открыть ее
            foreach (var result in results2)
            {
                if (result.Name.Contains("Википедия"))
                {
                    PageBase.LinkClick(result.Link);
                }
            }

        }

        [Test, Description("Проверка текста всплывающей подсказки в поле поиска")]
        public void Tooltip()
        {
            Asserts.AssertTooltipSearchField("поиск");
        }

        [Test, Description("Проверка пустой области результатов")]
        public void ClickLogo()
        {
            PageBase.Search("Газпром");
            PageBase.LogoClick();
            Asserts.AssertEmptyResult();
        }

        [TearDown]
        public void CleanUp()
        {
        }

    }
}
