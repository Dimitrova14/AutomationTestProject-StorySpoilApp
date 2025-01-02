using OpenQA.Selenium;

namespace StorySpoilAppTests.Pages
{
    public class MyProfilePage : BasePage
    {
        public MyProfilePage(IWebDriver driver) : base(driver)
        {
        }
        private readonly string Url = BaseUrl + "Profile";

        //selectors
        //sections
        private readonly By UserProfileSection = By.CssSelector(".card-body.text-center");
        private readonly By AboutMeSection = By.CssSelector(".card.mb-4.mb-md-0 > div");
        private readonly By UserAttributesSection = By.XPath("//div[@class='card mb-4']/div[@class='card-body']");

        //user profile section els
        private readonly Dictionary<string, By> UserProfileEls = new()
        {
            {"ProfilePicture",By.CssSelector(".rounded-circle.img-fluid")},
            {"Username",By.CssSelector(".card-body.text-center > h5") },
            {"EditBtn",By.CssSelector(".btn.btn-primary") },
        };

        //labels
        private readonly Dictionary<string, By> UserAttributesLabels = new()
        {
            {"FullName",By.XPath("//div[@class='col-sm-3']//p[text()='Full Name']") },
            {"Email",By.XPath("//div[@class='col-sm-3']//p[text()='Email']") },
            {"TotalSpoilers",By.XPath("//div[@class='col-sm-3']//p[text()='Total spoilers']") },
        };
        private readonly By AboutMeLabel = By.XPath("//p[contains(text(), 'About me:')]");

        //fields with text
        private readonly Dictionary<string, By> UserAttributesFields = new()
        {
            {"FullNameField",By.CssSelector(".row:first-child > .col-sm-9 > p") },
            {"EmailField",By.CssSelector(".row:nth-child(3) > div:nth-child(2) > p") },
            {"TotalSpoilers",By.CssSelector(".row:nth-child(5) > div:nth-child(2) > p") },
        };
        private readonly By AboutMeText = By.CssSelector(".card.mb-4.mb-md-0 > div > p:nth-child(2)");


        //main methods
        public bool IsPageDisplayed()
        {
            return driver.Url == Url && GetText(AboutMeText) == "You haven't written anything about yourself...";
        }
        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }

        //interaction with elements
        public void ClickEditBtn()
        {
            Click(UserProfileEls["EditBtn"]);
        }

        //check sections displayed
        public bool IsUserProfileSectionDisplayed()
        {
            return FindElement(UserProfileSection).Displayed;
        }
        public bool IsAboutMeSectionDisplayed()
        {
            return FindElement(AboutMeSection).Displayed;
        }
        public bool IsUserAttributesSectionDisplayed()
        {
            return FindElement(UserAttributesSection).Displayed;
        }

        //check sections els displayed 
        public bool CheckUserProfileSection_AllElsDisplayed()
        {
            var userProfileSection = FindElement(UserProfileSection);

            foreach (var el in UserProfileEls)
            {
                var element = userProfileSection.FindElement(el.Value);

                if (!element.Displayed)
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckUserAttributesSection_LabelsDisplayed()
        {
            foreach (var label in UserAttributesLabels)
            {
                var element = FindElement(label.Value);
                if (!element.Displayed)
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckUserAttributesSection_AllFieldsDisplayed()
        {
            foreach (var field in UserAttributesFields)
            {
                if (!FindElement(field.Value).Displayed)
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckAboutMeSection_LabelDisplayed()
        {
            return FindElement(AboutMeLabel).Displayed;
        }
        public bool CheckAboutMeSection_ElsDisplayed()
        {
            return FindElement(AboutMeText).Displayed;
        }

        //get text for all sections -> all fields
        public string GetNameOfTheUser()
        {
            return GetText(UserProfileEls["Username"]);
        }
        public string GetFullName()
        {
            return GetText(UserAttributesFields["FullNameField"]);
        }
        public string GetEmail()
        {
            return GetText(UserAttributesFields["EmailField"]);
        }
        public string GetTotalSpoilersCount()
        {
            return GetText(UserAttributesFields["TotalSpoilers"]);
        }
        public string GetAboutMeMessage()
        {
            return GetText(AboutMeText);
        }

    }
}
