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
 @"
<table style=""width: 100%; margin: 0;"">
<tr>
    <td style=""padding: 8px 16px 8px 16px; background-color: #06262d;"">
        <a href=""https://nexus.scramnetwork.com""><img border=""0"" style=""color:#fff; text-decoration:none; font-family:Helvetica, arial, sans-serif; font-size:16px;"" width=""329""  height=""50"" alt=""SCRAM Nexus"" src=""https://nexus.scramnetwork.com/images/logo_scram_nexus.png""></a>
    </td>
</tr>
<tr>
    <td style=""font-family:Helvetica, arial, sans-serif; font-size:16px; padding: 20px;"">
        <p>
            Hello {{firsName}} {{lastName}},
        <p>

        <p>
            Here the report that you requested be sent to you from SCRAM Nexus:
        <p>
        <p>
            <a href=""{{reportUrl}}"">Click here to view the report.</a>
        </p>
    <td>
</tr>
</table>
";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}