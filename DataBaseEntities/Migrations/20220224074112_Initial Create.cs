using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBaseEntities.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e79b11b-037d-4b3d-b5f5-6e6fdd9fd919");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3cab8335-f6a1-4755-876d-89d4a741cffd", "94e8a652-e7ad-451d-b9c9-13dae2e142d1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cab8335-f6a1-4755-876d-89d4a741cffd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94e8a652-e7ad-451d-b9c9-13dae2e142d1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3cab8335-f6a1-4755-876d-89d4a741cffd", "6081c57e-6bac-4d32-ae3e-8cdac00b4f77", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9e79b11b-037d-4b3d-b5f5-6e6fdd9fd919", "dc7fed43-78ac-4bd0-8ae7-83b5ac5b1245", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "94e8a652-e7ad-451d-b9c9-13dae2e142d1", 0, new DateTime(1995, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "110b6c75-3799-46de-b434-5df663f398c0", "admin@admin.com", false, "Maryam", "Rezaee", false, null, "ADMIN@ADMIN:COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEC39fQUR5nC2xvF+bM3oRX9HT6lrJ+xKROFtHzxV/1THN6SnoQ/NdM+ZWsPUuOgiPQ==", null, false, "48e6c171-2647-4b88-8737-4e5ea86f5a67", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3cab8335-f6a1-4755-876d-89d4a741cffd", "94e8a652-e7ad-451d-b9c9-13dae2e142d1" });
        }
    }
}
