
using Newtonsoft.Json;

namespace StorySpoilAppTests.Tests.NotAuthenticatedUser_Tests
{
    [TestFixture]
    public class LoginPageTests : BaseTest
    {
        private string Username;
        private string Password;

        [SetUp]
        public void Setup()
        {
            var testData_Login = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("testData_Login.json"));

            Username = testData_Login.Username;
            Password = testData_Login.Password;

            homePage_notLoggedIn.ClickLogInLink();
            Assert.That(loginPage.IsPageDisplayed(), Is.True, $"Login page is not displayed. Current url of the page: {driver.Url}");
        }
        [Test]
        //check login form displayed - DONE
        public void VerifyLoginFormIsDisplayed()
        {
            Assert.That(loginPage.IsLoginFormDisplayed(), Is.True, "Login form is not displayed with all elements.");
        }
        
        [Test]
        //check required fields - DONE
        public void VerifyRequiredFields()
        {
            //click login btn
            loginPage.ClickLoginBtn();

            //assert required fields are displayed
            Assert.Multiple(() =>
            {
                Assert.That(loginPage.GetMainErrorMsg(), Does.Contain("Unable to sign in!"), "Main error message does not match expected one");

                Assert.That(loginPage.GetErrorMsg_RequiredField("username"), Is.EqualTo("Username is required!"), "Validation message for required username does not match expected one");

                Assert.That(loginPage.GetErrorMsg_RequiredField("password"), Is.EqualTo("The password is required!"), "Validation message for required password does not match expected one");
            });

            Assert.That(loginPage.IsPageDisplayed(), Is.True, "User can login without username and password");
        }

        [Test]
        //login with all fields -DONE
        public void VerifySuccesfulLogin_CorrectUsernameAndPass()
        {
            //login all fields
            loginPage.Login_AllFields(Username, Password);

            Assert.Multiple(() =>
            {
                Assert.That(homePage_loggedIn.IsPageDisplayed(), Is.True, $"Home page for logged in users is not displayed. Current page url: {driver.Url}");

                Assert.That(homePage_loggedIn.CheckWelcomeSection_AllElsDisplayed(), Is.True, "Not all elements are displayed on welcome section");

                Assert.That(homePage_loggedIn.GetWelcomeMsg(), Does.Contain(Username), $"Username in the welcome message does not match the real username. Current text: {homePage_loggedIn.GetWelcomeMsg()}");

            });

        }
        [Test]
        //login w/out username - DONE
        public void VerifyLoginWithoutUsername_RequiredUsernameIsDisplayed()
        {
            Username = "";

            //login all fields
            loginPage.Login_AllFields(Username, Password);

            Assert.Multiple(() =>
            {
                Assert.That(loginPage.GetMainErrorMsg(), Does.Contain("Unable to sign in!"), $"Main error message does not match expected one");

                Assert.That(loginPage.GetErrorMsg_RequiredField("username"), Is.EqualTo("Username is required!"), "Validation message for required username does not match expected");
            });

            Assert.That(loginPage.IsPageDisplayed(), Is.True, "User can login without username");
        }
        [Test]
        //login w/out password - DONE
        public void VerifyLoginWithoutPassword_RequiredPasswordIsDisplayed()
        {
            Password = "";

            //login all fields
            loginPage.Login_AllFields(Username, Password);

            Assert.Multiple(() =>
            {
                Assert.That(loginPage.GetMainErrorMsg(), Does.Contain("Unable to sign in!"), $"Main error message does not match expected one");

                Assert.That(loginPage.GetErrorMsg_RequiredField("password"), Is.EqualTo("The password is required!"), "Validation message for required password does not match expected");
            });

            Assert.That(loginPage.IsPageDisplayed(), Is.True, "User can login without password");
        }
        [Test]
        //wrong username + correct pass - BUG - DONE
        public void VerifyNotSuccesfulLogin_IncorrectUsername_And_CorrectPass()
        {
            string prefix = Username.Substring(0, 8);
            string digits = Username.Substring(8);

            prefix = prefix.Replace("r", "R");

            Username = prefix + digits;

            //login all fields
            loginPage.Login_AllFields(Username, Password);

            Assert.Multiple(() =>
            {
                Assert.That(loginPage.GetMainErrorMsg(), Does.Contain("Unable to sign in!"), "Main error message does not match expected one");

                Assert.That(loginPage.IsPageDisplayed(), Is.True, $"User can login with incorrect username and correct password. Current page url: {driver.Url}");

            });

        }
        [Test]
        //wrong pass + correct username - DONE
        public void VerifyNotSuccesfulLogin_CorrectUsername_And_InCorrectPass()
        {
            Password += "7";

            //login all fields
            loginPage.Login_AllFields(Username, Password);

            Assert.Multiple(() =>
            {
                Assert.That(loginPage.GetMainErrorMsg(), Does.Contain("Unable to sign in!"), "Main error message does not match expected one");

                Assert.That(loginPage.IsPageDisplayed(), Is.True, "User can login with incorrect password and correct username");

            });

        }
        [Test]
        //wrong pass + wrong username - DONE
        public void VerifyNotSuccesfulLogin_InCorrectUsernameAndPass()
        {
            Username += "8";
            Password += "7";

            //login all fields
            loginPage.Login_AllFields(Username, Password);

            Assert.Multiple(() =>
            {
                Assert.That(loginPage.GetMainErrorMsg(), Does.Contain("Unable to sign in!"), "Main error message does not match expected one");

                Assert.That(loginPage.IsPageDisplayed(), Is.True, "User can login with incorrect password and correct username");

            });

        }
        [Test]
        //click forgot password link - BUG - DONE
        public void VerifyForgotPasswordLink_RestorePasswordPageIsDisplayed()
        {
            loginPage.ClickForgotPasswordLink();

            Assert.That(driver.Url, Does.Contain("/Restore"), $"Forgot password link does not redirect to Restore Password page. Current page url: {driver.Url}");
        }
        [Test]
        //click create new link - DONE
        public void VerifyCreateNewLink_RegisterPageIsDisplayed()
        {
            loginPage.ClickCreateNewLink();

            Assert.That(registerPage.IsPageDisplayed(), Is.True, $"'Create new' link does not redirect to Register page. Current page url: {driver.Url}");
        }

    }
}
