using OpenQA.Selenium;

namespace StorySpoilAppTests.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        private readonly string Url = BaseUrl + "/User/Login";

        //elements
        private readonly By usernameField = By.Id("username");
        private readonly By passwordField = By.Id("password");
        private readonly By loginBtn = By.CssSelector("button[type='submit']");

        //error msgs
        //methods
    }
}
