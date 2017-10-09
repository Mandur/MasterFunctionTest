using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CosmosDBCreationProcess
{
    public static class Creation
    {
        [FunctionName("Creation")]
        public static async System.Threading.Tasks.Task<HttpResponseMessage> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "HttpTriggerCSharp/name/{name}")]HttpRequestMessage req, string name, TraceWriter log)
        {
            string databaseaccount = "asdasxxq";
            //https://asdasxxq.documents.azure.com:443/
            log.Info("C# HTTP trigger function processed a request.");

            string endpoint=$"https://{databaseaccount}.documents.azure.com/dbs?api-version=2015-04-08";
            log.Info(endpoint);

       
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("authorization",
                    "[Put your token here]");
                client.DefaultRequestHeaders.Add("x-ms-version",
                  "2015-04-08");
      

                var values = new Dictionary<string, string>
                    {
                    { "id", "hello" }
                    };
               var json= JsonConvert.SerializeObject(values);
                log.Info(json.ToString());
                var content = new StringContent(json.ToString(),System.Text.Encoding.UTF8, "application/query+json");

                //Taken from https://github.com/Azure/azure-documentdb-dotnet/tree/master/samples/rest-from-.net , seems otherwise has a problem
                content.Headers.ContentType.CharSet = "";

                var response = await client.PostAsync(endpoint, content);

                var responseString = await response.Content.ReadAsStringAsync();

                    log.Info(responseString);
            }

            return null;
        }
    }
}
