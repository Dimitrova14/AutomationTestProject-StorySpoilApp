using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

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
            if (driver.Url != Url)
            {
                return false;
            }
            else
            {
                return GetText(HeaderSection["Subheading"]) == "Share your spoilers responsibly and kindle the love for storytelling.";
            }
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
        //First Section
        private readonly Dictionary<string, By> SummarizeSection = new()
        {
            { "Section", By.CssSelector("section:nth-child(3)")},
            { "Heading", By.CssSelector("#scroll div.p-5 > h2")},
            { "Description", By.CssSelector("section:nth-child(3) div.p-5 > p")},
            { "Image", By.CssSelector("section:nth-child(3) div.p-5 > img")},
        };
        //Second Section
        private readonly Dictionary<string, By> UploadSection = new()
        {
            { "Section", By.CssSelector("section:nth-child(4)")},
            { "Heading", By.CssSelector("section:nth-child(4) div.p-5 > h2")},
            { "Description", By.CssSelector("section:nth-child(4) div.p-5 > p")},
            { "Image", By.CssSelector("section:nth-child(4) div.p-5 > img")},
        };
        //Third Section
        private readonly Dictionary<string, By> ReadyToSpoilSection = new()
        {
           { "Section", By.CssSelector("section:nth-child(5)")},
           { "Heading", By.CssSelector("section:nth-child(5) div.p-5 > h2")},
           { "Description", By.CssSelector("section:nth-child(5) div.p-5 > p")},
           { "Image", By.CssSelector("section:nth-child(5) div.p-5 > img")},
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

        //check promote app sections -> els displayed
        public bool CheckSummarizeSection_AllElsDisplayed()
        {
            ScrollToElement(SummarizeSection["Section"]);

            foreach (var element in SummarizeSection)
            {
                if (!FindElement(element.Value).Displayed)
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckUploadSection_AllElsDisplayed()
        {
            ScrollToElement(UploadSection["Section"]);

            foreach (var element in UploadSection)
            {
                if (!FindElement(element.Value).Displayed)
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckReadyToSpoilSection_AllElsDisplayed()
        {
            ScrollToElement(ReadyToSpoilSection["Section"]);

            foreach (var element in ReadyToSpoilSection)
            {
                if (!FindElement(element.Value).Displayed)
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
        public async Task<bool> IsImageUrlValid()
        {
            ScrollToElement(UploadSection["Section"]);

            // Create a new HttpClient instance
            HttpClient client = new HttpClient();
            string imageUrl = FindElement(UploadSection["Image"]).GetDomProperty("src");

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
        protected override void ScrollToElement(By by)
        {
            var element = wait.Until(ExpectedConditions.ElementIsVisible(by));

            //scroll to element
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({ block: 'center'});", element);

            Thread.Sleep(200);
        }
        public void ClickCopyrightLink()
        {
            ScrollToElement(CopyrightFooterLink);
            Click(CopyrightFooterLink);
        }

        //get text on els
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
        public string GetHeading_PromoteAppSection(string sectionName) 
        {
            if (sectionName == "summarise")
            {
                return GetText(SummarizeSection["Heading"]);
            }
            else if (sectionName == "upload")
            {
                return GetText(UploadSection["Heading"]);
            }
            else if (sectionName == "ready")
            {
                return GetText(ReadyToSpoilSection["Heading"]);
            }
            else
            {
                return null;
            }
        }
        public string GetDescription_PromoteAppSection(string sectionName)
        {
            if (sectionName == "summarise")
            {
                return GetText(SummarizeSection["Description"]);
            }
            else if (sectionName == "upload")
            {
                return GetText(UploadSection["Description"]);
            }
            else if (sectionName == "ready")
            {
                return GetText(ReadyToSpoilSection["Description"]);
            }
            else
            {
                return null;
            }
        }

    }
}
