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
using OpenQA.Selenium.Support.UI;

namespace TestLibrary
{

    public class ClassWishListTest
    {
        readonly string password = "12345";
        readonly string login = "pristayko.pavlo@gmail.com";
        readonly string productName = "MacBook";
        readonly string LoginUrl = "http://atqc-shop.epizy.com/index.php?route=account/login";
        private IWebDriver driver;

        [OneTimeSetUp]
        public void BeforeAllMethods()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        }

        [OneTimeTearDown]
        public void AfterAllMethods()
        {
            driver.Close();
        }

        [TearDown]
        public void Logout()
        {
            WebDriverWait wait;
            IWebElement element;


            /*Clear Cart*/
            driver.FindElement(By.XPath("//button[contains(@class,'btn btn-inverse btn-block btn-lg dropdown-toggle')]")).Click();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            element = wait.Until(driver => driver.FindElement(By.XPath("//button[contains(@class,'btn btn-danger btn-xs')]")));
            element.Click();


            /*Logout*/
            driver.FindElement(By.XPath("//li[contains(@class,'dropdown')]//a[contains(@class,'dropdown-toggle')]")).Click();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            element = wait.Until(driver => driver.FindElement(By.XPath("//ul[contains(@class,'dropdown-menu dropdown-menu-right')]//a[contains(text(),'Logout')]")));
            element.Click();

        }

        [Test]
        public void AddItemToCart()
        {
            Login(login, password);

            driver.FindElement(By.Id("logo")).Click();//go to main page

            /*Add new item to wishlist*/
            driver.FindElement(By.XPath(String.Format("//div[contains(@class, 'product-layout')]//a[contains(text(),'{0}')]/../../following-sibling::div/button[contains(@data-original-title,'Wish')]", productName))).Click();
            
            /*Add item from wishlist to cart*/
            driver.FindElement(By.Id("wishlist-total")).Click();
            driver.FindElement(By.XPath("//td[contains(@class,'text-right')]//button[contains(@class,'btn btn-primary')]")).Click();

            WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driverWait.Until(driver=>driver.FindElement(By.Id("cart-total")).Text.Contains("$500.00"));

            string actual = driver.FindElement(By.Id("cart-total")).Text;
            string expected = driver.FindElement(By.XPath("//td[contains(@class,'text-right')]//div[contains(@class,'price')]")).Text;

            string[] price = actual.Split('-');
            actual = price[1].Replace(" ", string.Empty);
            Console.WriteLine(actual);

            Assert.AreEqual(expected,actual);
        }

        private void Login(string log, string pass)
        {

            driver.Navigate().GoToUrl(LoginUrl);

            driver.FindElement(By.Id("input-email")).SendKeys(log);
            driver.FindElement(By.Id("input-password")).SendKeys(pass);
            driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();



        }
    }
}
