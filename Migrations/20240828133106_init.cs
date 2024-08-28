﻿using System;
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "MealIngredient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_IngredientId = table.Column<int>(type: "int", nullable: false),
                    FK_MealId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealIngredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealIngredient_Ingredients_FK_IngredientId",
                        column: x => x.FK_IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealIngredient_Meals_FK_MealId",
                        column: x => x.FK_MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealMealCategory",
                columns: table => new
                {
                    MealCategoriesId = table.Column<int>(type: "int", nullable: false),
                    MealsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealMealCategory", x => new { x.MealCategoriesId, x.MealsId });
                    table.ForeignKey(
                        name: "FK_MealMealCategory_MealCategories_MealCategoriesId",
                        column: x => x.MealCategoriesId,
                        principalTable: "MealCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealMealCategory_Meals_MealsId",
                        column: x => x.MealsId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountOfGuests = table.Column<int>(type: "int", nullable: false),
                    ReservationStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    FK_CustomerId = table.Column<int>(type: "int", nullable: false),
                    TableId = table.Column<int>(type: "int", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Bookings_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TableBooking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_TableId = table.Column<int>(type: "int", nullable: false),
                    FK_BookingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableBooking_Bookings_FK_BookingId",
                        column: x => x.FK_BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TableBooking_Tables_FK_TableId",
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
                    { 1, null, false, "Tofu med kroppkakor", 0 },
                    { 2, null, true, "Pasta aglio e olio", 0 }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "AmountOfPlaces", "TableNumber" },
                values: new object[,]
                {
                    { 1, 4, 1 },
                    { 2, 2, 2 },
                    { 3, 4, 1 },
                    { 4, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "AmountOfGuests", "FK_CustomerId", "IsActive", "ReservationEnd", "ReservationStart", "TableId", "TimeStamp" },
                values: new object[,]
                {
                    { 1, 0, 2, true, new DateTime(2024, 8, 28, 17, 31, 6, 118, DateTimeKind.Local).AddTicks(9194), new DateTime(2024, 8, 28, 15, 31, 6, 118, DateTimeKind.Local).AddTicks(9139), null, new DateTime(2024, 8, 28, 15, 31, 6, 118, DateTimeKind.Local).AddTicks(9199) },
                    { 2, 6, 2, true, new DateTime(2024, 8, 28, 17, 31, 6, 118, DateTimeKind.Local).AddTicks(9205), new DateTime(2024, 8, 28, 15, 31, 6, 118, DateTimeKind.Local).AddTicks(9203), null, new DateTime(2024, 8, 28, 15, 31, 6, 118, DateTimeKind.Local).AddTicks(9208) }
                });

            migrationBuilder.InsertData(
                table: "MealIngredient",
                columns: new[] { "Id", "FK_IngredientId", "FK_MealId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 4, 1 },
                    { 3, 2, 2 },
                    { 4, 5, 2 }
                });

            migrationBuilder.InsertData(
                table: "TableBooking",
                columns: new[] { "Id", "FK_BookingId", "FK_TableId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FK_CustomerId",
                table: "Bookings",
                column: "FK_CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TableId",
                table: "Bookings",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_MealIngredient_FK_IngredientId",
                table: "MealIngredient",
                column: "FK_IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_MealIngredient_FK_MealId",
                table: "MealIngredient",
                column: "FK_MealId");

            migrationBuilder.CreateIndex(
                name: "IX_MealMealCategory_MealsId",
                table: "MealMealCategory",
                column: "MealsId");

            migrationBuilder.CreateIndex(
                name: "IX_TableBooking_FK_BookingId",
                table: "TableBooking",
                column: "FK_BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_TableBooking_FK_TableId",
                table: "TableBooking",
                column: "FK_TableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealIngredient");

            migrationBuilder.DropTable(
                name: "MealMealCategory");

            migrationBuilder.DropTable(
                name: "TableBooking");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "MealCategories");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Tables");
        }
    }
}
