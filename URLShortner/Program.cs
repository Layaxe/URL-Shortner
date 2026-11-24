using Scalar.AspNetCore;
using URLShortner.Repositories;
using URLShortner.Services;

namespace URLShortner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Stuff I added
            builder.Services.Configure<SnowflakeIdOptions>(builder.Configuration.GetSection("Snowflake"));
            builder.Services.AddSingleton(TimeProvider.System);
            builder.Services.AddSingleton<IIdGenerator, SnowflakeIdGenerator>();
            builder.Services.AddSingleton<IShortUrlRepository, InMemoryShortUrlRepository>();
            builder.Services.AddScoped<UrlService, UrlService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
