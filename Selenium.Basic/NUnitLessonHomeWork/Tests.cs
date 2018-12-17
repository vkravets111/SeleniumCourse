using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Selenium.Basic.NUnitLesson
{
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(InternetExplorerDriver))]
    [Author("Vitalii Kravets", "vk@example.com")]
    public class AtataTests<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        IWebDriver driver = null;
        [OneTimeSetUp]
        public void TestSetUp()
        {
            this.driver = new TWebDriver();
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
            
       [Test, Order(1), Timeout(5000)]
       public void VerifySuccessfullLogin()
        {
            Assert.True(driver.FindElement(By.XPath("//a[contains(text(),'Account')]")).Displayed);
        }

        
    }
}
