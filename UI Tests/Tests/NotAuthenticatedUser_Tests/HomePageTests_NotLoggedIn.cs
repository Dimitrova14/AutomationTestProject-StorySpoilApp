
namespace StorySpoilAppTests.Tests.NotAuthenticatedUser_Tests
{
    [TestFixture]
    public class HomePageTests_NotLoggedIn : BaseTest
    {
        //NAV BAR SECTION
        [Test]
        public void VerifyNavBarSectionIsDisplayed()
        {
            Assert.Multiple(() =>
            {
                Assert.That(homePage_notLoggedIn.IsNavBarDisplayed(), Is.True, "NavBar is not displayed");

                //els displayed 
                Assert.That(homePage_notLoggedIn.AreNavBarLinksDisplayed(), Is.True, "Not all links on NavBar are displayed");
            });   
        }
        [Test]
        public void VerifySignUpLink_RegisterPageIsDisplayed()
        {
            //click link
            homePage_notLoggedIn.ClickSignUpLink();

            //assert page is dispalyed
            Assert.That(registerPage.IsPageDisplayed(), Is.True, $"Sign Up link does not redirect to Register page.Current page url: {driver.Url}");
        }
        [Test]
        public void VerifyStorySpoilLink_HomePageIsDisplayed()
        {
            //click link
            homePage_notLoggedIn.ClickHomePageLink();

            //assert page is dispalyed
            Assert.That(homePage_notLoggedIn.IsPageDisplayed(), Is.True, $"StorySpoil link does not redirect to Home page for not logged in users. Current page url: {driver.Url}");
        }
        [Test]
        public void VerifyLogInLink_LoginPageIsDisplayed()
        {
            //click link
            homePage_notLoggedIn.ClickLogInLink();

            //assert page is dispalyed
            Assert.That(loginPage.IsPageDisplayed(), Is.True, $"Log In link does not redirect to Login page. Current page url: {driver.Url}");
        }

        //HEADER SECTION
        [Test]
        public void VerifyHeaderSectionIsDisplayed()
        {
            Assert.Multiple(() =>
            {
                Assert.That(homePage_notLoggedIn.CheckHeaderSection_AllElsDisplayed(), Is.True, "Not all elements are displayed on Header Section");

                //get the text on heading
                Assert.That(homePage_notLoggedIn.GetHeading_HeaderSection("heading"), Is.EqualTo("STORY SPOILER"), $"Text on Heading does not match expected. Displayed text: {homePage_notLoggedIn.GetHeading_HeaderSection("heading")}");

                //get the text on subheading
                Assert.That(homePage_notLoggedIn.GetHeading_HeaderSection("subheading"), Is.EqualTo("Share your spoilers responsibly and kindle the love for storytelling."), $"Text on Subheading does not match expected. Displayed text: {homePage_notLoggedIn.GetHeading_HeaderSection("subheading")}");
            });

        }

        //PROMOTE APP SECTIONS    
        [Test]
        public void VerifySummarizeSectionIsDisplayed() 
        {
            Assert.Multiple(() =>
            {
                Assert.That(homePage_notLoggedIn.CheckSummarizeSection_AllElsDisplayed(), Is.True, "Not All elements are displayed on Summarize Section");

                Assert.That(homePage_notLoggedIn.GetHeading_PromoteAppSection("summarise"), Does.Contain("Summarize the Story"), $"Heading on Summarize section does not match expected. Current text displayed: {homePage_notLoggedIn.GetHeading_PromoteAppSection("summarise")}");

                Assert.That(homePage_notLoggedIn.GetDescription_PromoteAppSection("summarise"), Does.Contain("Craft a tantalizing summary that captivates the imagination while respecting the magic of the narrative"), $"Description on Summarize section does not match expected. Current text displayed: {homePage_notLoggedIn.GetDescription_PromoteAppSection("summarise")}");
            });
        }
        [Test]
        public void VerifyUploadSectionIsDisplayed() 
        {
            Assert.Multiple(() =>
            {
                Assert.That(homePage_notLoggedIn.CheckUploadSection_AllElsDisplayed(), Is.True, "Not All elements are displayed on Upload Section");

                Assert.That(homePage_notLoggedIn.GetHeading_PromoteAppSection("upload"), Does.Contain("Upload a Picture"), $"Heading on Upload section does not match expected. Current text displayed: {homePage_notLoggedIn.GetHeading_PromoteAppSection("upload")}");

                Assert.That(homePage_notLoggedIn.GetDescription_PromoteAppSection("upload"), Does.Contain("Enhance your spoiler with visuals that add an extra layer of intrigue and excitement"), $"Description on Upload section does not match expected. Current text displayed: {homePage_notLoggedIn.GetDescription_PromoteAppSection("upload")}");
            });
        }
        [Test]
        //BUG
        public async Task VerifyImageUrlIsInvalid_UploadSection()
        {
            var isImgUrlValid = await homePage_notLoggedIn.IsImageUrlValid();

            //check for valid url
            Assert.That(isImgUrlValid, Is.False, "Image Url should not be valid as no picture is displayed");
        }
        [Test]
        public void VerifyReadyToSpoilSectionIsDisplayed() 
        {
            Assert.Multiple(() =>
            {
                Assert.That(homePage_notLoggedIn.CheckReadyToSpoilSection_AllElsDisplayed(), Is.True, "Not All elements are displayed on ReadyToSpoil Section");

                Assert.That(homePage_notLoggedIn.GetHeading_PromoteAppSection("ready"), Does.Contain("Ready to Spoil a Story?"), $"Heading on ReadyToSpoil section does not match expected. Current text displayed: {homePage_notLoggedIn.GetHeading_PromoteAppSection("ready")}");

                Assert.That(homePage_notLoggedIn.GetDescription_PromoteAppSection("ready"), Does.Contain("Join our community of passionate enthusiasts and embark on an unforgettable journey of shared storytelling!"), $"Description on ReadyToSpoil section does not match expected. Current text displayed: {homePage_notLoggedIn.GetDescription_PromoteAppSection("ready")}");
            });

        }

        //COPYRIGHT LINK - BUG  
        [Test]
        public void VerifyCopyrightLink_CopyrightPageIsDisplayed()
        {
            //click link
            homePage_notLoggedIn.ClickCopyrightLink();

            Assert.That(driver.Url, Does.Contain("/Copyright"), $"Copyright link does not redirect to Copyright page. Current page url: {driver.Url}");
        }
    }
}
