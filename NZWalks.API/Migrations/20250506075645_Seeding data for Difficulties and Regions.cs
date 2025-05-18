using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0abb3597-85b5-4d9b-aeb1-abe20834765b"), "Medium" },
                    { new Guid("8a8f1daf-aefc-492b-9463-2bfb5913dfb1"), "Hard" },
                    { new Guid("ebb9ae4c-86be-4f44-bf11-a1745df112bc"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("01276bef-5613-43b8-baf4-2b24597fc7f7"), "WGN", "Wellington", null },
                    { new Guid("4dde8174-be78-45ee-a15f-f2c6946f6409"), "STL", "Southland", null },
                    { new Guid("9c38ec4d-b715-495a-bd1c-6b050a85d25c"), "AKL", "Auckland", null },
                    { new Guid("bea23893-d31a-4a99-adcc-f51f5ed5de7b"), "BOP", "Bay Of Plenty", null },
                    { new Guid("de88803c-44a4-4c8c-bb5b-48d712547a33"), "NTL", "Northland", null },
                    { new Guid("f525993b-eec6-47b8-80c8-52923b3d2a27"), "NSN", "Nelson", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("0abb3597-85b5-4d9b-aeb1-abe20834765b"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("8a8f1daf-aefc-492b-9463-2bfb5913dfb1"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("ebb9ae4c-86be-4f44-bf11-a1745df112bc"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("01276bef-5613-43b8-baf4-2b24597fc7f7"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("4dde8174-be78-45ee-a15f-f2c6946f6409"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("9c38ec4d-b715-495a-bd1c-6b050a85d25c"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("bea23893-d31a-4a99-adcc-f51f5ed5de7b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("de88803c-44a4-4c8c-bb5b-48d712547a33"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f525993b-eec6-47b8-80c8-52923b3d2a27"));
        }
    }
}
