using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using EmailNotificationFunction.Modal;
using EmailNotificationFunction.Helper;

namespace EmailNotificationFunction
{
    public static class EmailNotificationUsingHttp
    {
        [FunctionName("EmailNotificationUsingHttp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("EmailNotification Function processing a request.");
            string rawRequest = await new StreamReader(req.Body).ReadToEndAsync();

            log.LogInformation($"Request body: {rawRequest}");
            var emailData = JsonConvert.DeserializeObject<EmailModal>(rawRequest);            

            if (string.IsNullOrEmpty(emailData?.recipientEmail) 
                || string.IsNullOrEmpty(emailData?.subject) 
                || string.IsNullOrEmpty(emailData?.body))
            {
                log.LogError("Request missing recipient email, subject, or body.");
                return new BadRequestObjectResult("Please provide recipient email, subject, and body in the request body.");
            }

            await new EmailHelper(log).SendEmail(emailData.recipientEmail, emailData.subject, emailData.body);

            return new OkObjectResult("Email Sent Successfully.");
        }
    }
}

//Instead of Email Body we can also use a templates which can be stored in a database or a file. And use its Id to be send as
// a parameter in the request body. Then we can fetch the template from the database or file and use it as the email body.
// We can also use a Time trigger function to send the email at a specific time.
