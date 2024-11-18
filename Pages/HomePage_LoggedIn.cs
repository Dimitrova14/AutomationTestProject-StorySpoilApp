using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace StorySpoilAppTests.Pages
{
    public class HomePage_LoggedIn : BasePage
    {
        public HomePage_LoggedIn(IWebDriver driver) : base(driver)
        {
        }

        private readonly string Url = BaseUrl;

        //elements
        private readonly By UserProfileLink = By.XPath("//a[@href='/Profile']");
        private readonly By GreetingMessg = By.XPath("");
        private readonly By SearchField = By.XPath("//input[@type='search']");
        private readonly By SearchBtn = By.XPath("//input[@type='search']");
        private readonly By SpoilerCards = By.CssSelector(".col-lg-6:nth-child(2)");
        private readonly By TitleLastCard = By.CssSelector(".col-lg-6:nth-child(2)");
        private readonly By spoilerCards = By.CssSelector(".col-lg-6:nth-child(2)");
        //private readonly By spoilerCards = By.CssSelector(".col-lg-6:nth-child(2)");
        //private readonly By spoilerCards = By.CssSelector(".col-lg-6:nth-child(2)");


    }
}
