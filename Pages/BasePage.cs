using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace StorySpoilAppTests.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected static readonly string BaseUrl = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:83";

        //ctor
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        //NAVBAR BTNS
        public readonly By SignUpBtn = By.XPath("//a[@href='/User/Register']");
        public readonly By LoginBtn = By.XPath("//a[@href='/User/Login']");
        public readonly By LogoutBtn = By.XPath("//a[@href='/User/Logout']");
        public readonly By CreateSpoilerBtn = By.XPath("//a[@href='/Story/Add']");

        protected IWebElement FindElement(By by)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(by));
        }
        protected ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return driver.FindElements(by);
        }
        protected void Click(By by)
        {
            FindElement(by).Click();
        }
        protected void Type(By by, string text)
        {
            var fieldElement = FindElement(by);
            fieldElement.Clear();
            fieldElement.SendKeys(text);
        }
        protected string GetText(By by)
        {
            return FindElement(by).Text;
        }
    }
}
