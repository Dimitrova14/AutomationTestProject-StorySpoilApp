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
        private readonly By NavBar = By.XPath("//header//nav//div[@class='container px-5']");
        private readonly Dictionary<string, By> NavBarLinks = new()
        {
            { "UserProfileLink", By.XPath("//a[@href='/Profile']")},
            { "StorySpoilLink", By.XPath(".navbar-brand")},
            { "LogoutBtn", By.XPath("//a[@href='/User/Logout']")},
            { "CreateSpoilerBtn", By.XPath("//a[@href='/Story/Add']")},
        };
        private readonly Dictionary<string, By> WelcomeSection = new()
        {
            { "StorySpoilerHeading", By.CssSelector(".masthead-heading.mb-0")},
            { "GreetingMesg", By.XPath(".masthead-subheading.mb-0")},
            { "SearchField", By.XPath("//input[@type='search']")},
            { "SearchBtn", By.CssSelector(".input-group > a")},
        };
        private readonly Dictionary<string, By> NoSpoilersSection = new()
        {
            { "NoSpoilersYetMsg", By.CssSelector(".col-lg-6:nth-child(2)  > div > h2")},
            { "WriteSpoilerBtn", By.XPath(".col-lg-6:nth-child(2)  > div > a")},
        };
        private readonly Dictionary<string, By> AddedSpoilersSection = new()
        {
            { "SpoilerCards", By.CssSelector(".col-lg-6:nth-child(2)")},
            { "TitleLastCard", By.XPath("div > h2")},
            { "DescriptionLastCard", By.XPath("div > p")},
            { "EditBtnLastCard", By.XPath("div > a:nth-child(4)")},
            { "DeleteBtnLastCard", By.XPath("div > a:nth-child(5)")},
        };

        //methods
        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
        //check navbarlinks
        public bool AreNavBarDisplayed()
        {
            return FindElement(NavBar).Displayed;
        }
        //public bool AreNavBarLinksDisplayed()
        //{
        //    foreach (var link in NavBarLinks)
        //    {
        //        return FindElement(link.Value).Displayed;
        //    }
            
        //}

        //check welcomesection
        //check NoSpoilersSection
        //check AddedSpoilersSection

        public bool AreSpoilerCardsDisplayed()
        {
            return FindElements(AddedSpoilersSection["SpoilerCards"]).Count != 0;
        }
        public bool IsTitleDisplayed()
        {
            return FindElement(AddedSpoilersSection["TitleLastCard"]).Displayed;
        }
        public bool IsDescriptionDisplayed()
        {
            return FindElement(AddedSpoilersSection["DescriptionLastCard"]).Displayed;
        }
        public bool IsEditBtnDisplayed()
        {
            return FindElement(AddedSpoilersSection["EditBtnLastCard"]).Displayed;
        }
        public bool IsDeleteBtnDisplayed()
        {
            return FindElement(AddedSpoilersSection["DeleteBtnLastCard"]).Displayed;
        }
        //check title
        //check description
        //check edit & delete buttons

    }
}
