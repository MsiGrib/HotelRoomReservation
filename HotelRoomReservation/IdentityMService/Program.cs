using DataModel.DataBase;
using IdentityMService.EntityGateWay;
using IdentityMService.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;

namespace IdentityMService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<BasicConfiguration>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                return new BasicConfiguration(configuration);
            });

            string? connectionString = builder?.Services?.BuildServiceProvider().GetRequiredService<BasicConfiguration>().ConnectionString;
            _ = builder?.Services.AddDbContext<IdentityDBContext>(options => options.UseNpgsql(connectionString));
            builder?.Services.AddScoped<IUserRepository, UserRepository>();
            builder?.Services.AddScoped<IUserService, UserService>();

            var app = builder!.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
