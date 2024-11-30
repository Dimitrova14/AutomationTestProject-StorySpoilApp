using OpenQA.Selenium;

namespace StorySpoilAppTests.Pages
{
    public class HomePage_NotLoggedIn : BasePage
    {
        public HomePage_NotLoggedIn(IWebDriver driver) : base(driver)
        {
        }
        private readonly string Url = BaseUrl;

        //main methods
        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
        public bool IsPageDisplayed()
        {
            return driver.Url == Url && GetText(HeaderSection["Subheading"]) == "Share your spoilers responsibly and kindle the love for storytelling.";
        }

        //selectors
        private readonly By NavBar = By.XPath("//header//nav//div[@class='container px-5']");
        private readonly Dictionary<string, By> NavBarLinks = new()
        {
            { "HomePageLink", By.CssSelector(".navbar-brand")},
            { "SignUpLink", By.CssSelector("a[href='/User/Register']")},
            { "LogInLink", By.CssSelector("a[href='/User/Login']")},
        };
        private readonly Dictionary<string, By> HeaderSection = new()
        {
            { "StorySpoilerHeading", By.CssSelector(".masthead-heading.mb-0")},
            { "Subheading", By.CssSelector(".masthead-subheading.mb-0")},
        };

        //sections promoting the app -> els
        private readonly Dictionary<string, By> Headings_PromoteAppSection = new()
        {
            { "SummariseStory", By.CssSelector("#scroll div.p-5 > h2")},
            { "UploadPicture", By.CssSelector("section:nth-child(4) div.p-5 > h2")},//THE SAME
            { "ReadyToSpoilStory", By.CssSelector("section:nth-child(5) div.p-5 > h2")},//last()
        };
        private readonly Dictionary<string, By> Descriptions_PromoteAppSection = new()
        {
            { "SummariseStory_Desc", By.CssSelector("section:nth-child(3) div.p-5 > p")},
            { "UploadPicture_Desc", By.CssSelector("section:nth-child(4) div.p-5 > p")},
            { "ReadyToSpoilStory_Desc", By.CssSelector("section:nth-child(5) div.p-5 > p")},
        };
        private readonly Dictionary<string, By> Images_PromoteAppSection = new()
        {
            { "SummariseStory_Img", By.CssSelector("section:nth-child(3) div.p-5 > img")},
            { "UploadPicture_Img", By.CssSelector("section:nth-child(4) div.p-5 > img")},
            { "ReadyToSpoilStory_Img", By.CssSelector("section:nth-child(5) div.p-5 > img")},//last()
        };

        //copyright footer link
        private readonly By CopyrightFooterLink = By.CssSelector("a[href='#']");

        //els displayed
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
        public bool CheckHeaderSection_AllElsDisplayed()
        {
            foreach (var el in HeaderSection)
            {
                if (!FindElement(el.Value).Displayed)
                {
                    return false;
                }
            }
            return true;
        }

        //check promote app section -> els displayed
        public bool CheckPromoteAppSection_HeadingsDisplayed()
        {
            foreach (var heading in Headings_PromoteAppSection)
            {
                if (!FindElement(heading.Value).Displayed)
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckPromoteAppSection_DescriptionsDisplayed()
        {
            foreach (var description in Descriptions_PromoteAppSection)
            {
                if (!FindElement(description.Value).Displayed)
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckPromoteAppSection_ImagesDisplayed()
        {
            foreach (var image in Images_PromoteAppSection)
            {
                if (!FindElement(image.Value).Displayed)
                {
                    return false;
                }
            }
            return true;
        }

        //check copyright link displayed
        public bool IsCopyrightLinkDisplayed()
        {
            return FindElement(CopyrightFooterLink).Displayed;
        }

        //check if img url is accesible 
        public async Task<bool> IsImageUrlValid(string imageUrl)
        {
            // Create a new HttpClient instance
            HttpClient client = new HttpClient();

            try
            {
                // Send the HTTP request asynchronously
                HttpResponseMessage response = await client.GetAsync(imageUrl);
                // Return true if the response status code indicates success (e.g., HTTP 200)
                return response.IsSuccessStatusCode;
            }
            finally
            {
                // Dispose of the HttpClient to release resources
                client.Dispose();
            }
        }


        //interaction with elements
        public void ClickHomePageLink()
        {
            Click(NavBarLinks["HomePageLink"]);
        }
        public void ClickSignUpLink()
        {
            Click(NavBarLinks["SignUpLink"]);
        }
        public void ClickLogInLink()
        {
            Click(NavBarLinks["LogInLink"]);
        }
        public void ClickCopyrightLink()
        {
            Click(CopyrightFooterLink);
        }

        //get text on els
        //pass -> "heading" || "subheading"
        public string GetHeading_HeaderSection(string typeHeading)
        {
            if (typeHeading == "heading")
            {
                return GetText(HeaderSection["StorySpoilerHeading"]);
            }
            else if (typeHeading == "subheading")
            {
                return GetText(HeaderSection["Subheading"]);
            }
            else
            {
                return null;
            }
        }
        public string GetHeading_PromoteAppSection(string firstWordfHeading)
        {
            if (firstWordfHeading == "summarise")
            {
                return GetText(Headings_PromoteAppSection["SummariseStory"]);
            }
            else if (firstWordfHeading == "upload")
            {
                return GetText(Headings_PromoteAppSection["UploadPicture"]);
            }
            else if (firstWordfHeading == "ready")
            {
                return GetText(Headings_PromoteAppSection["ReadyToSpoilStory"]);
            }
            else
            {
                return null;
            }
        }
        public string GetDescription_PromoteAppSection(string firstWordfHeading)
        {
            if (firstWordfHeading == "summarise")
            {
                return GetText(Descriptions_PromoteAppSection["SummariseStory_Desc"]);
            }
            else if (firstWordfHeading == "upload")
            {
                return GetText(Descriptions_PromoteAppSection["UploadPicture_Desc"]);
            }
            else if (firstWordfHeading == "ready")
            {
                return GetText(Descriptions_PromoteAppSection["ReadyToSpoilStory_Desc"]);
            }
            else
            {
                return null;
            }
        }

    }
}
