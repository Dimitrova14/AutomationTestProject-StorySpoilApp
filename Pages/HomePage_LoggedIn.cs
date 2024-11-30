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
            { "StorySpoilLink", By.CssSelector(".navbar-brand")},
            { "LogoutBtn", By.XPath("//a[@href='/User/Logout']")},
            { "CreateSpoilerBtn", By.XPath("//a[@href='/Story/Add']")},
        };
        private readonly By SearchField = By.CssSelector("input[type='search']");
        private readonly By SearchBtn = By.CssSelector(".input-group > a");

        //sections
        private readonly Dictionary<string, By> WelcomeSection = new()
        {
            { "StorySpoilerHeading", By.CssSelector(".masthead-heading.mb-0")},
            { "GreetingMesg", By.CssSelector(".masthead-subheading.mb-0")},
            { "SearchField", By.XPath("//input[@type='search']")},
            { "SearchBtn", By.CssSelector(".input-group > a")},
        };
        private readonly Dictionary<string, By> NoSpoilersSection = new()
        {
            { "NoSpoilersYetMsg", By.CssSelector(".col-lg-6:nth-child(2)  > div > h2")},
            { "WriteSpoilerBtn", By.CssSelector(".col-lg-6:nth-child(2)  > div > a")},
        };
        private readonly Dictionary<string, By> AddedSpoilersSection = new()
        {
            { "SpoilerCards", By.CssSelector(".col-lg-6:nth-child(2)")},
            { "TitleCard", By.CssSelector("div > h2")},
            { "DescriptionCard", By.CssSelector("div > p")},
            { "EditBtn", By.CssSelector("div > a:nth-child(4)")},
            { "DeleteBtn", By.CssSelector("div > a:nth-child(5)")},
        };

        //copyright footer link
        private readonly By CopyrightFooterLink = By.CssSelector("a[href='#']");

        //main methods
        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
        public bool IsPageDisplayed()
        {
            return driver.Url == Url && GetText(WelcomeSection["GreetingMesg"]).Contains("Welcome, ");
        }

        //sections & els displayed
        public bool IsNavBarDisplayed()
        {
            return FindElement(NavBar).Displayed;
        }
        public bool AreNavBarLinksDisplayed()
        {
            foreach (var link in NavBarLinks)
            {
                if (!FindElement(link.Value).Displayed)
                {
                    return false;
                }
            }
            return true;
        }

        //check search functionality displayed 
        public bool IsSearchFieldDisplayed()
        {
            return FindElement(SearchField).Displayed;
        }
        public bool IsSearchBtnDisplayed()
        {
            return FindElement(SearchBtn).Displayed;
        }

        //check sections displayed
        public bool CheckWelcomeSection_AllElsDisplayed()
        {
            foreach (var link in WelcomeSection)
            {
                if (!FindElement(link.Value).Displayed)
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckNoSpoilersSection_AllElsDisplayed()
        {
            foreach (var el in NoSpoilersSection)
            {
                if (!FindElement(el.Value).Displayed)
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckAddedSpoilersSection_AllElsDisplayed()
        {
            foreach (var el in AddedSpoilersSection)
            {
                if (FindElements(el.Value).Count == 0)
                {
                    return false;
                }
            }
            return true;
        }

        //check AddedSpoilersSection els displayed
        public bool AreSpoilerCardsDisplayed()
        {
            return FindElements(AddedSpoilersSection["SpoilerCards"]).Count != 0;
        }
        public bool IsTitleDisplayed()
        {
            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();
            return lastCard.FindElement(AddedSpoilersSection["TitleCard"]).Displayed;
        }
        public bool IsDescriptionDisplayed()
        {
            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();
            return lastCard.FindElement(AddedSpoilersSection["DescriptionLastCard"]).Displayed;
        }
        public bool IsEditBtnDisplayed()
        {
            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();
            return lastCard.FindElement(AddedSpoilersSection["EditBtn"]).Displayed;
        }
        public bool IsDeleteBtnDisplayed()
        {
            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();
            return lastCard.FindElement(AddedSpoilersSection["DeleteBtnLastCard"]).Displayed;
        }


        //interaction with elements
        public void ClickUserProfileLink()
        {
            Click(NavBarLinks["UserProfileLink"]);
        }
        public void ClickStorySpoilLink()
        {
            Click(NavBarLinks["StorySpoilLink"]);
        }
        public void ClickCreateSpoilerLink()
        { 
            Click(NavBarLinks["CreateSpoilerBtn"]);
        }
        public void ClickLogoutLink()
        {
            Click(NavBarLinks["LogoutBtn"]);
        }
        public void ClickEditBtn()
        {
            //FindElement(by).Click();
            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();
            lastCard.FindElement(AddedSpoilersSection["EditBtn"]).Click();
        }
        public void ClickDeleteBtn()
        {
            //FindElement(by).Click();
            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();
            lastCard.FindElement(AddedSpoilersSection["DeleteBtn"]).Click();
        }
        public void ClickCopyrightLink()
        {
            Click(CopyrightFooterLink);
        }

        //get texts on els
        public string GetWelcomeMsg()
        {
            return GetText(WelcomeSection["GreetingMesg"]);
        }
        public string GetTitleCard()
        {
            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();
            return lastCard.FindElement(AddedSpoilersSection["TitleCard"]).Text;
        }
        public string GetDescriptionCard()
        {
            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();
            return lastCard.FindElement(AddedSpoilersSection["DescriptionCard"]).Text;
        }

    }
}
