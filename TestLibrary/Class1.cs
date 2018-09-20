using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestLibrary
{
    [TestFixture]
    public class ClassWishListTest
    {
        string password = "12345";
        string login = "pristayko.pavlo@gmail.com";

        private IWebDriver driver;

        [OneTimeSetUp]
        public void BeforeAllMethods()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        }

        [OneTimeTearDown]
        public void AfterAllMethods()
        {
            driver.Close();
        }

        [Test]
        public void AddItemToCart()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("http://atqc-shop.epizy.com/index.php?route=account/login");

            driver.FindElement(By.Id("input-email")).SendKeys(login);
            driver.FindElement(By.Id("input-password")).SendKeys(password);


            driver.FindElement(By.ClassName("dropdown-toggle")).FindElement(By.XPath("//div[text() = 'TextToFind']")) .Click();
            Thread.Sleep(2000);
        }
    }
}
