namespace StorySpoilAppTests.Tests.AuthenticatedUser_Tests
{
    public class HomePageTests_LoggedIn : BaseTest
    {
        //Test Cases

        //1. Verify navbar links redirects to right pages 
        [Test]
        public void CheckNavBarSection()
        {
            homePage.OpenPage();
            var navBarLinks = homePage.NavBarLinks;

            //Assert.Multiple(() =>
            //{
            //    foreach (var link in navBarLinks)
            //    {
            //        link = basePage.F
            //    }
            //})

            homePage.
        }
    }
}

// Assert all elements using Assert.Multiple
//Assert.Multiple(() =>
//{
//    foreach (var link in navBarLinks)
//    {
//        Assert.IsTrue(link.Value.Displayed, $"{link.Key} is not displayed.");
//    }
//});
