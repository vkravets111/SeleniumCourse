using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Selenium.Basic
{
    public static class SecondHomeWork2
    {
        public static void Main(string[] args)
        {
            IWebDriver driver = null;

            FirstTask(driver);
            SecondTask(driver);
            ThirdTask(driver);
            ForthTask(driver);
            FifthTask(driver);
        }
        public static void FirstTask(IWebDriver driver)
        {
            /*   Get elements from https://atata-framework.github.io/atata-sample-app/#!/products
             *   under column Amount that have number 5 in their text using XPath 
             *   and CSS selectors   */
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://atata-framework.github.io/atata-sample-app/#!/products");
            List<IWebElement> findByxPath = driver.FindElements(By.XPath("//tbody//td[3][contains(text(), '5')]")).ToList();


            //returns null. have no idea how to fix
              List<IWebElement> findByCss = driver.FindElements(By.CssSelector("tr>td")).Where(x => x.Text.Contains("5")).ToList();

            driver.Quit();

        }

    public static void SecondTask(IWebDriver driver)
        {
            /*   Get from https://atata-framework.github.io/atata-sample-app/#!/plans
             *   numbers of projects from payment plans using XPath and CSS selectors    */

            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://atata-framework.github.io/atata-sample-app/#!/plans");

            List<IWebElement> findByxPath = driver.FindElements(By.XPath("//p[contains(text(), 'Number of projects:')]/b")).ToList();


            List<IWebElement> findByCss = driver.FindElements(By.CssSelector("p>b")).ToList();

            driver.Quit();
        }

        public static void ThirdTask(IWebDriver driver)
        {
            /*   get timer element from http://www.seleniumframework.com/Practiceform/ 
             *   when there is 35 seconds remaining Explicitly 
             *   and when there is 30 seconds remaining Implicitly   */


            //explicit wait
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://www.seleniumframework.com/Practiceform/");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            IWebElement timer = wait.Until(d => d.FindElement(By.XPath("//span[@class='timer']")));

            timer = wait.Until((d) =>
            {
                if (d.FindElement(By.XPath("//span[@class='timer']")).Text == "Seconds remaining: 35")
                    return d.FindElement(By.XPath("//span[@class='timer']"));
                else
                    return null;
            });

            //impicit wait
            driver.Navigate().GoToUrl("http://www.seleniumframework.com/Practiceform/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            IWebElement element = driver.FindElement(By.XPath("//span[@class='timer'][contains(text(), 'Seconds remaining: 30')]"));

            driver.Quit();
        }

        public static void ForthTask(IWebDriver driver)
        {
            /*   From https://tern.gp.gov.ua/ua/search.html
             *   select "За зменьшенням" from search dropdown and 
             *   select "Сортувати по: Даті" RadioButton    */

            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://tern.gp.gov.ua/ua/search.html");
            IWebElement dropdown = driver.FindElement(By.Name("order"));
            SelectElement selectElement = new SelectElement(dropdown);
            selectElement.SelectByText("За зменьшенням");

            IWebElement date = driver.FindElement(By.XPath("//input[@value='date']"));
            bool value = date.Selected;
            if(!value)
            {
                date.Click();
            }
            driver.Quit();

        }

        public static void FifthTask(IWebDriver driver)
        {
            /*  From http://www.seleniumframework.com/Practiceform/
             *  fill in and submit "Subscribe" form     */
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://www.seleniumframework.com/Practiceform/");

            driver.FindElement(By.Name("name")).SendKeys("Tratata");
            driver.FindElement(By.Name("email")).SendKeys("tratata@ololo.com");

            driver.FindElement(By.Name("telephone")).SendKeys("969696969696969");
            driver.FindElement(By.Name("country")).SendKeys("atatataatat");
            driver.FindElement(By.Name("company")).SendKeys("atatataatat");
            driver.FindElement(By.Name("company")).SendKeys("please kill me");
            driver.FindElement(By.Name("company")).Submit();

            driver.Quit();
        }


    }
}
