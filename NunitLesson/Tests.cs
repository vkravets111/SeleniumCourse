using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Serilog;
using System;

namespace Selenium.Basic.NUnitLesson
{

    [Author("Vitalii Kravets", "vk@example.com")]
    public class AtataTests
    {
        IWebDriver driver = null;
        [OneTimeSetUp]
        public void TestSetUp()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://atata-framework.github.io/atata-sample-app/#!/");
            driver.FindElement(By.Id("sign-in")).Click();
            Log.Debug("Opening Chrome Browser");
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
            Log.Information("Is Login successfull");
            Assert.True(driver.FindElement(By.XPath("//a[contains(text(),'Account')]")).Displayed);
        }

        [TestCaseSource(typeof(TestsSource), "Emails"), Timeout(6000)]
        public void VerifyThatDefaultUsersArePresent(string email)
        {
            try
            {
                Assert.True(driver.FindElement(By.XPath($"//td[contains(text(), '{email}')]")).Displayed);
            }

            catch (Exception exception)
            {
                Log.Error(exception, "1 test fails");
            }           
            
        }

        [TestCaseSource(typeof(TestsSource), "Buttons"), Timeout(6000)]
        public void VerifyButtonsAreDisplayed(string buttonName)
        {
            Log.Information("Checking if default buttons are displayed");
            Assert.True(driver.FindElement(By.XPath($"//*[@class='btn btn-default'][contains(text(), '{buttonName}')]")).Displayed);
        }

        class TestsSource
        {
            static readonly object[] Emails =
    {
        new object[] { "admin@mail.com"},
        new object[] { "failed test" }
    };
            static readonly object[] Buttons =
            {
        new object[] {"View"},
        new object[] {"Edit"},
        new object[] {"Delete"}

            };
        }
    }
}

