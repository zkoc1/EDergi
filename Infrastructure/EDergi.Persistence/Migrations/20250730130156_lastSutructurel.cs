using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDergi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class lastSutructurel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Magazines_MagazineId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Articles_ArticleId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Magazines_ViewStats_ViewStatsId",
                table: "Magazines");

            migrationBuilder.DropForeignKey(
                name: "FK_MDocuments_Magazines_MagazineId",
                table: "MDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Publishers_Magazines_MagazineId",
                table: "Publishers");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadIndices_Magazines_MagazineId",
                table: "ReadIndices");

            migrationBuilder.DropForeignKey(
                name: "FK_Volumes_Articles_ArticleId",
                table: "Volumes");

            migrationBuilder.DropTable(
                name: "ArticleIssues");

            migrationBuilder.DropTable(
                name: "MNumbers");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_MagazineId",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Authors_ArticleId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Articles_MagazineId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "MagazineId",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "MagazineId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "ArticleId",
                table: "Volumes",
                newName: "JMagazineId");

            migrationBuilder.RenameIndex(
                name: "IX_Volumes_ArticleId",
                table: "Volumes",
                newName: "IX_Volumes_JMagazineId");

            migrationBuilder.RenameColumn(
                name: "ViewStatsId",
                table: "Magazines",
                newName: "PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_Magazines_ViewStatsId",
                table: "Magazines",
                newName: "IX_Magazines_PublisherId");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Volumes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "MagazineId",
                table: "ViewStats",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "MagazineId",
                table: "ReadIndices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MagazineId",
                table: "MDocuments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDate",
                table: "Issues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "IssueId",
                table: "Articles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PdfUrl",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ArticleAuthors",
                columns: table => new
                {
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleAuthors", x => new { x.ArticleId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_ArticleAuthors_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ViewStats_MagazineId",
                table: "ViewStats",
                column: "MagazineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_IssueId",
                table: "Articles",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleAuthors_AuthorId",
                table: "ArticleAuthors",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Issues_IssueId",
                table: "Articles",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Magazines_Publishers_PublisherId",
                table: "Magazines",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MDocuments_Magazines_MagazineId",
                table: "MDocuments",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadIndices_Magazines_MagazineId",
                table: "ReadIndices",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ViewStats_Magazines_MagazineId",
                table: "ViewStats",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Volumes_Magazines_JMagazineId",
                table: "Volumes",
                column: "JMagazineId",
                principalTable: "Magazines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Issues_IssueId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Magazines_Publishers_PublisherId",
                table: "Magazines");

            migrationBuilder.DropForeignKey(
                name: "FK_MDocuments_Magazines_MagazineId",
                table: "MDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadIndices_Magazines_MagazineId",
                table: "ReadIndices");

            migrationBuilder.DropForeignKey(
                name: "FK_ViewStats_Magazines_MagazineId",
                table: "ViewStats");

            migrationBuilder.DropForeignKey(
                name: "FK_Volumes_Magazines_JMagazineId",
                table: "Volumes");

            migrationBuilder.DropTable(
                name: "ArticleAuthors");

            migrationBuilder.DropIndex(
                name: "IX_ViewStats_MagazineId",
                table: "ViewStats");

            migrationBuilder.DropIndex(
                name: "IX_Articles_IssueId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Volumes");

            migrationBuilder.DropColumn(
                name: "MagazineId",
                table: "ViewStats");

            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "IssueId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "PdfUrl",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "JMagazineId",
                table: "Volumes",
                newName: "ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_Volumes_JMagazineId",
                table: "Volumes",
                newName: "IX_Volumes_ArticleId");

            migrationBuilder.RenameColumn(
                name: "PublisherId",
                table: "Magazines",
                newName: "ViewStatsId");

            migrationBuilder.RenameIndex(
                name: "IX_Magazines_PublisherId",
                table: "Magazines",
                newName: "IX_Magazines_ViewStatsId");

            migrationBuilder.AlterColumn<Guid>(
                name: "MagazineId",
                table: "ReadIndices",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "MagazineId",
                table: "Publishers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MagazineId",
                table: "MDocuments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ArticleId",
                table: "Authors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MagazineId",
                table: "Articles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ArticleIssues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IssueId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleIssues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleIssues_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArticleIssues_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MNumbers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VolumesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MagazineId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NumberOf = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MNumbers_Magazines_MagazineId",
                        column: x => x.MagazineId,
                        principalTable: "Magazines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MNumbers_Volumes_VolumesId",
                        column: x => x.VolumesId,
                        principalTable: "Volumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_MagazineId",
                table: "Publishers",
                column: "MagazineId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_ArticleId",
                table: "Authors",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_MagazineId",
                table: "Articles",
                column: "MagazineId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleIssues_ArticleId",
                table: "ArticleIssues",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleIssues_IssueId",
                table: "ArticleIssues",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_MNumbers_MagazineId",
                table: "MNumbers",
                column: "MagazineId");

            migrationBuilder.CreateIndex(
                name: "IX_MNumbers_VolumesId",
                table: "MNumbers",
                column: "VolumesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Magazines_MagazineId",
                table: "Articles",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Articles_ArticleId",
                table: "Authors",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Magazines_ViewStats_ViewStatsId",
                table: "Magazines",
                column: "ViewStatsId",
                principalTable: "ViewStats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MDocuments_Magazines_MagazineId",
                table: "MDocuments",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Publishers_Magazines_MagazineId",
                table: "Publishers",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReadIndices_Magazines_MagazineId",
                table: "ReadIndices",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Volumes_Articles_ArticleId",
                table: "Volumes",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
