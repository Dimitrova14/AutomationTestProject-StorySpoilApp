using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using System.Text.Json.Nodes;

namespace StorySpoilAppTests.API_Tests
{
    [TestFixture]
    public class StorySpoilApi_PositiveTests : IDisposable
    {
        private RestClient client;
        private string token;
        private string LastStoryTitle;
        private string LastStoryDescription;
        private string Username;
        private string Password;

        [SetUp]
        public void Setup()
        {
            var testData_Login = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testData_Login.json")));

            Username = testData_Login.Username;
            Password = testData_Login.Password;

            client = new RestClient(GlobalConstants.BaseUrl);
            token = GlobalConstants.AuthenticateUser(Username, Password);

            Assert.That(token, Is.Not.Null.Or.Empty, "Authentication token is null or empty");
        }
        public void Dispose()
        {
            client?.Dispose();
        }

        [Test, Order(1)]
        public void CreateSpoiler()
        {
            //create spoiler
            LastStoryTitle = $"Title: {GlobalConstants.GenerateRandomNumber()}";
            LastStoryDescription = $"Description: {GlobalConstants.GenerateRandomNumber()}";

            var createSpoilerRequest = new RestRequest("/Story/Create", Method.Post);
            createSpoilerRequest.AddHeader("Authorization", $"Bearer {token}");
            createSpoilerRequest.AddJsonBody(new
            {
                title = LastStoryTitle,
                description = LastStoryDescription
            });

            var createSpoilerResponse = client.Execute(createSpoilerRequest);

            Assert.Multiple(() =>
            {
                Assert.That(createSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created), "Spoiler is not created");
                Assert.That(createSpoilerResponse.Content, Is.Not.Empty.Or.Null, "Response should not be null or empty");
            });

            var jtokenResponse = JToken.Parse(createSpoilerResponse.Content);

            Assert.That(jtokenResponse.Type, Is.EqualTo(JTokenType.Object), "Response body should be Object");

            var jsonBody = (JObject)jtokenResponse;

            Assert.Multiple(() =>
            {
                Assert.That(jsonBody.ContainsKey("msg"), Is.True, "Msg property should exist");
                Assert.That(jsonBody.ContainsKey("storyId"), Is.True, "storyId property should exist");

                //assert type
                Assert.That(jsonBody["msg"].Type, Is.EqualTo(JTokenType.String), "Msg property should be of type 'String'");
                Assert.That(jsonBody["storyId"].Type, Is.EqualTo(JTokenType.String), "storyId property should be of type 'String'");

                //assert text macthes created
                Assert.That(jsonBody["msg"].ToString(), Is.EqualTo("Successfully created!"), "Message for created spoiler does not match expected");
                Assert.That(jsonBody["storyId"].ToString(), Is.Not.Null.Or.Empty, "storyId property is null or empty");
            });

            string spoilerId = jsonBody["storyId"].ToString();

            File.WriteAllText(
                "testData_Spoiler.json", $"{{\"Title\": \"{LastStoryTitle}\", \"Description\":\"{LastStoryDescription}\",\"Id\":\"{spoilerId}\"}}");

        }

        [Test, Order(2)]
        public void SearchForExistentSpoiler()
        {
            var testData_Spoiler = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("testData_Spoiler.json"));

            string searchTitle = (string)testData_Spoiler.Title;
            string spoilerDesc = (string)testData_Spoiler.Description;
            string spoilerId = (string)testData_Spoiler.Id;

            var searchSpoilerRequest = new RestRequest($"/Story/Search", Method.Get);
            searchSpoilerRequest.AddQueryParameter("keyword", $"{searchTitle}");
            searchSpoilerRequest.AddHeader("Authorization", $"Bearer {token}");

            var searchSpoilerResponse = client.Execute(searchSpoilerRequest);

            Assert.Multiple(() =>
            {
                Assert.That(searchSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "No spoiler is found");
                Assert.That(searchSpoilerResponse.Content, Is.Not.Empty.Or.Null, "Response should not be null or empty");
            });

            var jtokenResponse = JToken.Parse(searchSpoilerResponse.Content);

            Assert.That(jtokenResponse.Type, Is.EqualTo(JTokenType.Array), "Response body should be Array");
            
            var jsonBody = (JArray)jtokenResponse;

            Assert.That(jsonBody.Count, Is.GreaterThan(0), "Response body should not be empty");

            var foundSpoiler = (JObject)jsonBody.First();

            Assert.Multiple(() =>
            {
                Assert.That(foundSpoiler.ContainsKey("id"), Is.True, "Id property should exist");
                Assert.That(foundSpoiler.ContainsKey("title"), Is.True, "Title property should exist");
                Assert.That(foundSpoiler.ContainsKey("description"), Is.True, "Description property should exist");
                Assert.That(foundSpoiler.ContainsKey("url"), Is.True, "Url property should exist");
            });

            Assert.Multiple(() =>
            {
                //assert type
                Assert.That(foundSpoiler["id"].Type, Is.EqualTo(JTokenType.String), "Id property should be of type 'String'");

                Assert.That(foundSpoiler["title"].Type, Is.EqualTo(JTokenType.String), "Title property should be of type 'String'");

                Assert.That(foundSpoiler["description"].Type, Is.EqualTo(JTokenType.String), "Description property should be of type 'String'");
            });
            
            Assert.Multiple(() =>
            {
                //assert text matches created
                Assert.That(foundSpoiler["id"].ToString(), Is.EqualTo(spoilerId), "Id of the found spoiler does not match the Id of the created spoiler'");

                Assert.That(foundSpoiler["title"].ToString(), Is.EqualTo(searchTitle), "Title of the found spoiler does not match the Title of the created spoiler'");

                Assert.That(foundSpoiler["description"].ToString(), Is.EqualTo(spoilerDesc), "Description of the found spoiler does not match the Description of the created spoiler'");
            });

        }

        [Test, Order(3)]
        public void EditCreatedSpoiler()
        {
            //update created story by id 
            LastStoryTitle = $"UpdatedTitle_{GlobalConstants.GenerateRandomNumber()}";
            LastStoryDescription = $"UpdatedDesc_{GlobalConstants.GenerateRandomNumber()}";

            var testData_Spoiler = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("testData_Spoiler.json"));

            string spoilerId = (string)testData_Spoiler.Id;

            var editCreatedSpoilerRequest = new RestRequest($"/Story/Edit/{spoilerId}", Method.Put);

            editCreatedSpoilerRequest.AddHeader("Authorization", $"Bearer {token}");
            editCreatedSpoilerRequest.AddJsonBody(new
            {
                Title = LastStoryTitle,
                Description = LastStoryDescription,
            });

            var editCreatedSpoilerResponse = client.Execute(editCreatedSpoilerRequest);

            Assert.Multiple(() =>
            {
                Assert.That(editCreatedSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Spoiler is not edited");
                Assert.That(editCreatedSpoilerResponse.Content, Is.Not.Empty.Or.Null, "Response body should not be null or empty");
            });

            var jtokenResponse = JToken.Parse(editCreatedSpoilerResponse.Content);

            Assert.That(jtokenResponse.Type, Is.EqualTo(JTokenType.Object), "Response body should be Object");

            var editedSpoiler = (JObject)jtokenResponse;

            Assert.Multiple(() =>
            {
                //assert properties exist
                Assert.That(editedSpoiler.ContainsKey("msg"), Is.True, "Msg property should exist");
                Assert.That(editedSpoiler.ContainsKey("storyId"), Is.True, "storyId property should exist");

                //assert type of properties
                Assert.That(editedSpoiler["msg"].Type, Is.EqualTo(JTokenType.String), "Msg property should be of type 'String'");
                Assert.That(editedSpoiler["storyId"].Type, Is.EqualTo(JTokenType.String), "storyId property should be of type 'String'");

                //assert text on  properties
                Assert.That(editedSpoiler["msg"].ToString(), Is.EqualTo("Successfully edited"), "Message for edited spoiler does not match expected");
                Assert.That(editedSpoiler["storyId"].ToString(), Is.EqualTo(spoilerId), "Id for edited spoiler does not match initial Id");

            });

            testData_Spoiler.Id = editedSpoiler["storyId"].ToString();
            testData_Spoiler.Title = LastStoryTitle;
            testData_Spoiler.Description = LastStoryDescription;

            File.WriteAllText("testData_Spoiler.json", JsonConvert.SerializeObject(testData_Spoiler, Formatting.Indented));
        }

        [Test, Order(4)]
        public void Create2Spoilers()
        {
            //create 1 spoiler
            string firstStoryTitle = $"Title: {GlobalConstants.GenerateRandomNumber()}";
            string firstStoryDesc = $"Description: {GlobalConstants.GenerateRandomNumber()}";

            var createFirstSpoilerRequest = new RestRequest("/Story/Create", Method.Post);
            createFirstSpoilerRequest.AddHeader("Authorization", $"Bearer {token}");
            createFirstSpoilerRequest.AddJsonBody(new
            {
                title = firstStoryTitle,
                description = firstStoryDesc
            });

            var createFirstSpoilerResponse = client.Execute(createFirstSpoilerRequest);

            Assert.Multiple(() =>
            {
                Assert.That(createFirstSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created), "Spoiler is not created");
                Assert.That(createFirstSpoilerResponse.Content, Is.Not.Empty.Or.Null, "Response should not be null or empty");
            });

            //create 2 spoiler
            string secondStoryTitle = $"Title: {GlobalConstants.GenerateRandomNumber()}";
            string secondStoryDesc = $"Description: {GlobalConstants.GenerateRandomNumber()}";

            var createSecondSpoilerRequest = new RestRequest("/Story/Create", Method.Post);
            createSecondSpoilerRequest.AddHeader("Authorization", $"Bearer {token}");
            createSecondSpoilerRequest.AddJsonBody(new
            {
                title = secondStoryTitle,
                description = secondStoryDesc
            });

            var createSecondSpoilerResponse = client.Execute(createSecondSpoilerRequest);

            Assert.That(createSecondSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created), "Spoiler is not created successfully");

            Assert.Multiple(() =>
            {
                Assert.That(createSecondSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created), "Spoiler is not created");
                Assert.That(createSecondSpoilerResponse.Content, Is.Not.Empty.Or.Null, "Response should not be null or empty");
            });

            File.WriteAllText(
                "testData_2Spoilers.json", $"{{\"FirstTitle\": \"{firstStoryTitle}\", \"FirstDesc\":\"{firstStoryDesc}\", \"SecondTitle\": \"{secondStoryTitle}\", \"SecondDesc\":\"{secondStoryDesc}\"}}");
        }

        [Test, Order(5)]
        public void GetAllSpoilers()
        {
            var testData_UpdatedSpoiler = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("testData_Spoiler.json"));
            var testData_Spoilers = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("testData_2Spoilers.json"));

            string firstTitle = (string)testData_Spoilers.FirstTitle;
            string firstDesc = (string)testData_Spoilers.FirstDesc;

            string secondTitle = (string)testData_Spoilers.SecondTitle;
            string secondDesc = (string)testData_Spoilers.SecondDesc;

            string updatedTitle = (string)testData_UpdatedSpoiler.Title;
            string updatedDesc = (string)testData_UpdatedSpoiler.Description;

            //get all
            var getAllSpoilersRequest = new RestRequest("/Story/All", Method.Get);
            getAllSpoilersRequest.AddHeader("Authorization", $"Bearer {token}");

            var getAllSpoilersResponse = client.Execute(getAllSpoilersRequest);

            Assert.Multiple(() =>
            {
                Assert.That(getAllSpoilersResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK");
                Assert.That(getAllSpoilersResponse.Content, Is.Not.Empty.Or.Null, "No spoilers are found");
            });

            var jtokenResponse = JToken.Parse(getAllSpoilersResponse.Content);

            Assert.That(jtokenResponse.Type, Is.EqualTo(JTokenType.Array), "Response body should be Array");

            var spoilers = (JArray)jtokenResponse;

            Assert.That(spoilers.Count, Is.GreaterThanOrEqualTo(2), $"Array should contain 2 or more spoilers. Current count: {spoilers.Count}");

            foreach (var spoiler in spoilers)
            {
                var spoilerObj = (JObject)spoiler;

                Assert.Multiple(() =>
                {
                    //assert properties exist
                    Assert.That(spoilerObj.ContainsKey("id"), Is.True, "Id property should exist");

                    Assert.That(spoilerObj.ContainsKey("title"), Is.True, "Title property should exist");

                    Assert.That(spoilerObj.ContainsKey("description"), Is.True, "Description property should exist");

                    //assert type
                    Assert.That(spoiler["id"].ToString(), Is.Not.Empty.Or.Null, "Id property should not be null or empty");

                    Assert.That(spoiler["title"].ToString(), Is.Not.Empty.Or.Null, "Title property should not be null or empty");

                    Assert.That(spoiler["description"].ToString(), Is.Not.Empty.Or.Null, "Description property should not be null or empty");
                });
            }

            var firstCreatedSpoiler = spoilers.FirstOrDefault(s => s["title"].ToString() == firstTitle);

            var secondCreatedSpoiler = spoilers.FirstOrDefault(s => s["title"].ToString() == secondTitle);

            var editedSpoiler = spoilers.FirstOrDefault(s => s["title"].ToString() == updatedTitle);

            //assert not null
            Assert.Multiple(() =>
            {
                Assert.That(firstCreatedSpoiler, Is.Not.Null, $"Spoiler with title: '{firstTitle}' is not found");
                Assert.That(secondCreatedSpoiler, Is.Not.Null, $"Spoiler with title: '{secondTitle}' is not found");
                Assert.That(editedSpoiler, Is.Not.Null, $"Spoiler with title: '{updatedTitle}' is not found");
            });

            Assert.Multiple(() =>
            {
                Assert.That(firstCreatedSpoiler["description"].ToString(), Is.EqualTo(firstDesc), $"Descripton of specified spoiler: '{firstTitle}' does not match expected.");

                Assert.That(secondCreatedSpoiler["description"].ToString(), Is.EqualTo(secondDesc), $"Descripton of specified spoiler: '{secondTitle}' does not match expected.");

                Assert.That(editedSpoiler["description"].ToString(), Is.EqualTo(updatedDesc), $"Descripton of specified spoiler: '{updatedTitle}' does not match expected.");

            });

        }

        [Test, Order(5)]
        public void DeleteCreatedSpoiler()
        {
            var testData_Spoiler = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("testData_Spoiler.json"));

            string spoilerId = (string)testData_Spoiler.Id;
            string spoilerTitle = (string)testData_Spoiler.Title;

            var deleteSpoilerRequest = new RestRequest($"/Story/Delete/{spoilerId}", Method.Delete);
            deleteSpoilerRequest.AddHeader("Authorization", $"Bearer {token}");

            var deleteSpoilerResponse = client.Execute(deleteSpoilerRequest);

            Assert.Multiple(() =>
            {
                Assert.That(deleteSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Spoiler is not deleted");
                Assert.That(deleteSpoilerResponse.Content, Is.Not.Empty.Or.Null, "Response body is null or empty");
            });

            var jtokenResponse = JToken.Parse(deleteSpoilerResponse.Content);

            Assert.That(jtokenResponse.Type, Is.EqualTo(JTokenType.Object), "Response body should be Array");

            var deletedResponse = (JObject)jtokenResponse;

            Assert.Multiple(() =>
            {
                Assert.That(deletedResponse.ContainsKey("msg"), Is.True, "Msg property should exist");
                Assert.That(deletedResponse["msg"].Type, Is.EqualTo(JTokenType.String), "Msg property should be of type 'String'");
                Assert.That(deletedResponse["msg"].ToString(), Is.EqualTo("Deleted successfully!"), "Message for deleted spoiler does not match expected");
            });

            //get deleted spoiler
            var getDeletedSpoilerRequest = new RestRequest("/Story/Search", Method.Get);
            getDeletedSpoilerRequest.AddQueryParameter("keyword", $"{spoilerTitle}");
            getDeletedSpoilerRequest.AddHeader("Authorization", $"Bearer {token}");

            var getDeletedSpoilerResponse = client.Execute(getDeletedSpoilerRequest);

            Assert.That(getDeletedSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "Status code should be 'Not Found -> 404' when retrieving deleted spoiler");

            var deletedSpoiler = JObject.Parse(getDeletedSpoilerResponse.Content);

            Assert.Multiple(() =>
            {
                Assert.That(deletedSpoiler.ContainsKey("msg"), Is.True, "Msg property should exist");
                Assert.That(deletedSpoiler["msg"].Type, Is.EqualTo(JTokenType.String), "Msg property should be of type 'String'");
                Assert.That(deletedSpoiler["msg"].ToString(), Is.EqualTo("No spoilers..."), "Message for not found spoiler does not match expected");
            });
        }

        [Test, Order(6)]
        public void DeleteCreatedSpoilers()
        {
            //get all spoilers
            var getAllSpoilersRequest = new RestRequest("/Story/All", Method.Get);
            getAllSpoilersRequest.AddHeader("Authorization", $"Bearer {token}");

            var getAllSpoilersResponse = client.Execute(getAllSpoilersRequest);

            Assert.Multiple(() =>
            {
                Assert.That(getAllSpoilersResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK");
                Assert.That(getAllSpoilersResponse.Content, Is.Not.Empty.Or.Null, "No spoilers are found");
            });
            
            var spoilers = JArray.Parse(getAllSpoilersResponse.Content);

            //iterate in all spoilers
            foreach (var spoiler in spoilers)
            {
                var spoilerObj = (JObject)spoiler;
                string spoilerTitle = spoilerObj["title"].ToString();
                string spoilerId = spoilerObj["id"].ToString();

                if(spoilerTitle.Contains("Title:"))
                {
                    //delete all 3
                    var deleteSpoilerRequest = new RestRequest($"/Story/Delete/{spoilerId}", Method.Delete);
                    deleteSpoilerRequest.AddHeader("Authorization", $"Bearer {token}");

                    var deleteSpoilerResponse = client.Execute(deleteSpoilerRequest);

                    Assert.That(deleteSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Spoiler is not deleted successfully");
                }
            }

            //verify spoilers are deleted
            var verifyDeletedSpoilersRequest = new RestRequest("/Story/All", Method.Get);
            verifyDeletedSpoilersRequest.AddHeader("Authorization", $"Bearer {token}");

            var verifyDeletedSpoilersResponse = client.Execute(verifyDeletedSpoilersRequest);

            Assert.Multiple(() =>
            {
                Assert.That(verifyDeletedSpoilersResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "Status code should be 'Not Found' when searching for deleted spoilers");
                Assert.That(verifyDeletedSpoilersResponse.Content, Is.Not.Empty.Or.Null, "Response body should not be empty");
            });

            var deletedResponse = JObject.Parse(verifyDeletedSpoilersResponse.Content);

            Assert.Multiple(() =>
            {
                Assert.That(deletedResponse.ContainsKey("msg"), Is.True, "Msg property should exist");
                Assert.That(deletedResponse["msg"].Type, Is.EqualTo(JTokenType.String), "Msg property should be of type 'String'");
                Assert.That(deletedResponse["msg"].ToString(), Is.EqualTo("No spoilers..."), "Message for not found spoilers does not match expected");
            });
        }
    }
}
