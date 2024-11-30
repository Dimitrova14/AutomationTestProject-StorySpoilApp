using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using StorySpoilAppTests.Pages;

namespace StorySpoilAppTests.Tests.NotAuthenticatedUser_Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected RegisterPage registerPage;
        protected HomePage_NotLoggedIn homePage;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            registerPage = new RegisterPage(driver);
            homePage = new HomePage_NotLoggedIn(driver);
            registerPage.OpenPage();
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
}
}
