using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDergi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mid_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("1023e284-3b64-446c-9035-144a97b7dc33"), new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "IsSysAdmin", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UpdatedAt", "UserName" },
                values: new object[] { new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f6"), 0, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e93829a7-b199-4822-8369-1f0b0eb3084b", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@erciyes.edu.tr", true, "Web", true, "Admin", false, null, "ADMIN@ERCIYES.EDU.TR", "ADMIN@ERCIYES.EDU.TR", "AQAAAAIAAYagAAAAEBHtCwKlszb4j/GPnT3K6o8x8Rj1sP31TyPCqFfe08K4vbEftcWEtCcIpXeyEIAbQw==", null, false, "b271d0a5-3b86-4f55-b6c8-0ba8dfdabc6f", true, false, null, "admin@erciyes.edu.tr" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("1023e284-3b64-446c-9035-144a97b7dc33"), new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f6") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("1023e284-3b64-446c-9035-144a97b7dc33"), new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f6") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f6"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "IsSysAdmin", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UpdatedAt", "UserName" },
                values: new object[] { new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7"), 0, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e93829a7-b199-4822-8369-1f0b0eb3084b", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@erciyes.edu.tr", true, "Web", true, "Admin", false, null, "ADMIN@ERCIYES.EDU.TR", "ADMIN@ERCIYES.EDU.TR", "AQAAAAIAAYagAAAAEBHtCwKlszb4j/GPnT3K6o8x8Rj1sP31TyPCqFfe08K4vbEftcWEtCcIpXeyEIAbQw==", null, false, "b271d0a5-3b86-4f55-b6c8-0ba8dfdabc6f", true, false, null, "admin@erciyes.edu.tr" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("1023e284-3b64-446c-9035-144a97b7dc33"), new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7") });
        }
    }
}
