using EDergi.Domain.Entitites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace EDergi.Persistence.Contexts
{
    public class EDergiDbContext : IdentityDbContext<AppUser, AppRole, Guid,
        IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public EDergiDbContext(DbContextOptions<EDergiDbContext> options) : base(options) { }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<MDocument> MDocuments { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<ReadIndex> ReadIndices { get; set; }
        public DbSet<ViewStats> ViewStats { get; set; }
        public DbSet<Volume> Volumes { get; set; }
        public DbSet<ArticleAuthor> ArticleAuthors { get; set; }
        public DbSet<Endpoint> Endpoints { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<EmailMessage> EmailMessages { get; set; }
        public DbSet<EmailResult> EmailResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var adminRoleId = new Guid("1023E284-3B64-446C-9035-144A97B7DC33");
            var userRoleId = new Guid("5A71C6CF-74FB-46A8-B115-F3C6AF89D11E");
            var sysAdminUserId = new Guid("B22698B8-42A2-4115-9631-1C2D1E2AC5F6");
		

			modelBuilder.Entity<AppRole>().HasData(
                new AppRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new AppRole
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER"
                },
				new AppRole
				{
					Id = sysAdminUserId,
					Name = "SysAdmin",
					NormalizedName = "SYSADMIN"
				}
			);

            var adminUser = new AppUser
            {
                Id = sysAdminUserId,
                UserName = "admin@erciyes.edu.tr",
                NormalizedUserName = "ADMIN@ERCIYES.EDU.TR",
                Email = "admin@erciyes.edu.tr",
                NormalizedEmail = "ADMIN@ERCIYES.EDU.TR",
                EmailConfirmed = true,
                SecurityStamp = "b271d0a5-3b86-4f55-b6c8-0ba8dfdabc6f",
                ConcurrencyStamp = "e93829a7-b199-4822-8369-1f0b0eb3084b",
                FirstName = "Web",
                LastName = "Admin",
                IsChangedPassword = true,
                BirthDate = new DateTime(1990, 1, 1),
                IsSysAdmin = true,
                CreatedAt = new DateTime(2024, 1, 1),
                Status = true,
                PasswordHash = "AQAAAAIAAYagAAAAEBHtCwKlszb4j/GPnT3K6o8x8Rj1sP31TyPCqFfe08K4vbEftcWEtCcIpXeyEIAbQw==" // Q1w2e3r4.
            };

            modelBuilder.Entity<AppUser>().HasData(adminUser);
            modelBuilder.Entity<EmailMessage>().HasNoKey();
            modelBuilder.Entity<EmailResult>().HasNoKey();

            modelBuilder.Entity<AppUserRole>().HasData(
                new AppUserRole
                {
                    UserId = sysAdminUserId,
                    RoleId = adminRoleId
                }
            );

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(u => u.FirstName).HasMaxLength(50).IsRequired();
                entity.Property(u => u.LastName).HasMaxLength(50).IsRequired();
                entity.Property(u => u.CreatedAt).IsRequired();
            });

            modelBuilder.Entity<AppUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.ToTable("UserRoles");
            });

            modelBuilder.Entity<ArticleAuthor>()
                .HasKey(aa => new { aa.ArticleId, aa.AuthorId });

            modelBuilder.Entity<ArticleAuthor>()
                .HasOne(aa => aa.Article)
                .WithMany(a => a.ArticleAuthors)
                .HasForeignKey(aa => aa.ArticleId);

            modelBuilder.Entity<ArticleAuthor>()
                .HasOne(aa => aa.Author)
                .WithMany(a => a.ArticleAuthors)
                .HasForeignKey(aa => aa.AuthorId);

            modelBuilder.Entity<Endpoint>()
                .HasMany(e => e.Roles)
                .WithMany(r => r.Endpoints)
                .UsingEntity(j => j.ToTable("EndpointRoles"));
        }
    }
}
