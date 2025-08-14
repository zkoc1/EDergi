using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDergi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mid_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JMagazineId",
                table: "Volumes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "JMagazineId",
                table: "Volumes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
