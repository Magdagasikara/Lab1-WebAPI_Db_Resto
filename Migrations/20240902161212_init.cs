using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lab1_WebAPI_Db_Resto.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableNumber = table.Column<int>(type: "int", nullable: false),
                    AmountOfPlaces = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountOfGuests = table.Column<int>(type: "int", nullable: false),
                    ReservationStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FK_CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_FK_CustomerId",
                        column: x => x.FK_CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TableBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_TableId = table.Column<int>(type: "int", nullable: false),
                    FK_BookingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableBookings_Bookings_FK_BookingId",
                        column: x => x.FK_BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TableBookings_Tables_FK_TableId",
                        column: x => x.FK_TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "magda@m.m", "Magda", "076" },
                    { 2, "jocke@j.j", "Joakim", "070" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "IsAvailable", "Name" },
                values: new object[,]
                {
                    { 1, true, "Tofu" },
                    { 2, true, "Garlic" },
                    { 3, true, "Tomato" },
                    { 4, false, "Potato" },
                    { 5, true, "Pasta" },
                    { 6, false, "Chicken" }
                });

            migrationBuilder.InsertData(
                table: "MealCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Starter" },
                    { 2, "Dinner" },
                    { 3, "Dessert" },
                    { 4, "Snacks" },
                    { 5, "Drinks non-alco" },
                    { 6, "Beer" }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Description", "IsAvailable", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Det är bara svampsås som saknas", false, "Tofu med kroppkakor", 100 },
                    { 2, "Nom nom tres bienos!", true, "Pasta aglio e olio", 89 }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "AmountOfPlaces", "TableNumber" },
                values: new object[,]
                {
                    { 1, 4, 1 },
                    { 2, 2, 2 },
                    { 3, 4, 3 },
                    { 4, 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "AmountOfGuests", "BookingNumber", "FK_CustomerId", "ReservationEnd", "ReservationStart", "TimeStamp" },
                values: new object[,]
                {
                    { 1, 4, "120240830", 2, new DateTime(2024, 9, 2, 20, 12, 11, 650, DateTimeKind.Local).AddTicks(3619), new DateTime(2024, 9, 2, 18, 12, 11, 650, DateTimeKind.Local).AddTicks(3554), new DateTime(2024, 9, 2, 18, 12, 11, 650, DateTimeKind.Local).AddTicks(3624) },
                    { 2, 6, "220240830", 2, new DateTime(2024, 9, 2, 20, 12, 11, 650, DateTimeKind.Local).AddTicks(3634), new DateTime(2024, 9, 2, 18, 12, 11, 650, DateTimeKind.Local).AddTicks(3632), new DateTime(2024, 9, 2, 18, 12, 11, 650, DateTimeKind.Local).AddTicks(3636) }
                });

            migrationBuilder.InsertData(
                table: "TableBookings",
                columns: new[] { "Id", "FK_BookingId", "FK_TableId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingNumber",
                table: "Bookings",
                column: "BookingNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FK_CustomerId",
                table: "Bookings",
                column: "FK_CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MealIngredients_IngredientId",
                table: "MealIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_TableBookings_FK_BookingId",
                table: "TableBookings",
                column: "FK_BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_TableBookings_FK_TableId",
                table: "TableBookings",
                column: "FK_TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_TableNumber",
                table: "Tables",
                column: "TableNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealCategories");

            migrationBuilder.DropTable(
                name: "MealIngredients");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "TableBookings");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
