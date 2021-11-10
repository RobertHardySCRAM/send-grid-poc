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
            var to = new EmailAddress("rhardy@scramsystems.com", "Robert Hardy");
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
        <a href=""https://nexus.scramnetwork.com""><img border=""0"" style=""color:#fff; text-decoration:none; font-family:Helvetica, arial, sans-serif; font-size:16px;"" width=""329""  height=""50"" alt=""SCRAM Nexus"" src=""data:image/png;base64, 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""></a>
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