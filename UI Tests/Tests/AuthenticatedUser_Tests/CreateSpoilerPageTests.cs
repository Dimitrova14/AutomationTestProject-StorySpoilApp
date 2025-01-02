using StorySpoilAppTests.Pages;

namespace StorySpoilAppTests.Tests.AuthenticatedUser_Tests
{
    [TestFixture]
    public class CreateSpoilerPageTests : BaseTest
    {
        private string LastSpoilerTitle;
        private string LastSpoilerDescription;
        private string LastSpoilerPictureUrl;

        [SetUp]
        public void Setup()
        {
            homePage_LoggedIn.ClickCreateSpoilerLink();
            Assert.That(createSpoilerPage.IsPageDisplayed(), Is.True);
        }

        [Test]
        //check if form is displayed - DONE
        public void VerifyCreateFormIsDisplayed()
        {
            //navbar displayed
            Assert.That(createSpoilerPage.IsCreateFormDisplayed(), Is.True);
        }
        [Test]
        //check for required fields - DONE
        public void VerifyRequiredFields()
        {
            //click create button
            createSpoilerPage.ClickCreateBtn();

            Assert.Multiple(() =>
            {
                Assert.That(createSpoilerPage.GetErrorMsg_RequiredField("title"), Is.EqualTo("The Title field is required."));
                Assert.That(createSpoilerPage.GetErrorMsg_RequiredField("description"), Is.EqualTo("The Description field is required."));
                Assert.That(createSpoilerPage.GetMainErrorMsg(), Does.Contain("Unable to add this spoiler!"));
                Assert.That(createSpoilerPage.IsPageDisplayed(), Is.True);
            });
        }
        [Test]
        //create spoiler -> mandatory fields - DONE
        public void VerifySucessfullCreationOfSpoiler_MandatoryFields()
        {
            LastSpoilerTitle = $"Title: {GenerateRandomNumber()}";
            LastSpoilerDescription = $"Description: {GenerateRandomNumber()}";

            //type title
            createSpoilerPage.CreateSpoiler_RequiredFields(LastSpoilerTitle, LastSpoilerDescription);
 
            Assert.That(homePage_LoggedIn.IsPageDisplayed(), Is.True, $"Home page for logged in users is not displayed. Current url of the page: {driver.Url}");

            Assert.Multiple(() =>
            {
                //assert els are displayed 
                Assert.That(homePage_LoggedIn.IsTitleDisplayed(), Is.True);
                Assert.That(homePage_LoggedIn.IsDescriptionDisplayed(), Is.True);
                Assert.That(homePage_LoggedIn.IsImageDisplayed(), Is.True);
                Assert.That(homePage_LoggedIn.IsEditBtnDisplayed(), Is.True);
                Assert.That(homePage_LoggedIn.IsDeleteBtnDisplayed(), Is.True);

                //assert text matches created
                Assert.That(homePage_LoggedIn.GetTitleCard(), Is.EqualTo(LastSpoilerTitle));
                Assert.That(homePage_LoggedIn.GetDescriptionCard(), Is.EqualTo(LastSpoilerDescription));
            });

            //Delete created card
            homePage_LoggedIn.ClickDeleteBtn();

            Assert.That(homePage_LoggedIn.GetCountCards(), Is.EqualTo(0), "Card is still present after deletion");
        }
        [Test]
        //create spoiler w/out title - DONE 
        public void VerifySpoilerIsNotCreated_MissTitle_RequiredFieldIsDisplayed()
        {
            LastSpoilerDescription = $"Description: {GenerateRandomNumber()}";
            LastSpoilerTitle = "";

            //type title
            createSpoilerPage.TypeTitle(LastSpoilerTitle);
            createSpoilerPage.TypeDescription(LastSpoilerDescription);

            createSpoilerPage.ClickCreateBtn();

            Assert.Multiple(() =>
            {
                //assert required msg is displayed 
                Assert.That(createSpoilerPage.GetErrorMsg_RequiredField("title"), Is.EqualTo("The Title field is required."));
                Assert.That(createSpoilerPage.GetMainErrorMsg(), Does.Contain("Unable to add this spoiler!"));
                Assert.That(createSpoilerPage.IsPageDisplayed(), Is.True, "Invalid Spoiler can be created -> without title");
            });
        }  
        [Test]
        //create spoiler w/out Description - DONE
        public void VerifySpoilerIsNotCreated_MissDescription_RequiredFieldIsDisplayed()
        {
            LastSpoilerTitle = $"Title: {GenerateRandomNumber()}";
            LastSpoilerDescription = "";

            //type title
            createSpoilerPage.TypeTitle(LastSpoilerTitle); 
            createSpoilerPage.TypeDescription(LastSpoilerDescription);

            createSpoilerPage.ClickCreateBtn();

            Assert.Multiple(() =>
            {
                //assert required msg is displayed 
                Assert.That(createSpoilerPage.GetErrorMsg_RequiredField("description"), Is.EqualTo("The Description field is required."));
                Assert.That(createSpoilerPage.GetMainErrorMsg(), Does.Contain("Unable to add this spoiler!"));
                Assert.That(createSpoilerPage.IsPageDisplayed(), Is.True, "Invalid Spoiler can be created -> without description");
            });
        }

        [Test]
        //create spoiler with title below lower boundary -> 1 character - DONE
        public void VerifyTitleBelowLowerBoundary_MinValueMsgIsDisplayed()
        {
            //type in fields
            LastSpoilerTitle = "A";
            LastSpoilerDescription = $"Description: {GenerateRandomNumber()}";

            //type title
            createSpoilerPage.TypeTitle(LastSpoilerTitle);
            createSpoilerPage.TypeDescription(LastSpoilerDescription);

            createSpoilerPage.ClickCreateBtn();

            Assert.Multiple(() =>
            {
                //assert required msg is displayed 
                Assert.That(createSpoilerPage.GetErrorMsg_MinValue("title"), Is.EqualTo("Title must be at least 2 symbols."));
                Assert.That(createSpoilerPage.GetMainErrorMsg(), Does.Contain("Unable to add this spoiler!"));
                Assert.That(createSpoilerPage.IsPageDisplayed(), Is.True, "Invalid Spoiler can be created -> title below lower boundary");
            });
        }
        [Test]
        //create spoiler with description below lower boundary -> 2 characters - DONE
        public void VerifyDescriptionBelowLowerBoundary_MinValueMsgIsDisplayed()
        {
            //type in fields
            LastSpoilerTitle = $"Title: {GenerateRandomNumber()}";
            LastSpoilerDescription = "AB";

            //type title
            createSpoilerPage.TypeTitle(LastSpoilerTitle);
            createSpoilerPage.TypeDescription(LastSpoilerDescription);

            createSpoilerPage.ClickCreateBtn();

            Assert.Multiple(() =>
            {
                //assert required msg is displayed 
                Assert.That(createSpoilerPage.GetErrorMsg_MinValue("description"), Is.EqualTo("Desctiption must be at least 3 symbols."));
                Assert.That(createSpoilerPage.GetMainErrorMsg(), Does.Contain("Unable to add this spoiler!"));
                Assert.That(createSpoilerPage.IsPageDisplayed(), Is.True, "Invalid Spoiler can be created -> description below lower boundary");
            });
        }

        [Test] 
        //BUG - DONE
        //very long input fields
        public void VerifyTitleWithVeryLongInput_ValidLength_ResultsInContentOverflow()
        {
            string titleOf50Chars = ("Title: ").PadRight(50, '9');

            LastSpoilerTitle = titleOf50Chars;
            LastSpoilerDescription = $"Description: {GenerateRandomNumber()}";

            //type title
            createSpoilerPage.TypeTitle(LastSpoilerTitle);
            createSpoilerPage.TypeDescription(LastSpoilerDescription);

            createSpoilerPage.ClickCreateBtn();

            Assert.That(homePage_LoggedIn.IsPageDisplayed(), Is.True, $"Home page for logged in users is not displayed. Current url of the page: {driver.Url}");

            Assert.Multiple(() =>
            {
                //check for overflow hidden + wrap 
                Assert.That(homePage_LoggedIn.IsTitleWrappedAndHidden(), Is.True, "Title is not displayed on multiple lines and hidden when it overflows the field");

                //assert text matches created
                Assert.That(homePage_LoggedIn.GetTitleCard(), Is.EqualTo(LastSpoilerTitle));
                Assert.That(homePage_LoggedIn.GetDescriptionCard(), Is.EqualTo(LastSpoilerDescription));

                //assert els are displayed 
                Assert.That(homePage_LoggedIn.IsTitleDisplayed(), Is.True);
                Assert.That(homePage_LoggedIn.IsDescriptionDisplayed(), Is.True);
                Assert.That(homePage_LoggedIn.IsImageDisplayed(), Is.True);
                Assert.That(homePage_LoggedIn.IsEditBtnDisplayed(), Is.True);
                Assert.That(homePage_LoggedIn.IsDeleteBtnDisplayed(), Is.True);

                //Delete created card
                homePage_LoggedIn.ClickDeleteBtn();

                Assert.That(homePage_LoggedIn.GetCountCards(), Is.EqualTo(0), "Card is still present after deletion");
            });
        }
        [Test] 
        //DONE - BUG
        public void VerifyDescriptionWithVeryLongInput_ValidLength_ResultsInContentOverflow()
        {
            string descOf150Chars = ("Description: ").PadRight(150, '9');

            LastSpoilerDescription = descOf150Chars;
            LastSpoilerTitle = $"Title: {GenerateRandomNumber()}";

            //type in fields
            createSpoilerPage.TypeTitle(LastSpoilerTitle);
            createSpoilerPage.TypeDescription(LastSpoilerDescription);

            createSpoilerPage.ClickCreateBtn();

            Assert.That(homePage_LoggedIn.IsPageDisplayed(), Is.True, $"Home page for logged in users is not displayed. Current url of the page: {driver.Url}");

            Assert.Multiple(() =>
            {
                //check for overflow hidden & wrap 
                Assert.That(homePage_LoggedIn.IsDescriptionWrappedAndHidden(), Is.True, "Description is not displayed on multiple lines and hidden when it overflows the field");

                //assert text matches created
                Assert.That(homePage_LoggedIn.GetDescriptionCard(), Is.EqualTo(LastSpoilerDescription));
                Assert.That(homePage_LoggedIn.GetTitleCard(), Is.EqualTo(LastSpoilerTitle));

                //assert els are displayed 
                Assert.That(homePage_LoggedIn.IsTitleDisplayed(), Is.True);
                Assert.That(homePage_LoggedIn.IsDescriptionDisplayed(), Is.True);
                Assert.That(homePage_LoggedIn.IsImageDisplayed(), Is.True);
                Assert.That(homePage_LoggedIn.IsEditBtnDisplayed(), Is.True);
                Assert.That(homePage_LoggedIn.IsDeleteBtnDisplayed(), Is.True);

                //Delete created card
                homePage_LoggedIn.ClickDeleteBtn();

                Assert.That(homePage_LoggedIn.GetCountCards(), Is.EqualTo(0), "Card is still present after deletion");

            });
        }

        [Test]
        //create spoiler with invalid url -> without http:// - DONE
        public void VerifySpoilerWithInvalidUrlPicture_InvalidFieldIsDisplayed()
        {
            //type in fields
            LastSpoilerTitle = $"Title: {GenerateRandomNumber()}";
            LastSpoilerDescription = $"Description: {GenerateRandomNumber()}";
            LastSpoilerPictureUrl = "example.com/image.png";

            //create spoiler
            createSpoilerPage.CreateSpoiler_AllFields(LastSpoilerTitle, LastSpoilerDescription, LastSpoilerPictureUrl);

            //invalid url & stays on the same page 
            Assert.Multiple(() =>
            {
                Assert.That(createSpoilerPage.GetErrorMsg_InvalidUrlField(), Is.EqualTo("Invalid url!"));
                Assert.That(createSpoilerPage.GetMainErrorMsg(), Does.Contain("Unable to add this spoiler!"));
                Assert.That(createSpoilerPage.IsPageDisplayed(), Is.True, "Spoiler can be created witn invalid url -> without 'http://'");
            });

        }
        
    }
}
