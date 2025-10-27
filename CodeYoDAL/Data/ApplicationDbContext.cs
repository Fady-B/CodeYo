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
            builder.Entity<IdentityRole>().HasData(SeedData.GetIdentityRoles());
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
