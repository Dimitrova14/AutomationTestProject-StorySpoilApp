
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using static System.Net.WebRequestMethods;

namespace StorySpoilAppTests.API_Tests
{
    public class GlobalConstants
    {
        public const string BaseUrl = "https://d5wfqm7y6yb3q.cloudfront.net/api";

        public static string AuthenticateUser(string username, string password)
        {
            var authClient = new RestClient(BaseUrl);
            var request = new RestRequest("/User/Authentication", Method.Post);
            request.AddJsonBody(new { username, password });

            var response = authClient.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Assert.Fail($"Authentication failed with status code: {response.StatusCode}, " +
                    $"content: {response.Content}");
            }

            var content = JObject.Parse(response.Content);
            return content["accessToken"]?.ToString();
        }

        public static string GenerateRandomNumber()
        {
            var random = new Random();
            return random.Next(999, 100000).ToString();
        }
    }
}
