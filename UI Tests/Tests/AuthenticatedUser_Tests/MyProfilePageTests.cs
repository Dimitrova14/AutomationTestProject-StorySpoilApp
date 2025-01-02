

using Newtonsoft.Json;

namespace StorySpoilAppTests.Tests.AuthenticatedUser_Tests
{
    [TestFixture]
    public class MyProfilePageTests : BaseTest
    {
        private string FirstName;
        private string LastName;
        private string Email;

        [SetUp]
        public void SetUp()
        {
            homePage_LoggedIn.ClickUserProfileLink();
            Assert.That(myProfilePage.IsPageDisplayed(), Is.True);

            var testData = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("testData_UserData.json"));

            FirstName = testData.FirstName;
            LastName = testData.LastName;
            Email = testData.Email;
        }

        //USER PROFILE SECTION
        [Test]
        //DONE
        public void VerifyUserProfileSectionIsDisplayed()
        {
            Assert.Multiple(() =>
            {
                Assert.That(myProfilePage.IsUserProfileSectionDisplayed(), Is.True);

                Assert.That(myProfilePage.CheckUserProfileSection_AllElsDisplayed(), Is.True);

                Assert.That(myProfilePage.GetNameOfTheUser(), Is.EqualTo(Username));
            });
        }
        [Test]
        //DONE  - BUG 
        public void VerifyEditBtn_EditProfilePageIsDiplayed()
        {
            myProfilePage.ClickEditBtn();

            Assert.That(myProfilePage.IsPageDisplayed() , Is.False, $"Edit button does not redirect to Edit Profile page. Current page url: {driver.Url}");
        }


        //USER ATTRIBUTES SECTION
        [Test]
        //DONE
        public void VerifyUserAttributesSectionIsDisplayed()
        {
            Assert.Multiple(() =>
            { 
                Assert.That(myProfilePage.IsUserAttributesSectionDisplayed(), Is.True);

                Assert.That(myProfilePage.CheckUserAttributesSection_AllFieldsDisplayed(), Is.True);

                Assert.That(myProfilePage.CheckUserAttributesSection_LabelsDisplayed(), Is.True);
            });
        }
        [Test]
        //DONE
        public void VerifyTextOnFieldsIsCorrect()
        {
            //check email value 
            Assert.Multiple(() =>
            {
                Assert.That(myProfilePage.GetFullName(), Is.EqualTo($"{FirstName} {LastName}"));
                Assert.That(myProfilePage.GetEmail(), Is.EqualTo($"{Email}"));
            });
        }
        [Test]
        //DONE
        public void VerifyTotalSpoilersCounter()
        {
            //GET COUNT CARDS
            //ASSERT VALUE

            Assert.That(myProfilePage.GetTotalSpoilersCount(), Is.EqualTo(CountCards));
        }


        //ABOUT ME SECTION
        [Test]
        //DONE
        public void VerifyAboutMeSectionIsDisplayed()
        {
            Assert.Multiple(() =>
            {
                Assert.That(myProfilePage.IsAboutMeSectionDisplayed(), Is.True);

                Assert.That(myProfilePage.CheckAboutMeSection_LabelDisplayed(), Is.True);

                Assert.That(myProfilePage.CheckAboutMeSection_ElsDisplayed(), Is.True);
            });
        }
        [Test]
        //DONE
        public void VerifyTextOnAboutMeField()
        { 
            Assert.That(myProfilePage.GetAboutMeMessage(), Is.EqualTo("You haven't written anything about yourself..."));
        }
    }
}
