using Bramka.Server.Interfaces;
using Bramka.Server.Services;
using Bramka.Shared.DTOs.UserDTO;
using Bramka.Shared.Interfaces.Services;
using Bramka.Shared.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Bramka.Shared.DTOs.QrCodeDTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Bramka
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddSwaggerGen(options => { });
            var configuration = builder.Configuration;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        builder.Configuration.GetSection("AppSettings:Token").Value!)),
                };
            });

            builder.Services.AddScoped<IDbConnection>(_ => new SqlConnection(connectionString));


            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ILogService, LogService>();
            builder.Services.AddScoped<IQrCodeService, QrCodeService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            builder.Services.AddFluentValidation();
            builder.Services.AddTransient<IValidator<UserRegistrationDTO>, UserRegistrationValidation>();
            builder.Services.AddTransient<IValidator<UserEditDTO>, UserEditValidation>();
            builder.Services.AddTransient<IValidator<UserSetPasswordDTO>, UserSetPasswordValidation>();
            builder.Services.AddTransient<IValidator<QrCodeCreateDTO>, QrCodeCreateValidation>();


            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                app.UseSwagger();
                app.UseSwaggerUI(options => { });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}
