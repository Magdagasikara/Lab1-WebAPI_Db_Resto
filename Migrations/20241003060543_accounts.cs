using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab1_WebAPI_Db_Resto.Migrations
{
    /// <inheritdoc />
    public partial class accounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ReservationEnd", "ReservationStart", "TimeStamp" },
                values: new object[] { new DateTime(2024, 10, 3, 10, 5, 42, 448, DateTimeKind.Local).AddTicks(8858), new DateTime(2024, 10, 3, 8, 5, 42, 448, DateTimeKind.Local).AddTicks(8791), new DateTime(2024, 10, 3, 8, 5, 42, 448, DateTimeKind.Local).AddTicks(8863) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ReservationEnd", "ReservationStart", "TimeStamp" },
                values: new object[] { new DateTime(2024, 10, 3, 10, 5, 42, 448, DateTimeKind.Local).AddTicks(8871), new DateTime(2024, 10, 3, 8, 5, 42, 448, DateTimeKind.Local).AddTicks(8868), new DateTime(2024, 10, 3, 8, 5, 42, 448, DateTimeKind.Local).AddTicks(8874) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ReservationEnd", "ReservationStart", "TimeStamp" },
                values: new object[] { new DateTime(2024, 9, 12, 11, 49, 4, 235, DateTimeKind.Local).AddTicks(6396), new DateTime(2024, 9, 12, 9, 49, 4, 235, DateTimeKind.Local).AddTicks(6342), new DateTime(2024, 9, 12, 9, 49, 4, 235, DateTimeKind.Local).AddTicks(6403) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ReservationEnd", "ReservationStart", "TimeStamp" },
                values: new object[] { new DateTime(2024, 9, 12, 11, 49, 4, 235, DateTimeKind.Local).AddTicks(6409), new DateTime(2024, 9, 12, 9, 49, 4, 235, DateTimeKind.Local).AddTicks(6407), new DateTime(2024, 9, 12, 9, 49, 4, 235, DateTimeKind.Local).AddTicks(6412) });
        }
    }
}
