using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LoggingLesson.Lesson9
{
    [TestFixture]
    public class Lesson9
    {
        IWebDriver driver = null;

        [OneTimeSetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void RandomDataWithFluentAssertion()
        {
            var faker = new Bogus.Faker();
            string firstName = faker.Name.FirstName();
            string lastName = faker.Name.LastName();
            string company = faker.Company.CompanyName();
            string email = faker.Internet.Email(firstName, lastName);
            string password = faker.Internet.Password(15, true, "123", "Qw123#@");
            
            driver.Navigate().GoToUrl("https://hwdecoration20181219063908.azurewebsites.net/");
            driver.FindElement(By.Id("registerLink")).Click();
            driver.FindElement(By.Id("Email")).SendKeys(email);
            driver.FindElement(By.Id("Name")).SendKeys(firstName);
            driver.FindElement(By.Id("Surname")).SendKeys(lastName);
            driver.FindElement(By.Id("Company")).SendKeys(company);
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys(password);
            driver.FindElement(By.Id("ConfirmPassword")).Submit();

            driver.FindElement(By.XPath($"//a[@title][contains(text(), 'Hello {email}')]")).Click();

            driver.FindElement(By.Id("userName")).Text.Should().ContainEquivalentOf(firstName);
            driver.FindElement(By.Id("userSurname")).Text.Should().ContainEquivalentOf(lastName);
            driver.FindElement(By.Id("UserCompany")).Text.Should().ContainEquivalentOf(company);
            driver.FindElement(By.Id("userEmail")).Text.Should().ContainEquivalentOf(email);

            driver.FindElement(By.XPath("//a[text()='Log off']")).Click();

            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("Email")).SendKeys(email);
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.Id("Password")).Submit();


            driver.FindElement(By.XPath("//a[@title]")).Text.Should().EndWith(email + "!");

        }

        [OneTimeTearDown]
        public void Teardown()
        {
            driver.Quit();
        }
    }
}
