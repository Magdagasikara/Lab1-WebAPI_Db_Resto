
using Lab1_WebAPI_Db_Resto.Data;
using Lab1_WebAPI_Db_Resto.Data.Repositories;
using Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories;
using Lab1_WebAPI_Db_Resto.Services;
using Lab1_WebAPI_Db_Resto.Services.IServices;
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
                options.UseSqlServer(builder.Configuration.GetConnectionString("RestoConnection"));
            });
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            // add Cors to communicate with frontend
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalFrontend",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:7170")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<ITableRepository, TableRepository>();
            builder.Services.AddScoped<ITableServices, TableService>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IMealRepository, MealRepository>();
            builder.Services.AddScoped<IMealService, MealService>();
            builder.Services.AddScoped<IMealCategoryRepository, MealCategoryRepository>();
            builder.Services.AddScoped<IMealCategoryService, MealCategoryService>();

            var app = builder.Build();

            app.UseCors("AllowLocalFrontend");

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
