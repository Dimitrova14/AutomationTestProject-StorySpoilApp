using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
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
        };
        private readonly Dictionary<string, By> NoSpoilersSection = new()
        {
            { "Section", By.Id("scroll")},
            { "NoSpoilersYetMsg", By.CssSelector(".col-lg-6:nth-child(2)  > div > h2")},
            { "NoSpoilersYetDesc", By.CssSelector(".p-5 > p")},
            { "WriteSpoilerBtn", By.CssSelector(".col-lg-6:nth-child(2)  > div > a")},
        };
        private readonly Dictionary<string, By> AddedSpoilersSection = new()
        {
            { "Section", By.Id("scroll")},
            { "SpoilerCards", By.CssSelector(".col-lg-6:nth-child(2)")},
            { "TitleCard", By.CssSelector(".p-5 > h2")},
            { "DescriptionCard", By.CssSelector(".p-5 > p")},
            { "ImageCard", By.CssSelector(".img-fluid.rounded-circle")},
            { "EditBtn", By.CssSelector("div > a:nth-child(4)")},
            { "DeleteBtn", By.CssSelector("div > a:nth-child(5)")},
            { "ShareBtn", By.XPath("//a[text()='Share']")},
        };

        //copyright footer link
        private readonly By CopyrightFooterLink = By.CssSelector("a[href='#']");
        private readonly By Footer = By.CssSelector(".py-5.mt-lg-5");

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
            ScrollToElement(NoSpoilersSection["Section"]);

            foreach (var el in NoSpoilersSection)
            {
                if (!FindElement(el.Value).Displayed)
                {
                    return false;
                }
            }
            return true;
        }

        //check AddedSpoilersSection els displayed
        public int GetCountCards()
        {
            ScrollToElement(AddedSpoilersSection["Section"]);

            int countCards = FindElements(AddedSpoilersSection["SpoilerCards"]).Count;

            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();

            var titleLastCard= lastCard.FindElement(AddedSpoilersSection["TitleCard"]).Text;

            if (!titleLastCard.Contains("No Spoilers Yet!"))
            {
                return countCards;
            }
            else
            {
                return 0;
            }
        }

        public List<string> TitleAvailableCards()
        {
            List<string> titles = new List<string>();

            var foundTitles = FindElements(AddedSpoilersSection["TitleCard"]);

            foreach (var foundTitle in foundTitles) 
            {
                string title = foundTitle.Text;
                titles.Add(title);
            }

            return titles;
        }
        public bool IsTitleDisplayed()
        {
            ScrollToElement(AddedSpoilersSection["Section"]);

            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();
            return lastCard.FindElement(AddedSpoilersSection["TitleCard"]).Displayed;
        }
        public bool IsDescriptionDisplayed()
        {
            ScrollToElement(AddedSpoilersSection["Section"]);

            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();
            return lastCard.FindElement(AddedSpoilersSection["DescriptionCard"]).Displayed;
        }
        public bool IsImageDisplayed()
        {
            ScrollToElement(AddedSpoilersSection["Section"]);

            return FindElement(AddedSpoilersSection["ImageCard"]).Displayed;
        }
        public bool IsEditBtnDisplayed()
        {
            ScrollToElement(AddedSpoilersSection["Section"]);

            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();
            return lastCard.FindElement(AddedSpoilersSection["EditBtn"]).Displayed;
        }
        public bool IsDeleteBtnDisplayed()
        {
            ScrollToElement(AddedSpoilersSection["Section"]);

            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();
            return lastCard.FindElement(AddedSpoilersSection["DeleteBtn"]).Displayed;
        }

        public bool IsShareBtnDisplayed()
        {
            ScrollToElement(AddedSpoilersSection["Section"]);

            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();
            return lastCard.FindElement(AddedSpoilersSection["ShareBtn"]).Displayed;
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
        public void ClickWriteSpoilerBtn()
        {
            ScrollToElement(AddedSpoilersSection["Section"]);

            Click(NoSpoilersSection["WriteSpoilerBtn"]);
        }
        public void ScrollToLastCard(IWebElement lastCard)
        {
            lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();

            //scroll to element
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({ block: 'center'});", lastCard);

            Thread.Sleep(200);
        }
        public void ScrollToButtonOnCard(string nameButton)
        {
            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();
            var button = lastCard.FindElement(AddedSpoilersSection[nameButton]);
            
            //scroll to element
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({ block: 'center'});", button);

            Thread.Sleep(200);

        }
        public void ClickEditBtn()
        {
            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();

            ScrollToLastCard(lastCard);
            var editBtn = lastCard.FindElement(AddedSpoilersSection["EditBtn"]);

            ScrollToButtonOnCard("EditBtn");

            editBtn.Click();
        }
        public void ClickDeleteBtn()
        {
            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();

            ScrollToLastCard(lastCard);
            var deleteBtn = lastCard.FindElement(AddedSpoilersSection["DeleteBtn"]);

            ScrollToButtonOnCard("DeleteBtn");

            deleteBtn.Click();
        }
        public void ClickShareBtn()
        {
            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();

            ScrollToLastCard(lastCard);
            var shareBtn = lastCard.FindElement(AddedSpoilersSection["ShareBtn"]);

            ScrollToButtonOnCard("ShareBtn");

            shareBtn.Click();
        }
        public void ClickCopyrightLink()
        {
            ScrollToElement(Footer);

            Click(CopyrightFooterLink);
        }
        public void ClickSearchBtn()
        {
            Click(SearchBtn);
        }
        public bool IsTitleWrappedAndHidden()
        {
            ScrollToElement(AddedSpoilersSection["Section"]);

            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();
            var titleLastCard = lastCard.FindElement(AddedSpoilersSection["TitleCard"]);

            return IsTextWrappedAndHidden(titleLastCard);
        }
        public bool IsDescriptionWrappedAndHidden()
        {
            ScrollToElement(AddedSpoilersSection["Section"]);

            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();
            var descriptionLastCard = lastCard.FindElement(AddedSpoilersSection["DescriptionCard"]);

            return IsTextWrappedAndHidden(descriptionLastCard);
        }
        public void TypeInSearchField(string text)
        {
            ScrollToElement(SearchField);

            Type(SearchField, text);
        }


        //get texts on els
        public string GetWelcomeMsg()
        {
            return GetText(WelcomeSection["GreetingMesg"]);
        }
        public string GetNoSpoilersMsg()
        {
            ScrollToElement(AddedSpoilersSection["Section"]);

            return GetText(NoSpoilersSection["NoSpoilersYetMsg"]);
        }
        public string GetNoSpoilersDesc()
        {
            ScrollToElement(AddedSpoilersSection["Section"]);

            return GetText(NoSpoilersSection["NoSpoilersYetDesc"]);
        }
        public string GetTitleCard()
        {
            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();

            ScrollToElement(AddedSpoilersSection["Section"]);
            return lastCard.FindElement(AddedSpoilersSection["TitleCard"]).Text;
        }
        public string GetDescriptionCard()
        {
            var lastCard = FindElements(AddedSpoilersSection["SpoilerCards"]).Last();

            ScrollToElement(AddedSpoilersSection["Section"]);
            return lastCard.FindElement(AddedSpoilersSection["DescriptionCard"]).Text;
        }
    }
}
