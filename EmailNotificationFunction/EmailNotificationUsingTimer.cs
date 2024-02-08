using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace EmailNotificationFunction
{
    public class EmailNotificationUsingTimer
    {
        [FunctionName("EmailNotificationUsingTimer")]
        public async Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            log.LogInformation("Fetching new records from DB to send Email Notifications");
            var recipientEmail = "";
            var subject = "";
            var body = "";

            log.LogInformation("Sending Email now...");
            await new Helper.EmailHelper(log).SendEmail(recipientEmail, subject, body);
            
            log.LogInformation("Email sent successfully");            
        }
    }
}
