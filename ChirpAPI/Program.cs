using Npgsql;
using Microsoft.EntityFrameworkCore;
using ChirpAPI.Model;
using ChirpAPI.services.Services.Interfaces;
using ChirpAPI.services.Services;
using Serilog;

namespace ChirpAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            // Add services to the container.
            builder.Services.AddDbContext<ChirpContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));

            builder.Services.AddControllers();

            builder.Services.AddScoped<IChirpsService, HugoChirpService>();

            var app = builder.Build();

            //builder.Services.AddCors(options =>
            //    options.AddPolicy("AllowAllOrigins",
            //        builder => builder.AllowAnyOrigin()
            //                          .AllowAnyMethod()
            //                          .AllowAnyHeader())
            //);

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();
            //app.UseCors("AllowAllOrigins");


            app.MapControllers();

            app.Run();
        }
    }
}
