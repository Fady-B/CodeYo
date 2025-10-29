using CodeYoDAL.DALHelpers;
using CodeYoDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShaghalnyDAL.Models;
using System.Reflection.Emit;

namespace CodeYoDAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region SeedData
            //builder.Entity<IdentityRole>().HasData(SeedData.GetIdentityRoles());
            builder.Entity<IdentityRole>().HasData(new List<IdentityRole>
            {
                new IdentityRole{Id = "8420350d-159f-4fe8-a729-5d72b5179884",Name = "Super Admin",NormalizedName = "SUPER ADMIN",ConcurrencyStamp = "e6648bfc-6c73-491b-81a3-b4000fb382e9"},
                new IdentityRole{Id = "3406c413-b2da-4599-8523-aeff97777549",Name = "Admin",NormalizedName = "ADMIN",ConcurrencyStamp = "bbc527bc-ea03-4b17-a8f8-4a9db04483a3"},
                new IdentityRole{Id = "23e23f22-5cc6-4a22-a6be-b9c9eecaa491",Name = "Supervisor",NormalizedName = "SUPERVISOR",ConcurrencyStamp = "9da54755-c68b-4226-a568-f9098ec7aa74"},
                new IdentityRole{Id = "7a871170-3e96-4ab3-a76b-aec96092a618",Name = "Home Page",NormalizedName = "HOME PAGE",ConcurrencyStamp = "322f0125-66de-4d30-b093-a89f4b59384e"},
                new IdentityRole{Id = "f8f08728-2742-45e5-a222-cc9b889c2b21",Name = "Students",NormalizedName = "STUDENTS",ConcurrencyStamp = "b2ac13ff-1145-4331-8e7e-2167e2cb92e0"},
                new IdentityRole{Id = "32f77820-adbd-4bf4-8bb9-c0668b6fa55e",Name = "Design Card",NormalizedName = "DESIGN CARD",ConcurrencyStamp = "4427b4f1-cf74-470a-a013-49461a1bd4e4"},
                new IdentityRole{Id = "32dcc3d9-17cf-44a8-aad8-69af8cd29277",Name = "Teachers",NormalizedName = "TEACHERS",ConcurrencyStamp = "c05658e7-7e35-4f87-a132-327d0aac7a27"}
            });
            builder.Entity<UserType>().HasData(SeedData.GetUserTypes());
            #endregion

            #region Relations
            builder.Entity<Teachers>()
                    .HasOne(t => t.TeacherCardsAttachement)
                    .WithOne(c => c.Teacher)
                    .HasForeignKey<TeacherCardsAttachements>(c => c.TeacherId);

            builder.Entity<TeacherStudents>().HasKey(st => new { st.StudentId, st.TeacherId });

            builder.Entity<TeacherStudents>()
                .HasOne(st => st.Student)
                .WithMany(s => s.TeacherStudents)
                .HasForeignKey(st => st.StudentId);

            builder.Entity<TeacherStudents>()
                .HasOne(st => st.Teacher)
                .WithMany(t => t.TeacherStudents)
                .HasForeignKey(st => st.TeacherId);
            #endregion

            #region Identitiy
            builder.Entity<ApplicationUser>()
                    //Table Rename 
                    .ToTable("Users", "security")
                    //To ignore columns from that table
                    .Ignore(i => i.PhoneNumberConfirmed);

            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
            #endregion

        }


        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<TeacherStudents> TeacherStudents { get; set; }
        public DbSet<TeacherCardsAttachements> TeacherCardsAttachements { get; set; }

    }
}
