using Application;
using Application.Services;
using Infrastructure;
using Infrastructure.IDateTimeService;
using Infrastructure.TargetAssetRepo;

namespace Demo_API
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
            builder.Services.AddHttpClient("TargetAssetAPI", c =>
            {
                c.BaseAddress = new Uri("https://06ba2c18-ac5b-4e14-988c-94f400643ebf.mock.pstmn.io");
            });

            builder.Services.AddScoped<ITargetAssetRepository, TargetAssetRepository>();
            builder.Services.AddScoped<ITargetAssetService, TargetAssetService>();
            builder.Services.AddScoped<IDateTimeService, DateTimeService>();

            var app = builder.Build();

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