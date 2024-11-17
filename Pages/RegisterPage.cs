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
        private readonly By usernameField = By.Id("username");
        private readonly By emailField = By.Id("email");
        private readonly By firstNameField = By.Id("firstName");
        private readonly By middleNameField = By.Id("midName");
        private readonly By lastNameField = By.Id("lastName");
        private readonly By passwordField = By.Id("password");
        private readonly By confirmPasswordField = By.Id("rePassword");
        private readonly By signUpBtn = By.CssSelector("button[type='submit']");

        private readonly Dictionary<string, By> requredErrorMsgs = new()
        {
            {"UsernameField",By.CssSelector("span[data-valmsg-for='UserName']") },
            {"EmailField",By.CssSelector("span[data-valmsg-for='Email']") },
            {"LastNameField",By.CssSelector("span[data-valmsg-for='LastName']") },
            {"PasswordField",By.CssSelector("span[data-valmsg-for='Password']") },
            {"ConfirmPassField",By.CssSelector("span[data-valmsg-for='RePassword']") },
        };
        private readonly Dictionary<string, By> minValueErrorMsgs = new()
        {
            {"UsernameField",By.CssSelector("span[data-valmsg-for='UserName']") },
            {"LastNameField",By.CssSelector("span[data-valmsg-for='LastName']") },
            {"PasswordField",By.CssSelector("span[data-valmsg-for='Password']") },
        };
        private readonly By notMatchingPass_errorMsg = By.CssSelector("span[data-valmsg-for='RePassword']");

        public void TypeUsername(string username)
        {
            Type(usernameField, username);
        }
        public void TypeEmail(string email)
        {
            Type(emailField, email);
        }
        public void TypeFirstName(string firstName)
        {
            Type(firstNameField, firstName);
        }
        public void TypeLastName(string lastName)
        {
            Type(lastNameField, lastName);
        }
        public void TypePassword(string password)
        {
            Type(passwordField, password);
        }
        public void TypeConfirmPass(string confirmPass)
        {
            Type(confirmPasswordField, confirmPass);
        }
        public void ClickRegsiterBtn()
        {
            Click(loginBtn);
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
                return GetText(requredErrorMsgs["UsernameField"]);
            }
            else if (fieldName == "email")
            {
                return GetText(requredErrorMsgs["EmailField"]);
            }
            else if (fieldName == "lastname")
            {
                return GetText(requredErrorMsgs["LastNameField"]);
            }
            else if (fieldName == "password")
            {
                return GetText(requredErrorMsgs["PasswordField"]);
            }
            else if (fieldName == "confirm password")
            {
                return GetText(requredErrorMsgs["ConfirmPassField"]);
            } 
            else
            {
                return null;
            }

        }

        public string GetErrorMsgRequiredField(string fieldName)
        {
            if (fieldName == "username")
            {
                return GetText(requredErrorMsgs["UsernameField"]);
            }
            else if (fieldName == "email")
            {
                return GetText(requredErrorMsgs["EmailField"]);
            }
            else if (fieldName == "lastname")
            {
                return GetText(requredErrorMsgs["LastNameField"]);
            }
            else if (fieldName == "password")
            {
                return GetText(requredErrorMsgs["PasswordField"]);
            }
            else if (fieldName == "confirm password")
            {
                return GetText(requredErrorMsgs["ConfirmPassField"]);
            }
            else
            {
                return null;
            }

        }
        public string GetErrorMsg_NotMatchingPass()
        {
            return GetText(notMatchingPass_errorMsg);
        }





    }
}


