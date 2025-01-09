using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using StorySpoilAppTests.Pages;

namespace StorySpoilAppTests.Tests.AuthenticatedUser_Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected LoginPage loginPage;
        protected RegisterPage registerPage;
        protected HomePage_LoggedIn homePage_LoggedIn;
        protected HomePage_NotLoggedIn homePage_notLogged;
        protected CreateSpoilerPage createSpoilerPage;
        protected EditSpoilerPage editSpoilerPage;
        protected MyProfilePage myProfilePage;
        protected string Username;
        protected string Password;
        protected string CountCards;

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
            homePage_LoggedIn = new HomePage_LoggedIn(driver);
            homePage_notLogged = new HomePage_NotLoggedIn(driver);
            createSpoilerPage = new CreateSpoilerPage(driver);
            editSpoilerPage = new EditSpoilerPage(driver);
            myProfilePage = new MyProfilePage(driver);

            homePage_notLogged.OpenPage();
            homePage_notLogged.ClickLogInLink();

            var projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\StorySpoilAppTests"));

            var testDataFolderPath = Path.Combine(projectDirectory, "TestData");

            var testData_LoginFilePath = Path.Combine(testDataFolderPath, "testData_Login.json");

            var testData_Login = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(testData_LoginFilePath));

            Username = testData_Login.Username;
            Password = testData_Login.Password;

            loginPage.Login_AllFields(Username, Password);

            CountCards = homePage_LoggedIn.GetCountCards().ToString();
        }

        [TearDown]
        public void TearDown()
        {
             driver?.Quit();
             driver?.Dispose();
        }

        //generate random number
        public string GenerateRandomNumber()
        {
            var random = new Random();
            return random.Next(999, 9999).ToString();
        }
    }
}