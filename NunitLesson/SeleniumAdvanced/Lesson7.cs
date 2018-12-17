using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Linq;
using System.Threading;

namespace LoggingLesson.SeleniumAdvanced
{
    [TestFixture]
    public class Lesson7
    {
        IWebDriver driver = null;

        [OneTimeSetUp]
        public void TestSetUp()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();
        }
        [Test]
        public void Test1()
        {
            driver.Navigate().GoToUrl("http://www.leafground.com/home.html");
            By hyperLink = By.XPath("//h5[contains(text(), 'HyperLink')]");
            new Actions(driver).KeyDown(Keys.Control).Click(driver.FindElement(hyperLink)).Perform();
            driver.SwitchTo().Window(driver.WindowHandles.Last());

            By goToHomePage = By.XPath("//a[text()='Go to Home Page']");
            new Actions(driver).MoveToElement(driver.FindElement(goToHomePage)).Perform();
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
         
            var destinationPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            var screenshotPath = System.IO.Path.Combine(destinationPath, "screenshot.png");
            screenshot.SaveAsFile(screenshotPath);
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.First());

        }

        [Test]
        public void Test2()
        {
            driver.Navigate().GoToUrl("https://jqueryui.com/droppable/");
            var actions = new Actions(driver);
            By frameLocator = By.ClassName("demo-frame");
            driver.SwitchTo().Frame(driver.FindElement(frameLocator));
            By dropTargetLocator = By.Id("droppable");
            By draggableLocator = By.Id("draggable");
            var dropTargetElement = driver.FindElement(dropTargetLocator);
            var draggableElement = driver.FindElement(draggableLocator);
            
            actions.DragAndDrop(draggableElement, dropTargetElement).Perform();
            Assert.AreEqual("Dropped!", dropTargetElement.Text);
        }
    }
}
