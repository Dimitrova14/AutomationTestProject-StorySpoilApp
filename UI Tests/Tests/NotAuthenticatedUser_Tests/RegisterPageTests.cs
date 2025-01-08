
using StorySpoilAppTests.Pages;

namespace StorySpoilAppTests.Tests.NotAuthenticatedUser_Tests
{
    [TestFixture]
    public class RegisterPageTests : BaseTest
    {
        private string Username;
        private string Email;
        private string FirstName;
        private string MiddleName;
        private string LastName;
        private string Password;
        private string ConfrimPass;
 
        [SetUp]
        public void Setup()
        {
            homePage_notLoggedIn.ClickSignUpLink();
            Assert.That(registerPage.IsPageDisplayed(), Is.True, $"Register page is not displayed. Current url of the page: {driver.Url}");
        }

        [Test]
        //check register form displayed
        public void VerifyRegisterFormIsDisplayed()
        {
            Assert.That(registerPage.IsRegisterFormDisplayed(), Is.True, "Register form is not displayed with all elements.");
        }
        [Test]
        //check placeholder for repeat pass - BUG
        public void VerifyRepeatPassPlaceholder()
        {
            Assert.That(registerPage.GetRepeatPassPlaceholder(), Is.EqualTo("Repeat Password"), $"Placeholder for Repeat Password does not match expected. Current displayed placeholder: {registerPage.GetRepeatPassPlaceholder()}");
        }
        [Test]
        //check required fields
        public void VerifyRequiredFields()
        {
            //click register btn
            registerPage.ClickRegisterBtn();

            //assert required fields are displayed
            Assert.Multiple(() =>
            {
                Assert.That(registerPage.GetErrorMsg_RequiredField("username"), Is.EqualTo("User name is required!"), "Validation message for required username does not match expected one");

                Assert.That(registerPage.GetErrorMsg_RequiredField("email"), Is.EqualTo("The e-mail is required!"), "Validation message for required email does not match expected one");

                Assert.That(registerPage.GetErrorMsg_RequiredField("first name"), Is.EqualTo("First name is required!"), "Validation message for required First name does not match expected one");

                Assert.That(registerPage.GetErrorMsg_RequiredField("last name"), Is.EqualTo("Last name is required!"), "Validation message for required Last name does not match expected one");

                Assert.That(registerPage.GetErrorMsg_RequiredField("password"), Is.EqualTo("The password is required!"), "Validation message for required password does not match expected one");

                Assert.That(registerPage.GetErrorMsg_RequiredField("confirm password"), Is.EqualTo("The repeat password is required!"), "Validation message for required confirm password does not match expected one");
            });

            Assert.That(registerPage.IsPageDisplayed(), Is.True, "User can register with empty register form");
        }
        [Test]
        //register with mandatory fields
        public void VerifySuccesfulRegistration_MandatoryFields()
        {
            //test data
            Username = $"testUser_{GenerateRandomNumber()}";
            Email = $"{Username}@abv.bg";
            FirstName = "Test";
            LastName = "User";
            Password = "123456";
            ConfrimPass = Password;

            File.WriteAllText("testData_Login.json", $"{{\"Username\": \"{Username}\", \"Password\":\"{Password}\"}}");

            File.WriteAllText("testData_UserData.json", $"{{\"FirstName\": \"{FirstName}\", \"LastName\":\"{LastName}\", \"Email\":\"{Email}\"}}");

            //login all fields
            registerPage.RegisterUser_RequiredFields(Username, Email, FirstName, LastName, Password, ConfrimPass);

            Assert.Multiple(() =>
            {
                Assert.That(homePage_loggedIn.IsPageDisplayed(), Is.True, $"Home page for logged in users is not displayed. Current page url: {driver.Url}");

                Assert.That(homePage_loggedIn.CheckWelcomeSection_AllElsDisplayed(), Is.True, "Not all elements are displayed on welcome section");

                Assert.That(homePage_loggedIn.GetWelcomeMsg(), Does.Contain(Username), $"Username in the welcome message does not match the real username. Current text: {homePage_loggedIn.GetWelcomeMsg()}");

            });

        }
        [Test]
        //register with ALL fields
        public void VerifySuccesfulRegistration_AllFields()
        {
            //test data
            Username = $"testUser_{GenerateRandomNumber()}";
            Email = $"{Username}@abv.bg";
            FirstName = "Test";
            MiddleName = "Random";
            LastName = "User";
            Password = "123456";
            ConfrimPass = Password;

            //login all fields
            registerPage.RegisterUser_AllFields(Username, Email, FirstName, MiddleName,LastName, Password, ConfrimPass);

            Assert.Multiple(() =>
            {
                Assert.That(homePage_loggedIn.IsPageDisplayed(), Is.True, $"Home page for logged in users is not displayed. Current page url: {driver.Url}");

                Assert.That(homePage_loggedIn.CheckWelcomeSection_AllElsDisplayed(), Is.True, "Not all elements are displayed on welcome section");

                Assert.That(homePage_loggedIn.GetWelcomeMsg(), Does.Contain(Username), $"Username in the welcome message does not match the real username. Current text: {homePage_loggedIn.GetWelcomeMsg()}");

            });

        }
        [Test]
        //register without username
        public void VerifyRegisterWithoutUsername_RequiredUsernameIsDisplayed()
        {
            //test data
            Username = "";
            Email = "test@abv.bg";
            FirstName = "Test";
            LastName = "Test";
            Password = "123456";
            ConfrimPass = Password;

            //login all fields
            registerPage.RegisterUser_RequiredFields(Username, Email, FirstName, LastName, Password, ConfrimPass);

            Assert.Multiple(() =>
            {
                Assert.That(registerPage.GetErrorMsg_RequiredField("username"), Is.EqualTo("User name is required!"), "Validation message for required username does not match expected");

                Assert.That(homePage_loggedIn.IsPageDisplayed(), Is.False, $"User can register without username");
            });

        }
        [Test]
        //register without email
        public void VerifyRegisterWithoutEmail_RequiredEmailIsDisplayed()
        {
            //test data
            Username = "Test";
            Email = "";
            FirstName = "Test";
            LastName = "Test";
            Password = "123456";
            ConfrimPass = Password;

            //login all fields
            registerPage.RegisterUser_RequiredFields(Username, Email, FirstName, LastName, Password, ConfrimPass);

            Assert.Multiple(() =>
            {
                Assert.That(registerPage.GetErrorMsg_RequiredField("email"), Is.EqualTo("The e-mail is required!"), "Validation message for required email does not match expected");

                Assert.That(homePage_loggedIn.IsPageDisplayed(), Is.False, $"User can register without email");
            });

        }
        [Test]
        //register without first name
        public void VerifyRegisterWithoutFirstName_RequiredFirstNameIsDisplayed()
        {
            //test data
            Username = "Test";
            Email = "test@abv.bg";
            FirstName = "";
            LastName = "Test";
            Password = "123456";
            ConfrimPass = Password;

            //login all fields
            registerPage.RegisterUser_RequiredFields(Username, Email, FirstName, LastName, Password, ConfrimPass);

            Assert.Multiple(() =>
            {
                Assert.That(registerPage.GetErrorMsg_RequiredField("first name"), Is.EqualTo("First name is required!"), "Validation message for required 'First name' does not match expected");

                Assert.That(homePage_loggedIn.IsPageDisplayed(), Is.False, $"User can register without First name");
            });

        }
        [Test]
        //register without last name
        public void VerifyRegisterWithoutLastName_RequiredLastNameIsDisplayed()
        {
            //test data
            Username = "Test";
            Email = "test@abv.bg";
            FirstName = "Test";
            LastName = "";
            Password = "123456";
            ConfrimPass = Password;

            //login all fields
            registerPage.RegisterUser_RequiredFields(Username, Email, FirstName, LastName, Password, ConfrimPass);

            Assert.Multiple(() =>
            {
                Assert.That(registerPage.GetErrorMsg_RequiredField("last name"), Is.EqualTo("Last name is required!"), "Validation message for required 'Last name' does not match expected");

                Assert.That(homePage_loggedIn.IsPageDisplayed(), Is.False, $"User can register without Last name");
            });

        }
        [Test]
        //register without password
        public void VerifyRegisterWithoutPassword_RequiredPasswordIsDisplayed()
        {
            //test data
            Username = "Test";
            Email = "test@abv.bg";
            FirstName = "Test";
            LastName = "Test";
            Password = "";
            ConfrimPass = "123456";

            //login all fields
            registerPage.RegisterUser_RequiredFields(Username, Email, FirstName, LastName, Password, ConfrimPass);

            Assert.Multiple(() =>
            {
                Assert.That(registerPage.GetErrorMsg_RequiredField("password"), Is.EqualTo("The password is required!"), "Validation message for required password does not match expected");

                Assert.That(registerPage.GetErrorMsg_NotMatchingPass(), Is.EqualTo("Passwords don't match."), "Validation message for 'not matching passwords' does not match expected");

                Assert.That(homePage_loggedIn.IsPageDisplayed(), Is.False, $"User can register without password");
            });

        }
        [Test]
        //register without repeat password
        public void VerifyRegisterWithoutRepeatingPassword_RequiredRepeatPasswordIsDisplayed()
        {
            //test data
            Username = "Test";
            Email = "test@abv.bg";
            FirstName = "Test";
            LastName = "Test";
            Password = "123456";
            ConfrimPass = "";

            //login all fields
            registerPage.RegisterUser_RequiredFields(Username, Email, FirstName, LastName, Password, ConfrimPass);

            Assert.Multiple(() =>
            {
                Assert.That(registerPage.GetErrorMsg_RequiredField("confirm password"), Is.EqualTo("The repeat password is required!"), "Validation message for required 'repeat password' does not match expected");

                Assert.That(homePage_loggedIn.IsPageDisplayed(), Is.False, $"User can register without repeating password");
            });

        }
        [Test]
        //verify username below lower boundary -> 1 char
        public void VerifyUsernameBelowLowerBoundary_MinValueMsgIsDisplayed()
        {
            //test data
            Username = "A";
            Email = "test@abv.bg";
            FirstName = "Test";
            LastName = "Test";
            Password = "123456";
            ConfrimPass = Password;

            //login all fields
            registerPage.RegisterUser_RequiredFields(Username, Email, FirstName, LastName, Password, ConfrimPass);

            Assert.Multiple(() =>
            {
                Assert.That(registerPage.GetErrorMsg_MinValue("username"), Is.EqualTo("UserName has to be at least 2 symbols!"), "Message for required MIN length for Username does not match expected");

                Assert.That(homePage_loggedIn.IsPageDisplayed(), Is.False, $"User can register with invalid username of only 1 character");
            });

        }
        [Test]
        //verify email below lower boundary -> 5 chars
        public void VerifyEmailBelowLowerBoundary_MinValueMsgIsDisplayed()
        {
            //test data
            Username = "Test";
            Email = "t@a.g";
            FirstName = "Test";
            LastName = "Test";
            Password = "123456";
            ConfrimPass = Password;

            //login all fields
            registerPage.RegisterUser_RequiredFields(Username, Email, FirstName, LastName, Password, ConfrimPass);

            Assert.Multiple(() =>
            {
                Assert.That(registerPage.GetErrorMsg_MinValue("email"), Is.EqualTo("Email has to be at least 6 symbols!"), "Message for required MIN length for Email does not match expected");

                Assert.That(homePage_loggedIn.IsPageDisplayed(), Is.False, $"User can register with invalid Email -> below lower boundary");
            });

        }
        [Test]
        //verify first name below lower boundary -> 1 char
        public void VerifyFirstNameBelowLowerBoundary_MinValueMsgIsDisplayed()
        {
            //test data
            Username = "Test";
            Email = "test@abv.bg";
            FirstName = "A";
            LastName = "Test";
            Password = "123456";
            ConfrimPass = Password;

            //login all fields
            registerPage.RegisterUser_RequiredFields(Username, Email, FirstName, LastName, Password, ConfrimPass);

            Assert.Multiple(() =>
            {
                Assert.That(registerPage.GetErrorMsg_MinValue("first name"), Is.EqualTo("First name has to be at least 2 symbols!"), "Message for required MIN length for 'First name' does not match expected");

                Assert.That(homePage_loggedIn.IsPageDisplayed(), Is.False, $"User can register with invalid First name of only 1 character");
            });

        }
        [Test]
        //verify last name below lower boundary -> 1 char - BUG 
        public void VerifyLastNameBelowLowerBoundary_MinValueMsgIsDisplayed()
        {
            //test data
            Username = "Test";
            Email = "test@abv.bg";
            FirstName = "Test";
            LastName = "A";
            Password = "123456";
            ConfrimPass = Password;

            //login all fields
            registerPage.RegisterUser_RequiredFields(Username, Email, FirstName, LastName, Password, ConfrimPass);

            Assert.Multiple(() =>
            {
                Assert.That(registerPage.GetErrorMsg_MinValue("last name"), Is.EqualTo("Last name has to be at least 2 symbols!"), $"Message for required MIN length for 'Last name' does not match expected. Current validation message: {registerPage.GetErrorMsg_MinValue("last name")}");

                Assert.That(homePage_loggedIn.IsPageDisplayed(), Is.False, $"User can register with invalid Last name of only 1 character");
            });

        }
        [Test]
        //verify password below lower boundary -> 5 chars
        public void VerifyPasswordBelowLowerBoundary_MinValueMsgIsDisplayed()
        {
            //test data
            Username = "Test";
            Email = "test@abv.bg";
            FirstName = "Test";
            LastName = "Test";
            Password = "12345";
            ConfrimPass = Password;

            //login all fields
            registerPage.RegisterUser_RequiredFields(Username, Email, FirstName, LastName, Password, ConfrimPass);

            Assert.Multiple(() =>
            {
                Assert.That(registerPage.GetErrorMsg_MinValue("password"), Is.EqualTo("Password has to be at least 6 symbols!"), $"Message for required MIN length for Password does not match expected.");

                Assert.That(homePage_loggedIn.IsPageDisplayed(), Is.False, $"User can register with invalid Password -> below lower boundary");
            });

        }
        [Test]
        //verify not matching password
        public void VerifyNotMatchingPassword_NotMatchingPassMsgIsDisplayed()
        {
            //test data
            Username = "Test";
            Email = "test@abv.bg";
            FirstName = "Test";
            LastName = "Test";
            Password = "123456";
            ConfrimPass = Password + "7";

            //login all fields
            registerPage.RegisterUser_RequiredFields(Username, Email, FirstName, LastName, Password, ConfrimPass);

            Assert.Multiple(() =>
            {
                Assert.That(registerPage.GetErrorMsg_NotMatchingPass(), Is.EqualTo("Passwords don't match."), $"Message for NOT matching passwords does not match expected.");

                Assert.That(homePage_loggedIn.IsPageDisplayed(), Is.False, $"User can register with not matching password");
            });

        }
        [Test]
        //email without @
        public void VerifyInvalidEmail_WithoutAtSymbol()
        {
            //test data
            Username = $"Test_{GenerateRandomNumber()}";
            Email = $"{Username}abv.bg";
            FirstName = "Test";
            LastName = "Test";
            Password = "123456";
            ConfrimPass = Password;

            //login all fields
            registerPage.RegisterUser_RequiredFields(Username, Email, FirstName, LastName, Password, ConfrimPass);

            //check if it is valid & extract validation message
            Assert.Multiple(() =>
            {
                Assert.That(registerPage.IsEmailValid(), Is.False, $"Email without '@' should NOT be considered as valid");

                Assert.That(registerPage.GetErrorMsg_InvalidEmail(), Does.Contain("Please include an '@' in the email address"), "Validation message for invalid email does not match expected one");

                Assert.That(homePage_loggedIn.IsPageDisplayed(), Is.False, $"User can register with invalid email -> without '@' symbol");
            });
        }
        [Test]
        //email without top level domain -> .com - BUG
        public void VerifyInvalidEmail_WithoutTopLevelDomain()
        {
            //test data
            Username = $"Test_{GenerateRandomNumber()}";
            Email = $"{Username}@abv";
            FirstName = "Test";
            LastName = "Test";
            Password = "123456";
            ConfrimPass = Password;

            var errorMsg = registerPage.GetErrorMsg_InvalidEmail();

            //login all fields
            registerPage.RegisterUser_RequiredFields(Username, Email, FirstName, LastName, Password, ConfrimPass);

            Assert.Multiple(() =>
            {
                Assert.That(errorMsg, Is.Not.Null, $"Validation message for invalid email is not displayed after the register form is submitted.");
                Assert.That(registerPage.IsPageDisplayed(), Is.True, $"User should not be able to register with invalid email -> without 'Top level domain'. Current url: {driver.Url}");
            });
        }
        [Test]
        //email without domain & top level domain -> abv.com - BUG
        public void VerifyInvalidEmail_WithoutDomainAndTopLevelDomain()
        {
            //test data
            Username = $"Test_{GenerateRandomNumber()}";
            Email = $"{Username}@";
            FirstName = "Test";
            LastName = "Test";
            Password = "123456";
            ConfrimPass = Password;

            //login all fields
            registerPage.RegisterUser_RequiredFields(Username, Email, FirstName, LastName, Password, ConfrimPass);

            //check if it is valid & extract validation message
            Assert.Multiple(() =>
            {
                Assert.That(registerPage.IsEmailValid(), Is.False, $"Email without 'Domain & Top level domain' should NOT be considered as valid");

                Assert.That(registerPage.GetErrorMsg_InvalidEmail(), Does.Contain($"'{Email}' is incomplete"), "Validation message for invalid email does not match expected one");

                Assert.That(homePage_loggedIn.IsPageDisplayed(), Is.False, $"User can register with invalid email -> without 'Domain & Top level domain'");
            });
        }
        [Test]
        //verify fields on the lower boundaries
        public void VerifyMandatoryFieldsOnTheLowerBoundaries()
        {
            //test data
            Username = $"A{GenerateRandomChar()}";
            Email = $"A{GenerateRandomChar()}@a.bg";
            FirstName = "AB";
            LastName = "AB";
            Password = "123456";
            ConfrimPass = Password;

            //login all fields
            registerPage.RegisterUser_RequiredFields(Username, Email, FirstName, LastName, Password, ConfrimPass);

            Assert.Multiple(() =>
            {
                Assert.That(homePage_loggedIn.IsPageDisplayed(), Is.True, $"Home page for logged in users is not displayed. Current page url: {driver.Url}");

                Assert.That(homePage_loggedIn.CheckWelcomeSection_AllElsDisplayed(), Is.True, "Not all elements are displayed on welcome section");

                Assert.That(homePage_loggedIn.GetWelcomeMsg(), Does.Contain(Username), $"Username in the welcome message does not match the real username. Current text: {homePage_loggedIn.GetWelcomeMsg()}");

            });

        }
        [Test]
        //verify fields above the upper boundaries - BUG
        public void VerifyrRepeatPassAboveTheUpperBoundary()
        {
            //test data
            Username = $"testUser_{GenerateRandomNumber()}";
            Email = $"{Username}@abv.bg";
            FirstName = "Test";
            LastName = "User";
            Password = $"{GenerateRandomNumber()}".PadRight(31, '1');
            ConfrimPass = Password;

            //login all fields
            registerPage.RegisterUser_RequiredFields(Username, Email, FirstName, LastName, Password, ConfrimPass);

            //Please enter an email address
            Assert.Multiple(() =>
            {
                Assert.That(registerPage.IsRepeatPasswordAboveMaxLength(), Is.True, $"Repeat password is above MAX length and there is no validation to restrict it");

                Assert.That(registerPage.IsPageDisplayed(), Is.True, $"User can register with invalid repeat password -> above upper boundaries. Current url: {driver.Url}");

            });
        }
        [Test]
        //verify 'LOG IN HERE' link
        public void VerifyLoginHereLink_LoginPageIsDisplayed()
        {
            registerPage.ClickLoginHereLink();

            Assert.That(loginPage.IsPageDisplayed(), Is.True, $"Login Here link does not redirect to Login Page. Current page url: {driver.Url}");
        }
    }
}
