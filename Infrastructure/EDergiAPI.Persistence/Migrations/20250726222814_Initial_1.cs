using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDergiAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleIssue_Articles_ArticleId",
                table: "ArticleIssue");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleIssue_Issues_IssueId",
                table: "ArticleIssue");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Volume_VolumeId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_MNumberOf_Magazines_MagazineId",
                table: "MNumberOf");

            migrationBuilder.DropForeignKey(
                name: "FK_MNumberOf_Volume_VolumesId",
                table: "MNumberOf");

            migrationBuilder.DropForeignKey(
                name: "FK_Volume_Articles_ArticleId",
                table: "Volume");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Volume",
                table: "Volume");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MNumberOf",
                table: "MNumberOf");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleIssue",
                table: "ArticleIssue");

            migrationBuilder.RenameTable(
                name: "Volume",
                newName: "Volumes");

            migrationBuilder.RenameTable(
                name: "MNumberOf",
                newName: "MNumbers");

            migrationBuilder.RenameTable(
                name: "ArticleIssue",
                newName: "ArticleIssues");

            migrationBuilder.RenameIndex(
                name: "IX_Volume_ArticleId",
                table: "Volumes",
                newName: "IX_Volumes_ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_MNumberOf_VolumesId",
                table: "MNumbers",
                newName: "IX_MNumbers_VolumesId");

            migrationBuilder.RenameIndex(
                name: "IX_MNumberOf_MagazineId",
                table: "MNumbers",
                newName: "IX_MNumbers_MagazineId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleIssue_IssueId",
                table: "ArticleIssues",
                newName: "IX_ArticleIssues_IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleIssue_ArticleId",
                table: "ArticleIssues",
                newName: "IX_ArticleIssues_ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Volumes",
                table: "Volumes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MNumbers",
                table: "MNumbers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleIssues",
                table: "ArticleIssues",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleIssues_Articles_ArticleId",
                table: "ArticleIssues",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleIssues_Issues_IssueId",
                table: "ArticleIssues",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Volumes_VolumeId",
                table: "Issues",
                column: "VolumeId",
                principalTable: "Volumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MNumbers_Magazines_MagazineId",
                table: "MNumbers",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MNumbers_Volumes_VolumesId",
                table: "MNumbers",
                column: "VolumesId",
                principalTable: "Volumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Volumes_Articles_ArticleId",
                table: "Volumes",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleIssues_Articles_ArticleId",
                table: "ArticleIssues");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleIssues_Issues_IssueId",
                table: "ArticleIssues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Volumes_VolumeId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_MNumbers_Magazines_MagazineId",
                table: "MNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_MNumbers_Volumes_VolumesId",
                table: "MNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_Volumes_Articles_ArticleId",
                table: "Volumes");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Volumes",
                table: "Volumes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MNumbers",
                table: "MNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleIssues",
                table: "ArticleIssues");

            migrationBuilder.RenameTable(
                name: "Volumes",
                newName: "Volume");

            migrationBuilder.RenameTable(
                name: "MNumbers",
                newName: "MNumberOf");

            migrationBuilder.RenameTable(
                name: "ArticleIssues",
                newName: "ArticleIssue");

            migrationBuilder.RenameIndex(
                name: "IX_Volumes_ArticleId",
                table: "Volume",
                newName: "IX_Volume_ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_MNumbers_VolumesId",
                table: "MNumberOf",
                newName: "IX_MNumberOf_VolumesId");

            migrationBuilder.RenameIndex(
                name: "IX_MNumbers_MagazineId",
                table: "MNumberOf",
                newName: "IX_MNumberOf_MagazineId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleIssues_IssueId",
                table: "ArticleIssue",
                newName: "IX_ArticleIssue_IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleIssues_ArticleId",
                table: "ArticleIssue",
                newName: "IX_ArticleIssue_ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Volume",
                table: "Volume",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MNumberOf",
                table: "MNumberOf",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleIssue",
                table: "ArticleIssue",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleIssue_Articles_ArticleId",
                table: "ArticleIssue",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleIssue_Issues_IssueId",
                table: "ArticleIssue",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Volume_VolumeId",
                table: "Issues",
                column: "VolumeId",
                principalTable: "Volume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MNumberOf_Magazines_MagazineId",
                table: "MNumberOf",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MNumberOf_Volume_VolumesId",
                table: "MNumberOf",
                column: "VolumesId",
                principalTable: "Volume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Volume_Articles_ArticleId",
                table: "Volume",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
