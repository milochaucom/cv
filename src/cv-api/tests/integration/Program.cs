using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Milochau.CV.Tests.Integration.Apis;

namespace Milochau.CV.Tests.Integration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.FullName);
            });
            builder.Services.AddCors();

            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:3000");
            });
            app.MapGet("/", () =>
            {
                return Results.Redirect("/swagger");
            }).ExcludeFromDescription();

            app.MapResumesApis();
            app.MapOriginsApis();

            app.Run();
        }
    }
}