
using Lab1_WebAPI_Db_Resto.Data;
using Lab1_WebAPI_Db_Resto.Data.Repositories;
using Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Lab1_WebAPI_Db_Resto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<RestoContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<ICustomerRepository,CustomerRepository>();

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
