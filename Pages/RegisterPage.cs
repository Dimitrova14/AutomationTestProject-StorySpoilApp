using OpenQA.Selenium;

namespace StorySpoilAppTests.Pages
{
    public class RegisterPage : BasePage
    {
        public RegisterPage(IWebDriver driver) : base(driver)
        {
        }

        private readonly string Url = BaseUrl + "/User/Register";

        //selectors
        private readonly By UsernameField = By.Id("username");
        private readonly By EmailField = By.Id("email");
        private readonly By FirstNameField = By.Id("firstName");
        private readonly By MiddleNameField = By.Id("midName");
        private readonly By LastNameField = By.Id("lastName");
        private readonly By PasswordField = By.Id("password");
        private readonly By ConfirmPasswordField = By.Id("rePassword");
        private readonly By SignUpBtn = By.CssSelector("button[type='submit']");
        private readonly By LoginHereLink = By.CssSelector("a[type='button']");

        //form title and main heading
        private readonly By RegisterAccountMsg = By.CssSelector("form[method='post'] > p");
        private readonly By SpoilStoryHeading = By.CssSelector("h4:nth-child(2)");

        //error messages
        private readonly Dictionary<string, By> RequredErrorMsgs = new()
        {
            {"UsernameField",By.CssSelector("span[data-valmsg-for='UserName']") },
            {"EmailField",By.CssSelector("span[data-valmsg-for='Email']") },
            {"LastNameField",By.CssSelector("span[data-valmsg-for='LastName']") },
            {"PasswordField",By.CssSelector("span[data-valmsg-for='Password']") },
            {"ConfirmPassField",By.CssSelector("span[data-valmsg-for='RePassword']") },
        };
        private readonly Dictionary<string, By> MinValueErrorMsgs = new()
        {
            {"UsernameField",By.CssSelector("span[data-valmsg-for='UserName']") },
            {"LastNameField",By.CssSelector("span[data-valmsg-for='LastName']") },
            {"PasswordField",By.CssSelector("span[data-valmsg-for='Password']") },
        };
        private readonly By NotMatchingPass_errorMsg = By.CssSelector("span[data-valmsg-for='RePassword']");

        //main methods
        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
        public bool IsPageDisplayed()
        {
            return driver.Url == Url && GetText(RegisterAccountMsg) == "Please register new account";
        }

        //interaction with elements
        public void TypeUsername(string username)
        {
            Type(UsernameField, username);
        }
        public void TypeEmail(string email)
        {
            Type(EmailField, email);
        }
        public void TypeFirstName(string firstName)
        {
            Type(FirstNameField, firstName);
        }
        public void TypeMiddleName(string middleName)
        {
            Type(MiddleNameField, middleName);
        }
        public void TypeLastName(string lastName)
        {
            Type(LastNameField, lastName);
        }
        public void TypePassword(string password)
        {
            Type(PasswordField, password);
        }
        public void TypeConfirmPass(string confirmPass)
        {
            Type(ConfirmPasswordField, confirmPass);
        }
        public void ClickRegsiterBtn()
        {
            Click(SignUpBtn);
        }
        public void ClickLoginHereLink()
        {
            Click(LoginHereLink);
        }

        //register
        public void RegisterUser_AllFields(string username, string email, string firstName, string middleName, string lastName, string password, string confirmPass)
        {
            TypeUsername(username);
            TypeEmail(email);
            TypeFirstName(firstName);
            TypeFirstName(middleName);
            TypeLastName(lastName);
            TypePassword(password);
            TypeConfirmPass(confirmPass);
            ClickRegsiterBtn();
        }

        //get text for els
        public string GetMainHeading()
        {
            return GetText(SpoilStoryHeading);
        }
        //error msg text
        public string GetErrorMsg_RequiredField(string fieldName)
        {
            if (fieldName == "username")
            {
                return GetText(RequredErrorMsgs["UsernameField"]);
            }
            else if (fieldName == "email")
            {
                return GetText(RequredErrorMsgs["EmailField"]);
            }
            else if (fieldName == "lastname")
            {
                return GetText(RequredErrorMsgs["LastNameField"]);
            }
            else if (fieldName == "password")
            {
                return GetText(RequredErrorMsgs["PasswordField"]);
            }
            else if (fieldName == "confirm password")
            {
                return GetText(RequredErrorMsgs["ConfirmPassField"]);
            } 
            else
            {
                return null;
            }

        }
        public string GetErrorMsg_MinValue(string fieldName)
        {
            if (fieldName == "username")
            {
                return GetText(MinValueErrorMsgs["UsernameField"]);
            }
            else if (fieldName == "lastname")
            {
                return GetText(MinValueErrorMsgs["LastNameField"]);
            }
            else if (fieldName == "password")
            {
                return GetText(MinValueErrorMsgs["PasswordField"]);
            }
            else
            {
                return null;
            }
        }
        public string GetErrorMsg_NotMatchingPass()
        {
            return GetText(NotMatchingPass_errorMsg);
        }

    }
}


