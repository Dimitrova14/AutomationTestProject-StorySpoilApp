using OpenQA.Selenium;

namespace StorySpoilAppTests.Pages
{
    public class RegisterPage : BasePage
    {
        public RegisterPage(IWebDriver driver) : base(driver)
        {
        }

        private readonly string Url = BaseUrl + "User/Register";

        //register form
        private readonly Dictionary<string, By> RegisterForm = new()
        {
            {"UsernameField",  By.Id("username")},
            {"EmailField",  By.Id("email")},
            {"FirstNameField",  By.Id("firstName")},
            {"MiddleNameField",  By.Id("midName")},
            {"LastNameField",  By.Id("lastName")},
            {"PasswordField",  By.Id("password")},
            {"ConfirmPasswordField",  By.Id("rePassword")},
            {"ConfirmPassPlaceholder",  By.CssSelector("label[for='rePassword']")},

            {"SignUpBtn",  By.CssSelector("button[type='submit']")},
            {"LoginHereLink",  By.CssSelector("a[type='button']")},
        };

        //form title and main heading
        private readonly By RegisterAccountMsg = By.CssSelector("form[method='post'] > p");
        private readonly By SpoilStoryHeading = By.CssSelector("h4:nth-child(2)");

        //error messages
        private readonly Dictionary<string, By> RequredErrorMsgs = new()
        {
            {"UsernameField",By.CssSelector("span[data-valmsg-for='UserName']") },
            {"EmailField",By.CssSelector("span[data-valmsg-for='Email']") },
            {"FirstNameField",By.CssSelector("span[data-valmsg-for='FirstName']") },
            {"LastNameField",By.CssSelector("span[data-valmsg-for='LastName']") },
            {"PasswordField",By.CssSelector("span[data-valmsg-for='Password']") },
            {"ConfirmPassField",By.CssSelector("span[data-valmsg-for='RePassword']") },
        };
        private readonly Dictionary<string, By> MinValueErrorMsgs = new()
        {
            {"UsernameField",By.CssSelector("span[data-valmsg-for='UserName']") },
            {"EmailField",By.CssSelector("span[data-valmsg-for='Email']") },
            {"FirstNameField",By.CssSelector("span[data-valmsg-for='FirstName']") },
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

        //check fields displayed 
        public bool IsRegisterFormDisplayed()
        {
            foreach (var element in RegisterForm)
            {
                if (!FindElement(element.Value).Displayed)
                {
                    return false;
                }
            }
            return true;
        }

        //interaction with elements
        public void TypeUsername(string username)
        {
            var usernameField = RegisterForm["UsernameField"];
            Type(usernameField, username);
        }
        public void TypeEmail(string email)
        {
            var emailField = RegisterForm["EmailField"];
            Type(emailField, email);
        }
        public void TypeFirstName(string firstName)
        {
            var firstNameField = RegisterForm["FirstNameField"];
            Type(firstNameField, firstName);
        }
        public void TypeMiddleName(string middleName)
        {
            var middleNameField = RegisterForm["MiddleNameField"];
            Type(middleNameField, middleName);
        }
        public void TypeLastName(string lastName)
        {
            var lastNameField = RegisterForm["LastNameField"];
            Type(lastNameField, lastName);
        }
        public void TypePassword(string password)
        {
            var passwordField = RegisterForm["PasswordField"];
            Type(passwordField, password);
        }
        public void TypeConfirmPass(string confirmPass)
        {
            var confirmPasswordField = RegisterForm["ConfirmPasswordField"];
            Type(confirmPasswordField, confirmPass);
        }
        public void ClickRegisterBtn()
        {
            var registerBtn = RegisterForm["SignUpBtn"];
            Click(registerBtn);
        }
        public void ClickLoginHereLink()
        {
            var loginHereLink = RegisterForm["LoginHereLink"];
            Click(loginHereLink);
        }
        public bool IsEmailValid()
        {
            var emailField = FindElement(RegisterForm["EmailField"]);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            bool isEmailValid = (bool)js.ExecuteScript("return arguments[0].checkValidity();", emailField);

            if (isEmailValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsRepeatPasswordAboveMaxLength()
        {
            var repeatPassField = FindElement(RegisterForm["ConfirmPasswordField"]);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            bool isRepeatTooLong = (bool)js.ExecuteScript("return arguments[0].value.length > arguments[0].maxLength;", repeatPassField);

            if (isRepeatTooLong)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //register
        public void RegisterUser_RequiredFields(string username, string email, string firstName, string lastName, string password, string confirmPass)
        {
            TypeUsername(username);
            TypeEmail(email);
            TypeFirstName(firstName);
            TypeLastName(lastName);
            TypePassword(password);
            TypeConfirmPass(confirmPass);
            ClickRegisterBtn();
        }
        public void RegisterUser_AllFields(string username, string email, string firstName, string middleName, string lastName, string password, string confirmPass)
        {
            TypeUsername(username);
            TypeEmail(email);
            TypeFirstName(firstName);
            TypeMiddleName(middleName);
            TypeLastName(lastName);
            TypePassword(password);
            TypeConfirmPass(confirmPass);
            ClickRegisterBtn();
        }

        //get text for els
        public string GetMainHeading()
        {
            return GetText(SpoilStoryHeading);
        }
        public string GetRepeatPassPlaceholder()
        {
            return GetText(RegisterForm["ConfirmPassPlaceholder"]);
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
            else if (fieldName == "first name")
            {
                return GetText(RequredErrorMsgs["FirstNameField"]);
            }
            else if (fieldName == "last name")
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
            else if (fieldName == "email")
            {
                return GetText(MinValueErrorMsgs["EmailField"]);
            }
            else if (fieldName == "first name")
            {
                return GetText(MinValueErrorMsgs["FirstNameField"]);
            }
            else if (fieldName == "last name")
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
        public string GetErrorMsg_InvalidEmail()
        {
            var emailField = FindElement(RegisterForm["EmailField"]);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            bool isEmailValid = (bool)js.ExecuteScript("return arguments[0].checkValidity();", emailField);

            string validationMessage = (string)js.ExecuteScript("return arguments[0].validationMessage;", emailField);

            if (!isEmailValid)
            {
                return validationMessage;
            }
            else
            {
                return null;
            }
        }

        //get value for fields
        public string GetFirstName()
        {
            var firstNameField = RegisterForm["FirstNameField"];
            return GetInputValue(firstNameField);
        }
        public string GetLastName()
        {
            var lastNameField = RegisterForm["LastNameField"];
            return GetInputValue(lastNameField);
        }
        public string GetEmail()
        {
            var emailField = RegisterForm["EmailField"];
            return GetInputValue(emailField);
        }
    }
}


