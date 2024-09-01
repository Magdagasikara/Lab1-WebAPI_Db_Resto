using Lab1_WebAPI_Db_Resto.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1_WebAPI_Db_Resto.Data
{
    public class RestoContext : DbContext
    {
        public RestoContext(DbContextOptions<RestoContext> options) : base(options)
        {
        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealCategory> MealCategories { get; set; }
        public DbSet<MealIngredient> MealIngredients { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<TableBooking> TableBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Magda", Email = "magda@m.m", PhoneNumber = "076" },
                new Customer { Id = 2, Name = "Joakim", Email = "jocke@j.j", PhoneNumber = "070" }
                );
            modelBuilder.Entity<Table>().HasData(
                new Table { Id = 1, TableNumber = 1, AmountOfPlaces = 4 },
                new Table { Id = 2, TableNumber = 2, AmountOfPlaces = 2 },
                new Table { Id = 3, TableNumber = 3, AmountOfPlaces = 4 },
                new Table { Id = 4, TableNumber = 4, AmountOfPlaces = 2 }
                );
            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    Id = 1,
                    ReservationStart = DateTime.Now,
                    ReservationEnd = DateTime.Now + TimeSpan.FromHours(2),
                    TimeStamp = DateTime.Now,
                    BookingNumber = $"120240830",
                    AmountOfGuests = 4,
                    FK_CustomerId = 2
                },
                new Booking
                {
                    Id = 2,
                    ReservationStart = DateTime.Now,
                    ReservationEnd = DateTime.Now + TimeSpan.FromHours(2),
                    TimeStamp = DateTime.Now,
                    BookingNumber = $"220240830",
                    AmountOfGuests = 6,
                    FK_CustomerId = 2
                });

            modelBuilder.Entity<TableBooking>().HasData(
                new TableBooking { Id = 1, FK_TableId = 1, FK_BookingId = 1 },
                new TableBooking { Id = 2, FK_TableId = 2, FK_BookingId = 1 },
                new TableBooking { Id = 3, FK_TableId = 3, FK_BookingId = 2 }
                );
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "Tofu", IsAvailable = true },
                new Ingredient { Id = 2, Name = "Garlic", IsAvailable = true },
                new Ingredient { Id = 3, Name = "Tomato", IsAvailable = true },
                new Ingredient { Id = 4, Name = "Potato", IsAvailable = false },
                new Ingredient { Id = 5, Name = "Pasta", IsAvailable = true },
                new Ingredient { Id = 6, Name = "Chicken", IsAvailable = false }
                );
            modelBuilder.Entity<Meal>().HasData(
                new Meal
                {
                    Id = 1,
                    Name = "Tofu med kroppkakor",
                    IsAvailable = false
                },
                new Meal
                {
                    Id = 2,
                    Name = "Pasta aglio e olio",
                    IsAvailable = true
                }
                );
            modelBuilder.Entity<MealIngredient>().HasData(
                new MealIngredient { Id = 1, FK_MealId = 1, FK_IngredientId = 1 },
                new MealIngredient { Id = 2, FK_MealId = 1, FK_IngredientId = 4 },
                new MealIngredient { Id = 3, FK_MealId = 2, FK_IngredientId = 2 },
                new MealIngredient { Id = 4, FK_MealId = 2, FK_IngredientId = 5 }
                );
            modelBuilder.Entity<MealCategory>().HasData(
                new MealCategory { Id = 1, Name = "Starter" },
                new MealCategory { Id = 2, Name = "Dinner" },
                new MealCategory { Id = 3, Name = "Dessert" },
                new MealCategory { Id = 4, Name = "Snacks" },
                new MealCategory { Id = 5, Name = "Drinks non-alco" },
                new MealCategory { Id = 6, Name = "Beer" }
                );

        }

    }
}
