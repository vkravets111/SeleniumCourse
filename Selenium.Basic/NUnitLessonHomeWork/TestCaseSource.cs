using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium.Basic.NUnitLessonHomeWork
{
    public class TestCaseSource
    {
        IWebDriver driver = null;

        [OneTimeSetUp]
        public void TestSetUp()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://atata-framework.github.io/atata-sample-app/#!/");
            driver.FindElement(By.Id("sign-in")).Click();
        }

        [SetUp]
        public void RunBefroreEveryTest()
        {
            driver.FindElement(By.CssSelector("input#email")).Clear();
            driver.FindElement(By.CssSelector("input#email")).SendKeys("admin@mail.com");
            driver.FindElement(By.CssSelector("input#password")).Clear();
            driver.FindElement(By.CssSelector("input#password")).SendKeys("abc123");
            driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();
        }

        [TearDown]
        public void RunAfterEveryTest()
        {
            driver.FindElement(By.ClassName("caret")).Click();
            driver.FindElement(By.XPath("//a[text()='Sign Out']")).Click();
        }

        [OneTimeTearDown]
        public void RunAfterAllTests()
        {
            driver.Quit();
        }

        [TestCaseSource(typeof(TestsSource), "Emails"), Timeout(6000)]
        public void VerifyThatDefaultUsersArePresent(string email)
        {
            Assert.True(driver.FindElement(By.XPath($"//td[contains(text(), '{email}')]")).Displayed);
        }

        class TestsSource
        {
            static readonly object[] Emails =
    {
        new object[] { "admin@mail.com"},
        new object[] { "jane.smith@mail.com" }
    };
            static readonly object[] Buttons =
            {
        new object[] {"View"},
        new object[] {"Edit"},
        new object[] {"Delete"}

            };
        }

        [TestCaseSource(typeof(TestsSource), "Buttons"), Timeout(6000)]
        public void VerifyButtonsAreDisplayed(string buttonName)
        {
            Assert.True(driver.FindElement(By.XPath($"//*[@class='btn btn-default'][contains(text(), '{buttonName}')]")).Displayed);
        }
    }
}
