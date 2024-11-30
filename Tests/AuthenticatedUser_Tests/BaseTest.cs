using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using StorySpoilAppTests.Pages;

namespace StorySpoilAppTests.Tests.AuthenticatedUser_Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected RegisterPage registerPage;
        protected LoginPage loginPage;
        protected HomePage_LoggedIn homePage;
        protected CreateSpoilerPage createSpoilerPage;
        protected EditSpoilerPage editSpoilerPage;
        protected string Username;
        protected string Password;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            registerPage = new RegisterPage(driver);
            loginPage = new LoginPage(driver);
            homePage = new HomePage_LoggedIn(driver);
            createSpoilerPage = new CreateSpoilerPage(driver);
            editSpoilerPage = new EditSpoilerPage(driver);

            Username = "test_user";
            Password = "123456";

            loginPage.OpenPage();
            loginPage.Login(Username, Password);
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }

        protected void Login(string username, string password)
        {
            driver.Navigate().GoToUrl(loginPage.Url);
            loginPage.Login(username, password);
        }
    }
}