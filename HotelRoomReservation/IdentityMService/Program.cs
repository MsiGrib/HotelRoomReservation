using DataModel.DataBase;
using IdentityMService.EntityGateWay;
using IdentityMService.Service;
using IdentityMService.Utilitys;
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

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.ToString());
            });

            builder.Services.AddSingleton<BasicConfiguration>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                return new BasicConfiguration(configuration);
            });

            string? connectionString = builder?.Services?.BuildServiceProvider().GetRequiredService<BasicConfiguration>().ConnectionString;
            _ = builder?.Services.AddDbContext<IdentityDBContext>(options => options.UseNpgsql(connectionString));

            ServicesBinding(builder!.Services);

            var app = builder!.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

        private static void ServicesBinding(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IUserService, UserService>();

            serviceCollection.AddScoped<IUserProfileRepository, UserProfileRepository>();
            serviceCollection.AddScoped<IUserProfileService, UserProfileService>();
        }
    }
}
