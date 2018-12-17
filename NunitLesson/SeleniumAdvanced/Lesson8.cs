using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.IO;
using System.Linq;

namespace LoggingLesson.SeleniumAdvanced
{
    [TestFixture]
    public class Lesson8
    {
        IWebDriver driver;
      

        [Test]
        public void Lesson8Test1()
        {
           string downloadFilepath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            var options = new ChromeOptions();
            
            options.AddArguments
                (
                    "--start-maximized"
                );
            //options.AddUserProfilePreference("download.default_directory", downloadFilepath);
            options.AddUserProfilePreference("download.default_directory", @"C:\TestResults");
            var driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://unsplash.com/search/photos/test");

            //scroll to the bottom
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 300)");
            //var actions = new Actions(driver);
            System.Threading.Thread.Sleep(2000);
            By picture = By.XPath("(//img[@itemprop='thumbnailUrl'])[42]");
            driver.FindElement(picture).Click();

            var click = driver.FindElement(By.ClassName("_2Aga-"));
            IJavaScriptExecutor executor = driver;
            executor.ExecuteScript("arguments[0].click();", click);

            driver.Quit();

            Assert.That(Directory.GetFiles(@"C:\TestResults", "rawpixel-609006-unsplash.jpg"), Is.Not.Empty);
        }
    }
}
