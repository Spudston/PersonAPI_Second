using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonAPI_Second.Migrations
{
    /// <inheritdoc />
    public partial class RemovedSetEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("de4ed0ca-52f2-47fb-b9f4-48d1ffd7417c"));

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("f2df542a-2a2e-4f66-b5d5-0af9722f11f7"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Address", "DOB", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("de4ed0ca-52f2-47fb-b9f4-48d1ffd7417c"), "100 Charming Avenue", new DateOnly(2005, 4, 16), "Casper", "Mohabaty" },
                    { new Guid("f2df542a-2a2e-4f66-b5d5-0af9722f11f7"), "Wazzaaaaaa", new DateOnly(2000, 1, 1), "Flerbert", "Schminkledorf" }
                });
        }
    }
}
