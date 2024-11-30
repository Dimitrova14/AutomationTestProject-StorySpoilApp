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
        private readonly By UsernameField = By.Id("username");
        private readonly By PasswordField = By.Id("password");
        private readonly By LoginBtn = By.CssSelector("button[type='submit']");
        private readonly By ForgotPasswordLink = By.CssSelector(".text-muted");
        private readonly By CreateNewLink = By.CssSelector(".btn.btn-outline-info");

        //form message and main heading
        private readonly By LoginToAccountMsg = By.CssSelector("form[method='post'] > p");
        private readonly By SpoilStoryHeading = By.CssSelector("h4:nth-child(2)");

        //main methods
        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
        public bool IsPageDisplayed()
        {
            return driver.Url == Url && GetText(LoginToAccountMsg) == "Please login to your account";
        }

        //error msgs
        private readonly By MainErrorMssg = By.CssSelector(".text-info.validation-summary-errors > ul > li");
        private readonly Dictionary<string, By> RequredErrorMsgs = new()
        {
            {"UsernameField",By.CssSelector("span[data-valmsg-for='Username']") },
            {"PasswordField",By.CssSelector("span[data-valmsg-for='Password']") },
        };

        //interaction with elements
        public void TypeUsername(string username)
        {
            Type(UsernameField, username);
        }
        public void TypePassword(string password)
        {
            Type(PasswordField, password);
        }
        public void ClickLoginBtn()
        {
            Click(LoginBtn);
        }
        public void ClickForgotPasswordLink()
        {
            Click(ForgotPasswordLink);
        }
        public void ClickCreateNewLink()
        {
            Click(CreateNewLink);
        }

        //login
        public void Login_AllFields(string username, string password)
        {
            TypeUsername(username);
            TypePassword(password);
            ClickLoginBtn();
        }

        //main heading
        public string GetMainHeading()
        {
            return GetText(SpoilStoryHeading);
        }

        //error messages text
        public string GetErrorMsg_RequiredField(string fieldName)
        {
            if (fieldName == "username")
            {
                return GetText(RequredErrorMsgs["UsernameField"]);
            }
            else if (fieldName == "password")
            {
                return GetText(RequredErrorMsgs["PasswordField"]);
            }
            else
            {
                return null;
            }
        }
        public string GetMainErrorMsg()
        {
            return GetText(MainErrorMssg);
        }


    }
}
