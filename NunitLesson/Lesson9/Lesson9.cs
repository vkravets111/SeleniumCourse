using Bogus;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LoggingLesson.Lesson9
{
    [TestFixture]
    public class Lesson9
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public static IWebDriver driver = null;


        public static Lesson9 GenerateUser()
        {
            return new Faker<Lesson9>()
               .RuleFor(l => l.Name, f => f.Name.FirstName())
               .RuleFor(l => l.Surname, f => f.Name.LastName())
               .RuleFor(l => l.Company, f => f.Company.CompanyName())
               .RuleFor(l => l.Email, f => f.Internet.Email())
               .RuleFor(l => l.Password, f => f.Internet.Password(15, true, "123", "Qw123#@"))
              .Generate();

        }

        [OneTimeSetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void RandomDataWithFluentAssertion()
        {
            var user = GenerateUser();

            driver.Navigate().GoToUrl("https://hwdecoration20181219063908.azurewebsites.net/");
            driver.FindElement(By.Id("registerLink")).Click();
            driver.FindElement(By.Id("Email")).SendKeys(user.Email);
            driver.FindElement(By.Id("Name")).SendKeys(user.Name);
            driver.FindElement(By.Id("Surname")).SendKeys(user.Surname);
            driver.FindElement(By.Id("Company")).SendKeys(user.Company);

            driver.FindElement(By.Id("Password")).SendKeys(user.Password);
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys(user.Password);
            driver.FindElement(By.Id("ConfirmPassword")).Submit();

            driver.FindElement(By.XPath($"//a[@title][contains(text(), 'Hello {user.Email}')]")).Click();

            var actualUser = new AcutalUser().ReturnActualUser();

            actualUser.ActualCompany.Should().ContainEquivalentOf(user.Company);
            actualUser.ActualName.Should().ContainEquivalentOf(user.Name);
            actualUser.ActualSurname.Should().ContainEquivalentOf(user.Surname);
            actualUser.ActualEmail.Should().ContainEquivalentOf(user.Email);

            driver.FindElement(By.XPath("//a[text()='Log off']")).Click();

            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("Email")).SendKeys(user.Email);
            driver.FindElement(By.Id("Password")).SendKeys(user.Password);
            driver.FindElement(By.Id("Password")).Submit();


            driver.FindElement(By.XPath("//a[@title]")).Text.Should().EndWith(user.Email + "!");

        }

        [OneTimeTearDown]
        public void Teardown()
        {
             driver.Quit();
        }

       

        public class AcutalUser
            {
            public string ActualName { get; set; }
            public string ActualSurname { get; set; }
            public string ActualCompany { get; set; }
            public string ActualEmail { get; set; }

            public AcutalUser ReturnActualUser()
            {
                var user = new AcutalUser();
                user.ActualName = driver.FindElement(By.Id("userName")).Text;
                user.ActualSurname = driver.FindElement(By.Id("userSurname")).Text;
                user.ActualCompany = driver.FindElement(By.Id("UserCompany")).Text;
                user.ActualEmail = driver.FindElement(By.Id("userEmail")).Text;

                return user;

            }
        }

    }

    
}
