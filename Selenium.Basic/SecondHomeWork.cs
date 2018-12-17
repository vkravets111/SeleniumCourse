using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium.Basic
{
    
    public class SecondHomeWork
    {
        IWebDriver driver = null;
        // using (IWebDriver driver = private new FirefoxDriver())
       // FirstTask(driver);
       [Test]
        public void FirstTask(IWebDriver driver)
        {
            /*   Get elements from https://atata-framework.github.io/atata-sample-app/#!/products
             *   under column Amount that have number 5 in their text using XPath 
             *   and CSS selectors   */
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://atata-framework.github.io/atata-sample-app/#!/products");
            //List<IWebElement> spansWithClass = driver.FindElements(By.XPath("//span[@class and position() > 3] ")).ToList();


        }
    }
}
