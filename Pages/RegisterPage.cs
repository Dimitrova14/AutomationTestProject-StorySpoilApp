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
            {"ConfrimPassField",By.CssSelector("span[data-valmsg-for='RePassword']']") },
        };
        private readonly By errorMssg_MinValue = By.CssSelector("span[data-valmsg-for='UserName']");


        public void RegisterUser(string username, string email, string firstName, string lastName, string password, string confirmPass)
        {
            Type(usernameField, username);
            Type(emailField, email);
            Type(firstNameField, firstName);
            Type(lastNameField, lastName);
            Type(passwordField, password);
            Type(confirmPasswordField, confirmPass);
            Click(signUpBtn);
        }
    };

}


