using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace StorySpoilAppTests.API_Tests.NegativePath
{
    public class StorySpoilApi_NegativeTests : IDisposable
    {
        private RestClient client;
        private string token;
        private string LastStoryTitle;
        private string LastStoryDescription;
        private string Username;
        private string Password;
        public void Dispose()
        {
            client?.Dispose();
        }

        [SetUp]
        public void Setup()
        {
            var testData_Login = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("testData_Login.json"));

            Username = testData_Login.Username;
            Password = testData_Login.Password;

            client = new RestClient(GlobalConstants.BaseUrl);
            token = GlobalConstants.AuthenticateUser(Username, Password);

            Assert.That(token, Is.Not.Null.Or.Empty, "Authentication token is null or empty");
        }

        [Test, Order(1)]
        //get all with no auth - DONE
        public void GetAllSpoilers_NoAuth()
        {
            var getAllSpoilersRequest = new RestRequest("/Story/All", Method.Get);

            var getAllSpoilersResponse = client.Execute(getAllSpoilersRequest);

            Assert.Multiple(() =>
            {
                Assert.That(getAllSpoilersResponse.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized), "Status code should be 'Unauthorized' when retrieving All spoilers without authentication");
                Assert.That(getAllSpoilersResponse.Content, Is.Empty, "Response body should be empty");
            });
        }

        [Test, Order(2)]
        //create spoiler with no auth - DONE
        public void CreateSpoiler_NoAuth()
        {
            LastStoryTitle = $"Title: {GlobalConstants.GenerateRandomNumber()}";
            LastStoryDescription = $"Description: {GlobalConstants.GenerateRandomNumber()}";

            var createSpoilerRequest = new RestRequest("/Story/Create", Method.Post);
            createSpoilerRequest.AddJsonBody(new
            {
                title = LastStoryTitle,
                description = LastStoryDescription
            });

            var createSpoilerResponse = client.Execute(createSpoilerRequest);

            Assert.Multiple(() =>
            {
                Assert.That(createSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized), "Status code should be 'Unauthorized' when Creating spoiler without authentication");
                Assert.That(createSpoilerResponse.Content, Is.Empty, "Response body should be empty");
            });
        }

        [Test, Order(3)]
        //create spoiler without title - DONE
        public void CreateSpoilerWithoutTitle()
        {
            LastStoryTitle = "";
            LastStoryDescription = "Test";

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
                Assert.That(createSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest), "Status code should be 'BadRequest -> 400' when Creating spoiler without title");
                Assert.That(createSpoilerResponse.Content, Is.Not.Null.Or.Empty, "Response body should not be null or empty");
            });

            var errorResponse = JObject.Parse(createSpoilerResponse.Content);

            Assert.Multiple(() =>
            {
                Assert.That(errorResponse.ContainsKey("errors"), Is.True, "Errors property should exist");
                Assert.That(errorResponse.ContainsKey("title"), Is.True, "Title property should exist");
                Assert.That(errorResponse.ContainsKey("status"), Is.True, "Status property should exist");
                Assert.That(errorResponse.ContainsKey("traceId"), Is.True, "TraceId property should exist");
            });

            //assert type
            Assert.Multiple(() =>
            {
                Assert.That(errorResponse["errors"].Type, Is.EqualTo(JTokenType.Object), "Errors property should be Object");

                Assert.That(errorResponse["errors"]["Title"].Type, Is.EqualTo(JTokenType.Array), "Title property should be Array");

                Assert.That(errorResponse["title"].Type, Is.EqualTo(JTokenType.String), "title property should be String");

                Assert.That(errorResponse["status"].Type, Is.EqualTo(JTokenType.Integer), "Status property should be Integer");

                Assert.That(errorResponse["traceId"].Type, Is.EqualTo(JTokenType.String), "TraceId property should be String");
            });

            var titleArray = errorResponse["errors"]["Title"].ToArray();
            List<string> titleProperty = titleArray.Select(msg => msg.ToString()).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(titleProperty, Does.Contain("The Title field is required."), "Message for required Title should be present in array");

                Assert.That(titleProperty, Does.Contain("Title must be at least 2 symbols."), "Message for MIN length for Title should be present in array");

                Assert.That(errorResponse["title"].ToString(), Does.Contain("One or more validation errors occurred."), "Validation errors message should be present");

                Assert.That(errorResponse["status"].Value<int>, Is.EqualTo(400), "Status property should be 400");

                Assert.That(errorResponse["traceId"].ToString(), Is.Not.Null.Or.Empty, "traceId property should not be null or empty");
            });

        }

        [Test, Order(4)]
        //create spoiler without desc - DONE
        public void CreateSpoilerWithoutDescrption()
        {
            LastStoryTitle = "Test";
            LastStoryDescription = "";

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
                Assert.That(createSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest), "Status code should be 'BadRequest -> 400' when Creating spoiler without description");
                Assert.That(createSpoilerResponse.Content, Is.Not.Null.Or.Empty, "Response body should not be null or empty");
            });

            var errorResponse = JObject.Parse(createSpoilerResponse.Content);

            Assert.Multiple(() =>
            {
                Assert.That(errorResponse.ContainsKey("errors"), Is.True, "Errors property should exist");
                Assert.That(errorResponse.ContainsKey("title"), Is.True, "Title property should exist");
                Assert.That(errorResponse.ContainsKey("status"), Is.True, "Status property should exist");
                Assert.That(errorResponse.ContainsKey("traceId"), Is.True, "TraceId property should exist");
            });

            //assert type
            Assert.Multiple(() =>
            {
                Assert.That(errorResponse["errors"].Type, Is.EqualTo(JTokenType.Object), "Errors property should be Object");

                Assert.That(errorResponse["errors"]["Description"].Type, Is.EqualTo(JTokenType.Array), "Description property should be Array");

                Assert.That(errorResponse["title"].Type, Is.EqualTo(JTokenType.String), "title property should be String");

                Assert.That(errorResponse["status"].Type, Is.EqualTo(JTokenType.Integer), "Status property should be Integer");

                Assert.That(errorResponse["traceId"].Type, Is.EqualTo(JTokenType.String), "TraceId property should be String");
            });

            var descArray = errorResponse["errors"]["Description"].ToArray();
            List<string> descProperty = descArray.Select(msg => msg.ToString()).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(descProperty, Does.Contain("The Description field is required."), "Message for required Description should be present in array");

                Assert.That(descProperty, Does.Contain("Desctiption must be at least 3 symbols."), "Message for MIN length for Desctiption should be present in array");

                Assert.That(errorResponse["title"].ToString(), Does.Contain("One or more validation errors occurred."), "Validation errors message should be present");

                Assert.That(errorResponse["status"].Value<int>, Is.EqualTo(400), "Status property should be 400");

                Assert.That(errorResponse["traceId"].ToString(), Is.Not.Null.Or.Empty, "traceId property should not be null or empty");
            });


        }

        [Test, Order(5)]
        //create spoiler without title & desc - DONE
        public void CreateSpoiler_PassEmptyForm()
        {
            LastStoryTitle = "";
            LastStoryDescription = "";

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
                Assert.That(createSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest), "Status code should be 'BadRequest -> 400' when Creating spoiler without description");
                Assert.That(createSpoilerResponse.Content, Is.Not.Null.Or.Empty, "Response body should not be null or empty");
            });

            var errorResponse = JObject.Parse(createSpoilerResponse.Content);

            Assert.Multiple(() =>
            {
                Assert.That(errorResponse.ContainsKey("errors"), Is.True, "Errors property should exist");
                Assert.That(errorResponse.ContainsKey("title"), Is.True, "Title property should exist");
                Assert.That(errorResponse.ContainsKey("status"), Is.True, "Status property should exist");
                Assert.That(errorResponse.ContainsKey("traceId"), Is.True, "TraceId property should exist");
            });

            //assert type
            Assert.Multiple(() =>
            {
                Assert.That(errorResponse["errors"].Type, Is.EqualTo(JTokenType.Object), "Errors property should be Object");

                Assert.That(errorResponse["errors"]["Title"].Type, Is.EqualTo(JTokenType.Array), "Title property should be Array");

                Assert.That(errorResponse["errors"]["Description"].Type, Is.EqualTo(JTokenType.Array), "Description property should be Array");

                Assert.That(errorResponse["title"].Type, Is.EqualTo(JTokenType.String), "title property should be String");

                Assert.That(errorResponse["status"].Type, Is.EqualTo(JTokenType.Integer), "Status property should be Integer");

                Assert.That(errorResponse["traceId"].Type, Is.EqualTo(JTokenType.String), "TraceId property should be String");
            });

            var titleArray = errorResponse["errors"]["Title"].ToArray();
            List<string> titleProperty = titleArray.Select(msg => msg.ToString()).ToList();

            var descArray = errorResponse["errors"]["Description"].ToArray();
            List<string> descProperty = descArray.Select(msg => msg.ToString()).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(titleProperty, Does.Contain("The Title field is required."), "Message for required Title should be present in array");

                Assert.That(titleProperty, Does.Contain("Title must be at least 2 symbols."), "Message for MIN length for Title should be present in array");

                Assert.That(descProperty, Does.Contain("The Description field is required."), "Message for required Description should be present in array");

                Assert.That(descProperty, Does.Contain("Desctiption must be at least 3 symbols."), "Message for MIN length for Desctiption should be present in array");
            });

            Assert.Multiple(() =>
            {
                Assert.That(errorResponse["title"].ToString(), Does.Contain("One or more validation errors occurred."), "Validation errors message should be present");

                Assert.That(errorResponse["status"].Value<int>, Is.EqualTo(400), "Status property should be 400");

                Assert.That(errorResponse["traceId"].ToString(), Is.Not.Null.Or.Empty, "traceId property should not be null or empty");
            });


        }

        [Test, Order(6)]
        //create spoiler
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

        [Test, Order(7)]
        //search with no auth - DONE 
        public void SearchForExistentSpoiler_NoAuth()
        {
            var testData_Spoiler = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("testData_Spoiler.json"));

            string searchTitle = (string)testData_Spoiler.Title;
            
            //search for the spoiler
            var searchSpoilerRequest = new RestRequest($"/Story/Search", Method.Get);
            searchSpoilerRequest.AddQueryParameter("keyword", $"{searchTitle}");

            var searchSpoilerResponse = client.Execute(searchSpoilerRequest);

            Assert.Multiple(() =>
            {
                Assert.That(searchSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized), "Status code should be 'Unauthorized' when searching for spoiler without authentication");
                Assert.That(searchSpoilerResponse.Content, Is.Empty, "Response body should be empty");
            });
        }

        [Test, Order(8)]
        //search for non existent - DONE
        public void SearchForNonExistentSpoiler_WithAuth()
        {
            var testData_Spoiler = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("testData_Spoiler.json"));

            string searchTitle = (string)testData_Spoiler.Title + "1";

            var searchSpoilerRequest = new RestRequest($"/Story/Search", Method.Get);
            searchSpoilerRequest.AddQueryParameter("keyword", $"{searchTitle}");
            searchSpoilerRequest.AddHeader("Authorization", $"Bearer {token}");

            var searchSpoilerResponse = client.Execute(searchSpoilerRequest);

            Assert.Multiple(() =>
            {
                Assert.That(searchSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "Status code should be 'NotFound -> 404' when searching for non existent spoiler");
                Assert.That(searchSpoilerResponse.Content, Is.Not.Null.Or.Empty, "Response body should not be null or empty");
            });

            var responseBody = JObject.Parse(searchSpoilerResponse.Content);

            Assert.Multiple(() =>
            {
                Assert.That(responseBody.ContainsKey("msg"), Is.True, "Msg property should exist");
                Assert.That(responseBody["msg"].Type, Is.EqualTo(JTokenType.String), "Msg property should be of type 'String'");
                Assert.That(responseBody["msg"].ToString(), Is.EqualTo("No spoilers..."), "Message for not found spoiler does not match expected");
            });
        }

        [Test, Order(9)]
        //edit spoiler with no auth - DONE
        public void EditCreatedSpoiler_NoAuth()
        {
            var testData_Spoiler = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("testData_Spoiler.json"));

            string spoilerId = (string)testData_Spoiler.Id;

            LastStoryTitle = $"UpdatedTitle_{GlobalConstants.GenerateRandomNumber()}";
            LastStoryDescription = $"UpdatedDesc_{GlobalConstants.GenerateRandomNumber()}";
            string storyId = TestData_StoryCreation.LastStoryId ;

            var editCreatedSpoilerRequest = new RestRequest($"/Story/Edit/{spoilerId}", Method.Put);
            editCreatedSpoilerRequest.AddJsonBody(new
            {
                Title = LastStoryTitle,
                Description = LastStoryDescription,
            });

            var editCreatedSpoilerResponse = client.Execute(editCreatedSpoilerRequest);

            Assert.Multiple(() =>
            {
                Assert.That(editCreatedSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized), "Status code should be 'Unauthorized' when editing existent spoiler without authentication");
                Assert.That(editCreatedSpoilerResponse.Content, Is.Empty, "Response body should be empty");
            });
        }

        [Test, Order(10)]
        //edit spoiler -> clear title - DONE
        public void EditCreatedSpoiler_WithoutTitle()
        {
            var testData_Spoiler = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("testData_Spoiler.json"));

            string spoilerId = (string)testData_Spoiler.Id;

            LastStoryTitle = "";
            LastStoryDescription = $"UpdatedDesc_{GlobalConstants.GenerateRandomNumber()}";

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
                Assert.That(editCreatedSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest), "Status code should be 'BadRequest -> 400' when editing spoiler by removing title");
                Assert.That(editCreatedSpoilerResponse.Content, Is.Not.Null.Or.Empty, "Response body should not be null or empty");
            });

            var errorResponse = JObject.Parse(editCreatedSpoilerResponse.Content);

            Assert.Multiple(() =>
            {
                Assert.That(errorResponse.ContainsKey("errors"), Is.True, "Errors property should exist");
                Assert.That(errorResponse.ContainsKey("title"), Is.True, "Title property should exist");
                Assert.That(errorResponse.ContainsKey("status"), Is.True, "Status property should exist");
                Assert.That(errorResponse.ContainsKey("traceId"), Is.True, "TraceId property should exist");
            });

            //assert type
            Assert.Multiple(() =>
            {
                Assert.That(errorResponse["errors"].Type, Is.EqualTo(JTokenType.Object), "Errors property should be Object");

                Assert.That(errorResponse["errors"]["Title"].Type, Is.EqualTo(JTokenType.Array), "Title property should be Array");

                Assert.That(errorResponse["title"].Type, Is.EqualTo(JTokenType.String), "title property should be String");

                Assert.That(errorResponse["status"].Type, Is.EqualTo(JTokenType.Integer), "Status property should be Integer");

                Assert.That(errorResponse["traceId"].Type, Is.EqualTo(JTokenType.String), "TraceId property should be String");
            });

            var titleArray = errorResponse["errors"]["Title"].ToArray();
            List<string> titleProperty = titleArray.Select(msg => msg.ToString()).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(titleProperty, Does.Contain("The Title field is required."), "Message for required Title should be present in array");

                Assert.That(titleProperty, Does.Contain("Title must be at least 2 symbols."), "Message for MIN length for Title should be present in array");

                Assert.That(errorResponse["title"].ToString(), Does.Contain("One or more validation errors occurred."), "Validation errors message should be present");

                Assert.That(errorResponse["status"].Value<int>, Is.EqualTo(400), "Status property should be 400");

                Assert.That(errorResponse["traceId"].ToString(), Is.Not.Null.Or.Empty, "traceId property should not be null or empty");
            });
        }

        [Test, Order(11)]
        //edit spoiler -> clear desc - DONE
        public void EditCreatedSpoiler_WithoutDescription()
        {
            var testData_Spoiler = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("testData_Spoiler.json"));

            string spoilerId = (string)testData_Spoiler.Id;

            LastStoryTitle = $"UpdatedTitle_{GlobalConstants.GenerateRandomNumber()}";
            LastStoryDescription = "";

            var editCreatedSpoilerRequest = new RestRequest($"/Story/Edit/{spoilerId}", Method.Put);
            editCreatedSpoilerRequest.AddHeader("Authorization", $"Bearer {token}");
            editCreatedSpoilerRequest.AddJsonBody(new
            {
                Title = LastStoryTitle,
                Description = LastStoryDescription,
            });

            var editCreatedSpoilerResponse = client.Execute(editCreatedSpoilerRequest);

            var errorResponse = JObject.Parse(editCreatedSpoilerResponse.Content);

            Assert.Multiple(() =>
            {
                Assert.That(errorResponse.ContainsKey("errors"), Is.True, "Errors property should exist");
                Assert.That(errorResponse.ContainsKey("title"), Is.True, "Title property should exist");
                Assert.That(errorResponse.ContainsKey("status"), Is.True, "Status property should exist");
                Assert.That(errorResponse.ContainsKey("traceId"), Is.True, "TraceId property should exist");
            });

            //assert type
            Assert.Multiple(() =>
            {
                Assert.That(errorResponse["errors"].Type, Is.EqualTo(JTokenType.Object), "Errors property should be Object");

                Assert.That(errorResponse["errors"]["Description"].Type, Is.EqualTo(JTokenType.Array), "Description property should be Array");

                Assert.That(errorResponse["title"].Type, Is.EqualTo(JTokenType.String), "title property should be String");

                Assert.That(errorResponse["status"].Type, Is.EqualTo(JTokenType.Integer), "Status property should be Integer");

                Assert.That(errorResponse["traceId"].Type, Is.EqualTo(JTokenType.String), "TraceId property should be String");
            });

            var descArray = errorResponse["errors"]["Description"].ToArray();
            List<string> descProperty = descArray.Select(msg => msg.ToString()).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(descProperty, Does.Contain("The Description field is required."), "Message for required Description should be present in array");

                Assert.That(descProperty, Does.Contain("Desctiption must be at least 3 symbols."), "Message for MIN length for Desctiption should be present in array");

                Assert.That(errorResponse["title"].ToString(), Does.Contain("One or more validation errors occurred."), "Validation errors message should be present");

                Assert.That(errorResponse["status"].Value<int>, Is.EqualTo(400), "Status property should be 400");

                Assert.That(errorResponse["traceId"].ToString(), Is.Not.Null.Or.Empty, "traceId property should not be null or empty");
            });
        }

        [Test, Order(12)]
        //edit non existent spoiler - DONE
        public void EditNonExistentSpoiler_WithAuth()
        {
            var testData_Spoiler = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("testData_Spoiler.json"));

            string spoilerId = (string)testData_Spoiler.Id + "8";

            LastStoryTitle = $"UpdatedTitle_{GlobalConstants.GenerateRandomNumber()}";
            LastStoryDescription = $"UpdatedDesc_{GlobalConstants.GenerateRandomNumber()}";

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
                Assert.That(editCreatedSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "Status code should be 'NotFound -> 404' when editing non existent spoiler");
                Assert.That(editCreatedSpoilerResponse.Content, Is.Not.Null.Or.Empty, "Response body should not be null or empty");
            });

            var responseBody = JObject.Parse(editCreatedSpoilerResponse.Content);

            Assert.Multiple(() =>
            {
                Assert.That(responseBody.ContainsKey("msg"), Is.True, "Msg property should exist");
                Assert.That(responseBody["msg"].Type, Is.EqualTo(JTokenType.String), "Msg property should be of type 'String'");
                Assert.That(responseBody["msg"].ToString(), Is.EqualTo("No spoilers..."), "Message for not found spoiler does not match expected");
            });
        }

        [Test, Order(13)]
        //delete spoiler with no auth - DONE
        public void DeleteCreatedSpoiler_NoAuth()
        {
            var testData_Spoiler = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("testData_Spoiler.json"));

            string spoilerId = (string)testData_Spoiler.Id;

            var deleteSpoilerRequest = new RestRequest($"/Story/Delete/{spoilerId}", Method.Delete);

            var deleteSpoilerResponse = client.Execute(deleteSpoilerRequest);

            Assert.Multiple(() =>
            {
                Assert.That(deleteSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized), "Status code should be 'Unauthorized' when deleting existing spoiler without authentication");
                Assert.That(deleteSpoilerResponse.Content, Is.Empty, "Response body should be empty");
            });
        }

        [Test, Order(14)]
        //delete non existent spoiler -DONE
        public void DeletenNonExistentSpoiler_WithAuth()
        {
            var testData_Spoiler = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("testData_Spoiler.json"));

            string spoilerId = (string)testData_Spoiler.Id + "8";

            var deleteSpoilerRequest = new RestRequest($"/Story/Delete/{spoilerId}", Method.Delete);
            deleteSpoilerRequest.AddHeader("Authorization", $"Bearer {token}");

            var deleteSpoilerResponse = client.Execute(deleteSpoilerRequest);

            Assert.Multiple(() =>
            {
                Assert.That(deleteSpoilerResponse.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest), "Status code should be 'BadRequest -> 404' when deleting non existent spoiler");
                Assert.That(deleteSpoilerResponse.Content, Is.Not.Null.Or.Empty, "Response body should not be null or empty");
            });

            var jtokenResponse = JToken.Parse(deleteSpoilerResponse.Content);

            Assert.That(jtokenResponse.Type, Is.EqualTo(JTokenType.Object), "Response body should be Array");

            var errorResponse = (JObject)jtokenResponse;

            Assert.Multiple(() =>
            {
                Assert.That(errorResponse.ContainsKey("msg"), Is.True, "Msg property should exist");
                Assert.That(errorResponse["msg"].Type, Is.EqualTo(JTokenType.String), "Msg property should be of type 'String'");
                Assert.That(errorResponse["msg"].ToString(), Is.EqualTo("Unable to delete this story spoiler!"), "Error message does not match expected");
            });
        }
    }
 }
