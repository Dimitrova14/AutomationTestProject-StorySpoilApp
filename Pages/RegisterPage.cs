using OpenQA.Selenium;

namespace StorySpoilAppTests.Pages
{
    public class RegisterPage : BasePage
    {
        public RegisterPage(IWebDriver driver) : base(driver)
        {
        }

        private readonly string Url = BaseUrl + "/User/Register";
        //elements -> private
        private readonly By UsernameField = By.Id("username");
        private readonly By EmailField = By.Id("email");
        private readonly By FirstNameField = By.Id("firstName");
        private readonly By MiddleNameField = By.Id("midName");
        private readonly By LastNameField = By.Id("lastName");
        private readonly By PasswordField = By.Id("password");
        private readonly By ConfirmPasswordField = By.Id("rePassword");
        private readonly By SignUpBtn = By.CssSelector("button[type='submit']");

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

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
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
            Click(LoginBtn);
        }

        public void RegisterUser(string username, string email, string firstName, string lastName, string password, string confirmPass)
        {
            TypeUsername(username);
            TypeEmail(email);
            TypeFirstName(firstName);
            TypeLastName(lastName);
            TypePassword(password);
            TypeConfirmPass(confirmPass);
            ClickRegsiterBtn();
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


