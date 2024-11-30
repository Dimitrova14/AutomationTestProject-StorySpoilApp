using OpenQA.Selenium;

namespace StorySpoilAppTests.Pages
{
    public class EditSpoilerPage : BasePage
    {
        public EditSpoilerPage(IWebDriver driver) : base(driver)
        {
        }

        private readonly string Url = BaseUrl + "/Story/Edit";

        //selectors
        private readonly By EditStoryHeading = By.CssSelector(".mt-1.mb-5.pb-1");


        //main methods
        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
        public bool IsPageDisplayed()
        {
            return driver.Url.Contains(Url + "?storyId=") && GetText(EditStoryHeading).Equals("Edit your story spoiler");
        }

    }
}
