using Bramka.Server.Constans;
using Bramka.Server.Interfaces;
using Bramka.Shared.Interfaces.Services;
using Bramka.Shared.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Data;
using Dapper;


namespace Bramka.Server.Services
{
    public class EmailService : IEmailService
    {
        private readonly IUserService _userService;
        private readonly IDbConnection _connection;
        private readonly string _apiKey;

        public EmailService(IConfiguration configuration, IUserService userService, IDbConnection dbConnection)
        {
            _connection = dbConnection;
            _apiKey = configuration["SendGridApiKey"]!;
            _userService = userService;
        }

        public async Task SendConfirmEmailAsync(string userId, string code)
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

            User user = await _userService.GetUserByIdAsync(new Guid(userId));

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

        public async Task CreateVerificationCodeAsync(string userId)
        {
            var code = GenerateRandomCode();

            var result = await _connection.ExecuteScalarAsync(DataBaseConstants.CreateVerificationCode,
                new
                {
                    UserId = userId,
                    Code = code,
                },
                commandType: CommandType.StoredProcedure);

            Console.WriteLine(result);
            await SendConfirmEmailAsync(userId, code);
        }

        private string GenerateRandomCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 25);
        }
    }
}
