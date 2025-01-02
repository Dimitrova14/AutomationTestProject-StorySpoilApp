using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using StorySpoilAppTests.Pages;

namespace StorySpoilAppTests.Tests.NotAuthenticatedUser_Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected RegisterPage registerPage;
        protected LoginPage loginPage;
        protected HomePage_NotLoggedIn homePage_notLoggedIn;
        protected HomePage_LoggedIn homePage_loggedIn;

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

            homePage_notLoggedIn = new HomePage_NotLoggedIn(driver);
            homePage_loggedIn = new HomePage_LoggedIn(driver);

            homePage_notLoggedIn.OpenPage();
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }

        //genereate random number
        public string GenerateRandomNumber()
        {
            var random = new Random();
            return random.Next(999, 100000).ToString();
        }

        //genereate random number from 1 to 9
        public string GenerateRandomNumber_1To9()
        {
            var random = new Random();
            return random.Next(1, 9).ToString();
        }

        public string GenerateRandomChar()
        {
            // Generate an array of printable ASCII characters (from '!' to '~')
            char[] asciiCharacters = Enumerable.Range(33, 94).Select(x => (char)x).ToArray(); // 94 characters: '!' to '~'

            // Shuffle the array
            Random random = new Random();
            asciiCharacters = asciiCharacters.OrderBy(x => random.Next()).ToArray();

            // Return the first random character as a string
            return asciiCharacters[0].ToString();
        }

}
}
