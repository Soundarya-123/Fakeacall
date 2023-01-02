using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;


namespace Company.Function
{
    public static class Fakeacall
    {
        [FunctionName("Fakeacall")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string number = req.Query["number"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            number = number ?? data?.number;

            string accountSid = Environment.GetEnvironmentVariable("ACCOUNTSID");
            string authToken = Environment.GetEnvironmentVariable("AUTHTOKEN");

            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber(number);
            var from = new PhoneNumber(Envinornment.GetEnvironmentVariable
            ("TWILLIONUMBER"));

            var call = callResource.Create(to, from,
            twiml: new Twiml("<Response><say>HEYOOOOOOO, WELCOME</say></Response>"));

    



            string responseMessage = call.sid;

            
            return new OkObjectResult(responseMessage);
        }
    }
}
