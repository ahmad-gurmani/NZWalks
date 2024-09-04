using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class Seedingdataindatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("43b73e5e-fce6-4bf7-82cf-0c6acfc28838"), "Easy" },
                    { new Guid("83a89620-958d-4f66-a946-c9bd5ec2e5bf"), "Hard" },
                    { new Guid("d37febbf-1020-4d79-8f5f-7d717b6422bf"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("0147b3cd-e91f-424c-8b6f-57621605a926"), "AKL", "AuckLand", "https://ahdsdmad.com" },
                    { new Guid("2618cffb-7c72-4930-822c-9e216a137cae"), "MN", "Multan", "https://ahdsdmad.com" },
                    { new Guid("a41c6b3e-c709-44dc-9bb5-55350538c3ef"), "WLT", "Walington", "https://ahdsdmad.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("43b73e5e-fce6-4bf7-82cf-0c6acfc28838"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("83a89620-958d-4f66-a946-c9bd5ec2e5bf"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("d37febbf-1020-4d79-8f5f-7d717b6422bf"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("0147b3cd-e91f-424c-8b6f-57621605a926"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2618cffb-7c72-4930-822c-9e216a137cae"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a41c6b3e-c709-44dc-9bb5-55350538c3ef"));
        }
    }
}
