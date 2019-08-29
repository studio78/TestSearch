using NUnit.Framework;

namespace UITestProject
{
    public abstract class TestBase
    {
        public SeleniumTest Context;
        public PageBase PageBase;
        public Asserts Asserts;

        [OneTimeSetUp]
        public void Start()
        {
            Context = new SeleniumTest("ChromeDriver");
            PageBase = new PageBase(Context.driver);
            Asserts = new Asserts(Context.driver);
        }

        [OneTimeTearDown]
        public void Finish()
        {
            Context.driver.ClearLocalStorage();
            Context.driver.Quit();
        }

    }
}
