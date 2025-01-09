using Newtonsoft.Json;
using OpenQA.Selenium.BiDi.Modules.Input;
using System;

namespace StorySpoilAppTests.Tests.AuthenticatedUser_Tests
{
    [TestFixture]
    public class HomePageTests_LoggedIn : BaseTest
    {
        private string LastSpoilerTitle;
        private string LastSpoilerDescription;
        private string FirstSpoilerTitle;
        private string SecondSpoilerTitle;
        

        //NAV BAR SECTION
        [Test, Order(1)]
        public void VerifyNavBarSectionIsDisplayed()
        {
            Assert.Multiple(() =>
            {
                //navbar displayed
                Assert.That(homePage_LoggedIn.IsNavBarDisplayed(), Is.True, "NavBar is not displayed");

                //els displayed 
                Assert.That(homePage_LoggedIn.AreNavBarLinksDisplayed(), Is.True, "NavBar links are not displayed");
            });
        }
        [Test, Order(2)] 
        public void VerifyUserProfileLink_MyProfilePageIsDisplayed()
        {
            //click link
            homePage_LoggedIn.ClickUserProfileLink();

            //assert page is displayed
            Assert.That(myProfilePage.IsPageDisplayed(), Is.True);
        }
        [Test, Order(3)]
        public void VerifyStorySpoilLink_HomePageIsDisplayed()
        {
            //click link
            homePage_LoggedIn.ClickStorySpoilLink();

            //assert page is displayed
            Assert.That(homePage_LoggedIn.IsPageDisplayed(), Is.True);
        }
        [Test, Order(4)]
        public void VerifyCreateSpoilerLink_CreateSpoilerPageIsDisplayed()
        {
            //click link
            homePage_LoggedIn.ClickCreateSpoilerLink();

            //assert page is dispalyed
            Assert.That(createSpoilerPage.IsPageDisplayed(), Is.True);
        }
        [Test, Order(5)]
        public void VerifyLogoutLink_HomePageIsDisplayed()
        {
            //click link
            homePage_LoggedIn.ClickLogoutLink();

            //assert page is dispalyed
            Assert.That(homePage_notLogged.IsPageDisplayed(), Is.True);
        }


        //WELCOME SECTION
        [Test, Order(6)]
        public void VerifyWelcomeSection()
        {
            Assert.Multiple(() =>
            {
                //welcome section displayed
                Assert.That(homePage_LoggedIn.CheckWelcomeSection_AllElsDisplayed(), Is.True);

                //assert subheading contains username
                Assert.That(homePage_LoggedIn.GetWelcomeMsg(), Does.Contain(Username));
            });
        }


        //NO SPOILERS SECTION
        [Test, Order(7)]
        public void VerifyNoSpoilersSection()
        {
            string noSpoilersMsg = "No Spoilers Yet!";
            string noSpoilersDesc = "Your list of spoilers is currently empty";

            Assert.Multiple(() =>
            {
                Assert.That(homePage_LoggedIn.CheckNoSpoilersSection_AllElsDisplayed(), Is.True);
                Assert.That(homePage_LoggedIn.GetNoSpoilersMsg(), Does.Contain(noSpoilersMsg));
                Assert.That(homePage_LoggedIn.GetNoSpoilersDesc(), Does.Contain(noSpoilersDesc));
            });
        }
        [Test, Order(8)]
        public void ClickWriteSpoilerBtn_CreateSpoilerPageIsDisplayed()
        {
            //click link
            homePage_LoggedIn.ClickWriteSpoilerBtn();

            //assert page is dispalyed
            Assert.That(createSpoilerPage.IsPageDisplayed(), Is.True, $"Write spoiler button does not redirect to Create Spoiler page.Current page url: {driver.Url}");
        }


        //SEARCH FUNCTIONALITY
        //Create 2 spoilers
        [Test, Order(9)]
        public void Create2Spoilers()
        {
            homePage_LoggedIn.ClickCreateSpoilerLink();
            //create 1 spoiler
            FirstSpoilerTitle = "Title: 1";
            LastSpoilerDescription = $"Description: {GenerateRandomNumber()}";
            createSpoilerPage.CreateSpoiler_RequiredFields(FirstSpoilerTitle, LastSpoilerDescription);

            homePage_LoggedIn.ClickCreateSpoilerLink();
            //create 2 sppoiler
            SecondSpoilerTitle = "Title: 2";
            LastSpoilerDescription = $"Description: {GenerateRandomNumber()}";
            createSpoilerPage.CreateSpoiler_RequiredFields(SecondSpoilerTitle, LastSpoilerDescription);

            // Get the path of the project directory where your .csproj file is located
            var projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\StorySpoilAppTests"));

            // point to the TestData folder inside the project directory
            var testDataFolderPath = Path.Combine(projectDirectory, "TestData");

            // Ensure the TestData folder exists (create it if not)
            Directory.CreateDirectory(testDataFolderPath);

            // Specify the file path for the JSON file
            var testDataCardsFilePath = Path.Combine(testDataFolderPath, "testData_Cards.json");

            File.WriteAllText(testDataCardsFilePath, $"{{\"FirstTitle\": \"{FirstSpoilerTitle}\", \"SecondTitle\":\"{SecondSpoilerTitle}\"}}");

            Assert.That(homePage_LoggedIn.GetCountCards(), Is.EqualTo(2), $"Two cards should be present in the section. Current count cards: {homePage_LoggedIn.GetCountCards()}");
        }
        [Test, Order(10)]
        public void VerifySearchFunctionalityIsDisplayed()
        {
            Assert.Multiple(() =>
            {
                Assert.That(homePage_LoggedIn.IsSearchFieldDisplayed(), Is.True);
                Assert.That(homePage_LoggedIn.IsSearchBtnDisplayed(), Is.True);
            });
        }
        [Test, Order(11)]
        //BUG
        public void VerifySearchWithExistingSpoiler()
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\StorySpoilAppTests"));

            var testDataFolderPath = Path.Combine(projectDirectory, "TestData");

            var testData_CardsFilePath = Path.Combine(testDataFolderPath, "testData_Cards.json");

            var testData_Cards = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(testData_CardsFilePath));

            string secondSpoilerTitle = (string)testData_Cards.SecondTitle;
            string firstSpoilerTitle = (string)testData_Cards.FirstTitle;

            //type in search field
            homePage_LoggedIn.TypeInSearchField(secondSpoilerTitle);

            Assert.Multiple(() =>
            {
                //assert the spoiler with second spoiler title is present
                Assert.That(homePage_LoggedIn.GetCountCards(), Is.EqualTo(1), $"Only one card should be displayed with the specified title: {secondSpoilerTitle}. Current count cards: {homePage_LoggedIn.GetCountCards()}");

                var totalTitles = homePage_LoggedIn.TitleAvailableCards().ToArray();
                var titles = string.Join(", ", totalTitles);

                Assert.That(titles, Does.Not.Contain(firstSpoilerTitle), $"Only one title should be available in the list: {secondSpoilerTitle}. Current present titles: {titles}");


            });
        }
        [Test, Order(12)]
        //BUG
        public void VerifySearchWithNonExistingSpoiler()
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\StorySpoilAppTests"));

            var testDataFolderPath = Path.Combine(projectDirectory, "TestData");

            var testData_CardsFilePath = Path.Combine(testDataFolderPath, "testData_Cards.json");

            var testData_Cards = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(testData_CardsFilePath));

            string invalidSpoiler = (string)testData_Cards.FirstTitle + "1";

            //type in search field
            homePage_LoggedIn.TypeInSearchField(invalidSpoiler);

            Assert.Multiple(() =>
            {
                //assert the spoiler with first spoiler title is NOT present
                Assert.That(homePage_LoggedIn.GetCountCards(), Is.EqualTo(0), $"Not a single card should be displayed when searching for NON existent card. Current count cards: {homePage_LoggedIn.GetCountCards()}");

                Assert.That(homePage_LoggedIn.GetTitleCard(), Is.EqualTo("There are no spoilers :("), $"Message for not found spolers does not match expected one. Current message: {homePage_LoggedIn.GetTitleCard()}");

                int countCards = homePage_LoggedIn.GetCountCards();

                while (countCards != 0)
                {
                    homePage_LoggedIn.ClickDeleteBtn();
                    countCards--;
                }

                Assert.That(homePage_LoggedIn.GetCountCards(), Is.EqualTo(0), $"Not All cards are deleted. Current count cards: {homePage_LoggedIn.GetCountCards()}");
            });



        }


        //ADDED SPOILERS SECTION 
        [Test, Order(13)]
        //BUG
        public void VerifyAddedSpoilersSection_WithOneSpoiler()
        {
            homePage_LoggedIn.ClickWriteSpoilerBtn();

            LastSpoilerTitle = $"Title: {GenerateRandomNumber()}";
            LastSpoilerDescription = $"Description: {GenerateRandomNumber()}";

            //create spoiler
            createSpoilerPage.CreateSpoiler_RequiredFields(LastSpoilerTitle, LastSpoilerDescription);

            Assert.That(homePage_LoggedIn.IsPageDisplayed(), Is.True, $"Home page for logged in users is not displayed after spoiler creation. Current page url: {driver.Url}");

            Assert.Multiple(() =>
            {
                //Assert.That(homePage_LoggedIn.AreSpoilerCardsDisplayed(), Is.True);
                Assert.That(homePage_LoggedIn.IsTitleDisplayed(), Is.True);
                Assert.That(homePage_LoggedIn.IsDescriptionDisplayed(), Is.True);
                Assert.That(homePage_LoggedIn.IsImageDisplayed(), Is.True);
                Assert.That(homePage_LoggedIn.IsEditBtnDisplayed(), Is.True);
                Assert.That(homePage_LoggedIn.IsShareBtnDisplayed(), Is.True, "Share button is not displayed on spoiler card.");
                Assert.That(homePage_LoggedIn.IsDeleteBtnDisplayed(), Is.True);
            });

            //assert title and desc match created!
            Assert.Multiple(() =>
            {
                Assert.That(homePage_LoggedIn.GetTitleCard(), Is.EqualTo(LastSpoilerTitle));
                Assert.That(homePage_LoggedIn.GetDescriptionCard(), Is.EqualTo(LastSpoilerDescription));
            });

        }
        [Test, Order(14)]
        public void VerifyEditBtnOnCard_EditSpoilerPageIsDisplayed()
        {
            //click link
            homePage_LoggedIn.ClickEditBtn();

            //assert page is displayed
            Assert.That(editSpoilerPage.IsPageDisplayed(), Is.True, $"Edit button on card does not redirect to Edit Spolier page. Current page url: {driver.Url}");
        }
        [Test, Order(15)]
        public void VerifyDeletionOfCard_With_OneAvailableCard()
        {
            string noSpoilersMsg = "No Spoilers Yet!";

            homePage_LoggedIn.ClickDeleteBtn();

            Assert.Multiple(() =>
            {
                Assert.That(homePage_LoggedIn.GetCountCards(), Is.EqualTo(0), $"Card is not deleted after delete button is clicked. Current count cards: {homePage_LoggedIn.GetCountCards()}");
                Assert.That(homePage_LoggedIn.GetTitleCard(), Does.Contain(noSpoilersMsg));
                Assert.That(homePage_LoggedIn.IsPageDisplayed(), Is.True, $"Home page for logged in users is not displayed after spoiler deletion.Current page url: {driver.Url}");
            });
        }
        [Test, Order(16)]
        public void VerifyDeletionOfAllPresentCards()
        {
            //CREATE 2 SPOILERS
            Create2Spoilers();

            int countCards = homePage_LoggedIn.GetCountCards();
            string noSpoilersMsg = "No Spoilers Yet!";

            //delete cards until count is 0
            while (countCards != 0)
            {
                homePage_LoggedIn.ClickDeleteBtn();
                countCards--;
            }

            Assert.Multiple(() =>
            {
                Assert.That(homePage_LoggedIn.GetCountCards(), Is.EqualTo(0), $"Not All cards are deleted. Current count cards: {homePage_LoggedIn.GetCountCards()}");

                Assert.That(homePage_LoggedIn.GetTitleCard(), Does.Contain(noSpoilersMsg), "There are still left cards after deletion");

                Assert.That(homePage_LoggedIn.IsPageDisplayed(), Is.True, $"Home page for logged in users is not displayed after spoilers deletion.Current page url: {driver.Url}");
            });

        }
        [Test, Order(17)]
        //BUG 
        public void VerifyCopyrightLink_CopyrightPageIsDisplayed()
        {
            //click link
            homePage_LoggedIn.ClickCopyrightLink();

            //verify user is not on the same page 
            Assert.That(driver.Url, Does.Contain("/Copyright"), $"Copyright link does not redirect to Copyright page. Current page url: {driver.Url}");
        }
    }
}
