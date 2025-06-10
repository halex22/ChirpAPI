using Npgsql;
using Microsoft.EntityFrameworkCore;
using ChirpAPI.Model;

namespace ChirpAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ChirpContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
