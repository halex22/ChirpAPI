using Npgsql;
using Microsoft.EntityFrameworkCore;
using ChirpAPI.Model;
using ChirpAPI.services.Services.Interfaces;
using ChirpAPI.services.Services;
using Serilog;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi;

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

            builder.Host.UseSerilog();

            builder.Services.AddSwaggerGen(options =>
                options.SwaggerDoc("v3", new OpenApiInfo
                {
                    Title = "Chirp API",
                    Version = "v3",
                    Description = "API for managing chirps and comments."
                })
            ); 

            // Add services to the container.
            builder.Services.AddDbContext<ChirpContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));

            builder.Services.AddControllers();

            builder.Services.AddScoped<IChirpsService, HugoChirpService>();
            builder.Services.AddScoped<ICommentsService, HugoCommentService>();

            var app = builder.Build();

            app.UseSwagger(c =>
                c.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0
            );
            app.UseSwaggerUI( c =>
                c.SwaggerEndpoint("v3/swagger.json", "Chirp API V1")
            );

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
