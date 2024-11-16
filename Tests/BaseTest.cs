using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using StorySpoilAppTests.Pages;

namespace StorySpoilAppTests.Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected RegisterPage registerPage;
        protected LoginPage loginPage;
        protected HomePage homePage;
        protected CreateSpoilerPage createSpoilerPage;
        protected EditSpoilerPage editSpoilerPage;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            registerPage = new RegisterPage();
            loginPage = new LoginPage();
            homePage = new HomePage();
            createSpoilerPage = new CreateSpoilerPage();
            editSpoilerPage = new EditSpoilerPage();
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
            //extract from login page the url
            driver.Navigate().GoToUrl("");
            LoginPage.LoginUser(username, password);
        }
    }
}