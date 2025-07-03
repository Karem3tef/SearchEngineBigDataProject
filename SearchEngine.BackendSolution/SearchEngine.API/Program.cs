
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SearchEngine.Application;
using SearchEngine.Domain.Repository.Core;
using SearchEngine.Domain.Services.Core;
using SearchEngine.Infrastructure.Data;
using SearchEngine.Infrastructure.Repositories;

namespace SearchEngine.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<EngineContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped(typeof(IPageRankRepo),typeof(PageRankRepo));
            builder.Services.AddScoped(typeof(IWordToUrlRepo), typeof(WordToUrlRepo));
            builder.Services.AddScoped(typeof(ISearchService), typeof(SearchService));

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<EngineContext>();
                await EngineContextSeed.SeedAsync(dbContext); // Your seeding method
            }

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
