using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Dynamic_Application_Krish
{

    internal class Globals
    {
        private static string token;
        private static string lnId = "";
        private static string docId = "";

        public static string GetAccessToken()
        {
            return token;
        }

        public static async Task SetAccessToken()
        {
            var options = new RestClientOptions("https://api.elliemae.com")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/oauth2/v1/token", Method.Post);
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", "icestudent@encompass:BE11166969");
            request.AddParameter("password", "student@DEV22");
            request.AddParameter("client_id", "ruc51qr");
            request.AddParameter("client_secret", "ysli#MSdlbyHngOyctiJmK$0l82^9!o$dzSwvhW612!GPoi38loF4CFQ8ZiJTu$@");
            RestResponse response = await client.ExecuteAsync(request);
            //Console.WriteLine(response.Content);
            //Console.ReadLine();
            JObject respContent = JObject.Parse(response.Content);
            //Instantiate a JToken object with the extracted access token
            JToken aToken = respContent.SelectToken("$.access_token");
            //Set the token variable to the extracted access token as a string
            token = aToken.ToString();
        }
        public static string GetLoanId()
        {
            return lnId;
        }
        public static void SetLoanId(string id)
        {
            id = lnId;
        }
        public static async Task CreateLoanTemplate(string fname, string lname, string streetAddress, string city, string state, string zip)
        {
            var options = new RestClientOptions("https://api.elliemae.com")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/encompass/v3/loans?loanFolder=DevEssentialsCert&view=entity&templateType=templateSet&templatePath=Public%3A%5C%5CCompanywide%5CConventional", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);
            var body = @"{" + "\n" +
            @"    ""applications"": [" + "\n" +
            @"        {" + "\n" +
            @"            ""borrower"": {" + "\n" +
            @"                ""firstName"": """ + fname + @" "", " + "\n" +
            @"                ""lastName"": """ + lname + @" "" " + "\n" +
            @"            }" + "\n" +
            @"        }" + "\n" +
            @"    ]," + "\n" +
            @"    ""property"": {" + "\n" +
            @"        ""addressLineText"": """ + streetAddress + @"""," + "\n" +
            @"        ""city"": """ + city + @"""," + "\n" +
            @"        ""state"": """ + state + @"""," + "\n" +
            @"        ""postalCode"": """ + zip + @"""," + "\n" +
            @"    }" + "\n" +
            @"}";
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            //Parse the response content
            JObject respContent = JObject.Parse(response.Content);
            //Instantiate a JToken object with the extracted lnId
            JToken loanGuid = respContent.SelectToken("$.id");
            //Set the lnId variable to the extracted lnId as a string
            lnId = loanGuid.ToString();
        }
        public static string GetDocId()
        {
            return docId;
        }
        public static void SetDocId(string dId)
        {
            dId = docId;
        }
        public static async Task AddBankStatement()
        {
            var options = new RestClientOptions("https://api.elliemae.com")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/encompass/v3/loans/{loan}/documents?action=add&view=entity", Method.Patch)
                .AddUrlSegment("loan", GetLoanId());
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");
            var body = @"[" + "\n" +
            @"  {" + "\n" +
            @"    ""title"": ""Bank Statements""," + "\n" +
            @"    ""description"": ""Bank statements for Bank A and Bank B for the last two months.""" + "\n" +
            @"  }" + "\n" +
            @"]";
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            //Parse the response content
            docId = response.Content.Substring(response.Content.IndexOf(":") + 2, 36); ;
        }
    }
}


