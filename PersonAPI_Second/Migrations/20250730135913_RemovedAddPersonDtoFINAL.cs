using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonAPI_Second.Migrations
{
    /// <inheritdoc />
    public partial class RemovedAddPersonDtoFINAL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("133d523e-e57b-4865-a8d2-c8dccc5fe048"));

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("b861dd22-5c9e-4e6e-baf5-8784b979a8c3"));

            migrationBuilder.AddColumn<int>(
            name: "DisplayId",
            table: "Persons",
            nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Address", "DOB", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("133d523e-e57b-4865-a8d2-c8dccc5fe048"), "100 Charming Avenue", new DateOnly(2005, 4, 16), "Casper", "Mohabaty" },
                    { new Guid("b861dd22-5c9e-4e6e-baf5-8784b979a8c3"), "Wazzaaaaaa", new DateOnly(2000, 1, 1), "Flerbert", "Schminkledorf" }
                });

            migrationBuilder.DropColumn(
            name: "DisplayId",
            table: "Persons");
        }
    }
}
