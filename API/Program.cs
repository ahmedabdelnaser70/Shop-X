
using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ShopX
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

            builder.Services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("StoreXDB"));
            });

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AngularCors", policy =>
                {
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials()
                          .WithOrigins("http://localhost:4200", "https://localhost:4200");
                });
            });

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AngularCors");
            app.UseHttpsRedirection();

            //app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            #region migration for seedData

            try
            {
                using var scope = app.Services.CreateScope();
                var service = scope.ServiceProvider;
                var context = service.GetRequiredService<StoreContext>();
                await context.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            #endregion


            app.Run();
        }
    }
}
