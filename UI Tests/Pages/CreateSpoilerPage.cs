using OpenQA.Selenium;

namespace StorySpoilAppTests.Pages
{
    public class CreateSpoilerPage : BasePage
    {
        public CreateSpoilerPage(IWebDriver driver) : base(driver)
        {
        }

        private readonly string Url = BaseUrl + "Story/Add";

        //selectors
        private readonly By CreateStoryHeading = By.CssSelector(".mt-1.mb-5.pb-1");
        private readonly Dictionary<string, By> CreateSpoilerForm = new()
        {
            { "TitleField", By.Id("title")},
            { "DescriptionField", By.Id("description")},
            { "UrlField", By.Id("url")},
            { "CreateBtn", By.CssSelector("button[type='submit']")},
        };

        //error msg 
        private readonly By MainErrorMsg = By.CssSelector(".text-info.validation-summary-errors > ul > li");

        //required fields 
        private readonly Dictionary<string, By> Requred_ErrorMsgs = new()
        {
            {"TitleField",By.CssSelector("span[data-valmsg-for='Title']") },
            {"DescriptionField",By.CssSelector("span[data-valmsg-for='Description']") },
        };

        //invalid fields error msgs 
        private readonly By InvalidUrl_ErrorMsg = By.CssSelector(".text-info.field-validation-error");

        //min value
        private readonly Dictionary<string, By> MinValue_ErrorMsgs = new()
        {
            {"TitleField",By.CssSelector("span[data-valmsg-for='Title']") },
            {"DescriptionField",By.CssSelector("span[data-valmsg-for='Description']") },
            {"UrlField",By.CssSelector("span[data-valmsg-for='Url']") },
        };
        //max value
        private readonly Dictionary<string, By> MaxValue_ErrorMsgs = new()
        {
            {"TitleField",By.CssSelector("span[data-valmsg-for='Title']") },
            {"DescriptionField",By.CssSelector("span[data-valmsg-for='Description']") },
        };

        //main methods
        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
        public bool IsPageDisplayed()
        {
            return driver.Url == Url && GetText(CreateStoryHeading).Equals("Create your story spoiler");
        }

        //check fields displayed 
        public bool IsCreateFormDisplayed()
        {
            foreach (var element in CreateSpoilerForm)
            {
                if (!FindElement(element.Value).Displayed)
                {
                    return false;
                }
            }
            return true;
        }

        //mainn error msg 
        public string GetMainErrorMsg()
        {
            return GetText(MainErrorMsg);
        }
        //required fields 
        public string GetErrorMsg_RequiredField(string fieldName)
        {
            if (fieldName == "title")
            {
                return GetText(Requred_ErrorMsgs["TitleField"]);
            }
            else if (fieldName == "description")
            {
                return GetText(Requred_ErrorMsgs["DescriptionField"]);
            }
            else
            {
                return null;
            }

        }

        //invalid url field message
        public string GetErrorMsg_InvalidUrlField()
        {
            return GetText(InvalidUrl_ErrorMsg);
        }

        //min value -> invalid fields 
        public string GetErrorMsg_MinValue(string fieldName)
        {
            if (fieldName == "title")
            {
                return GetText(MinValue_ErrorMsgs["TitleField"]);
            }
            else if (fieldName == "description")
            {
                return GetText(MinValue_ErrorMsgs["DescriptionField"]);
            }
            else
            {
                return null;
            }
        }
        //max value
        public string GetErrorMsg_MaxValue(string fieldName)
        {
            if (fieldName == "title")
            {
                return GetText(MaxValue_ErrorMsgs["TitleField"]);
            }
            else if (fieldName == "description")
            {
                return GetText(MaxValue_ErrorMsgs["DescriptionField"]);
            }
            else
            {
                return null;
            }
        }

        //interaction with elements 
        public void TypeTitle(string title)
        {
            Type(CreateSpoilerForm["TitleField"], title);
        }
        public void TypeDescription(string description)
        {
            Type(CreateSpoilerForm["DescriptionField"], description);
        }
        public void TypeUrl(string url)
        {
            Type(CreateSpoilerForm["UrlField"], url);
        }
        public void ClickCreateBtn()
        {
            Click(CreateSpoilerForm["CreateBtn"]);
        }

        //create spoiler
        public void CreateSpoiler_AllFields(string title, string description, string url)
        {
            TypeTitle(title);
            TypeDescription(description);
            TypeUrl(url);
            ClickCreateBtn();
        }
        public void CreateSpoiler_RequiredFields(string title, string description)
        {
            TypeTitle(title);
            TypeDescription(description);
            ClickCreateBtn();
        }

    }
}
