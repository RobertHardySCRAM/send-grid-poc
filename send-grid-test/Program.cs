using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Example
{
    internal class Example
    {
        private static void Main()
        {
            Execute().Wait();
        }

        static async Task Execute()
        {
            var apiKey = "api-key-from-config";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("noreply@scramsystems.com", "SCRAM Systems No Reply");
            var subject = "SCRAM Nexus Report";
            var to = new EmailAddress("robert@goattea.com", "Robert Hardy");
            var plainTextContent = 
@"Hello {{firsName}} {{lastName}},

Here the report that you requested be sent to you from SCRAM Nexus: 
{{reportUrl}}
";
            var htmlContent =
 @"<div style=""padding: 8px 16px; margin: 0 0 20px 0; background-color: #06262d; width: 100%;"">
  <a href=""https://nexus.scramnetwork.com""><img border=""0"" style=""display:block; color:#000000; text-decoration:none; font-family:Helvetica, arial, sans-serif; font-size:16px;"" width=""329"" alt=""SCRAM Nexus"" data-proportionally-constrained=""true"" data-responsive=""false"" src=""https://nexus.scramnetwork.com/images/logo_scram_nexus.png"" height=""50""></a>
</div>

<p style=""font-family:Helvetica, arial, sans-serif; font-size:16px;"">
    Hello {{firsName}} {{lastName}},
<p>

<p style=""font-family:Helvetica, arial, sans-serif; font-size:16px;"">
    Here the report that you requested be sent to you from SCRAM Nexus:
<p>
<p style=""font-family:Helvetica, arial, sans-serif; font-size:16px;"">
    <a href=""{{reportUrl}}"">Click here to view the report.</a>
</p>
";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}