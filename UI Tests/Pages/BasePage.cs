using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace StorySpoilAppTests.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected static readonly string BaseUrl = "https://d24hkho2ozf732.cloudfront.net/";
        protected Actions action;
        protected Random random;


        //ctor
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        //NAVBAR BTNS
        public readonly By SignUpButton = By.XPath("//a[@href='/User/Register']");
        public readonly By LoginButton = By.XPath("//a[@href='/User/Login']");

        //main methods
        protected IWebElement FindElement(By by)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(by));

        }
        protected ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
            return driver.FindElements(by);
        }
        protected virtual void ScrollToElement(By by)
        {
            var element = wait.Until(ExpectedConditions.ElementIsVisible(by));

            //scroll to element
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({ block: 'center'});", element);

            Thread.Sleep(200);

        }
        protected bool IsTextWrappedAndHidden(IWebElement element)
        {
            string overflowValue = element.GetCssValue("overflow");
            string overflowWrapValue = element.GetCssValue("overflow-wrap");

            return overflowValue == "hidden" && overflowWrapValue == "break-word";
        }
        public bool IsClickable(By by)
        {
            try
            {
                // Wait for the element to be clickable
                wait.Until(ExpectedConditions.ElementToBeClickable(by));
                return true; // The element is clickable
            }
            catch (WebDriverTimeoutException)
            {
                // If timeout occurs, the element is not clickable
                return false;
            }
        }
        protected void Click(By by)
        {
            ScrollToElement(by);

            if (IsClickable(by))
            {
                FindElement(by).Click();
            }
            else
            {
                throw new Exception($"Element located by {by} is not clickable.");
            }
        }
        protected void Type(By by, string text)
        {
            var fieldElement = FindElement(by);
            fieldElement.Clear();
            fieldElement.SendKeys(text);
        }
        protected string GetText(By by)
        {
            ScrollToElement(by);

            return FindElement(by).Text;
        }
        protected string GetInputValue(By by)
        {
            var element = FindElement(by);
            return element.GetDomProperty("value");
        } 

    }
}
