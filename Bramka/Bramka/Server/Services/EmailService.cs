using Bramka.Server.Constans;
using Bramka.Server.Interfaces;
using Bramka.Shared.Interfaces.Services;
using Bramka.Shared.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Data;
using Dapper;
using System.Text;
using System.Text.Json;


namespace Bramka.Server.Services
{
    public class EmailService : IEmailService
    {
        private readonly IDbConnection _connection;
        private readonly string _apiKey;

        public EmailService(IConfiguration configuration, IDbConnection dbConnection)
        {
            _connection = dbConnection;
            _apiKey = configuration["SendGridApiKey"]!;
        }

        public async Task SendConfirmEmailAsync(User user)
        {
            #region SendGrid without SendGridMessage
            //var client = new SendGridClient(_apiKey);
            //var from = new EmailAddress("nyshchyi.nazar@gmail.com", "Nazar");
            //var to = new EmailAddress(toEmail);
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);
            //var response = await client.SendEmailAsync(msg);

            //if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
            //{
            //    throw new Exception($"Failed to send email. Status code: {response.StatusCode}");
            //}
            #endregion

            #region Test Fragment
            //var from = new EmailAddress("nyshchyi.nazar@gmail.com", "Nazar Nyshchyi");
            //var to = new EmailAddress(user.Email, user.Name);
            //var msg = MailHelper.CreateSingleEmail(from, to, 
            //    "Confirm your email for you new Bramka account", 
            //    "Confirm", 
            //    "Confirm");
            #endregion

            var code = await CreateVerificationCodeAsync(user.UserId.ToString());

            var client = new SendGridClient(_apiKey);

            var msg = new SendGridMessage();

            msg.SetTemplateId("d-644e61301c3642d4add2058581a91909");
            msg.SetFrom(new EmailAddress("nyshchyi.nazar@gmail.com", "Nazar Nyshchyi"));
            msg.AddTo(new EmailAddress(user.Email, user.Name));
            msg.SetTemplateData(new
            {
                confirmationEmailAddress = $"https://localhost:7218/api/Auth/confirmation?code={code}"
            });

            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new Exception($"Failed to send email. Status code: {response.StatusCode}");
            }
        }

        public async Task SendResetPasswordEmailAsync(User user)
        {
            var code = await CreateVerificationCodeAsync(user.UserId.ToString());

            await Console.Out.WriteLineAsync("Code: " + code);

            var client = new SendGridClient(_apiKey);

            var msg = new SendGridMessage();

            msg.SetTemplateId("d-ac614a2253024666a7862aa6fff039bd");
            msg.SetFrom(new EmailAddress("nyshchyi.nazar@gmail.com", "Bramka Team"));
            msg.AddTo(new EmailAddress(user.Email, user.Name));
            msg.SetTemplateData(new
            {
                resetPassword = $"https://localhost:7218/reset-password?code={code}"
            });

            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new Exception($"Failed to send email. Status code: {response.StatusCode}");
            }
        }

        public async Task<int> ConfirmEmailAsync(string code)
        {
            var response = await _connection.ExecuteAsync(DataBaseConstants.ConfirmEmail,
                new
                {
                    Code = code
                },
                commandType: CommandType.StoredProcedure);

            return response;
        }

        private async Task<string?> CreateVerificationCodeAsync(string userId)
        {
            var code = GenerateVerificationCode();

            var result = await _connection.ExecuteScalarAsync(DataBaseConstants.CreateVerificationCode,
                new
                {
                    UserId = userId,
                    Code = code,
                },
                commandType: CommandType.StoredProcedure);

            return code;

            #region 
            //var verCode = new VerificationCode()
            //{
            //    Code = GenerateVerificationCode(),
            //    Type = "VerificationEmail",
            //};

            //var byteCode = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(verCode));
            //var convertedCode = Convert.ToBase64String(byteCode);

            //var result = await _connection.ExecuteScalarAsync(DataBaseConstants.CreateVerificationCode,
            //    new
            //    {
            //        UserId = userId,
            //        verCode.Code,
            //    },
            //    commandType: CommandType.StoredProcedure);

            //Console.WriteLine(result);
            //await SendConfirmEmailAsync(userId, convertedCode);
            #endregion

            
        }

        private string GenerateVerificationCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 25);
        }
    }

    //class VerificationCode
    //{
    //    public string Code { get; set; }
    //    public string Type { get; set; }
    //}
}
