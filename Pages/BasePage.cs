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
        protected static readonly string BaseUrl = "https://d24hkho2ozf732.cloudfront.net";

        //ctor
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        //NAVBAR BTNS
        public readonly By SignUpBtn = By.XPath("//a[@href='/User/Register']");
        public readonly By LoginBtn = By.XPath("//a[@href='/User/Login']");

        protected IWebElement FindElement(By by)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(by));
        }
        protected ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
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
