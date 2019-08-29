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

        [Test]
        public void SearchByName()
        {
            // поиск по названию организации
            PageBase.Search("Газпром");

        }

        [TearDown]
        public void CleanUp()
        {
        }

    }
}
