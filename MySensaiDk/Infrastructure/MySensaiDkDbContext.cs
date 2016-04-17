using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySensaiDk.Models;
using Microsoft.AspNet.Identity;
using System.Collections;
using MySensaiDk.Migrations;
using System.Data.Entity.Validation;

namespace MySensaiDk.Infrastructure
{
    public partial class MySensaiDkDbContext : IdentityDbContext<AppUser>
    {
        public MySensaiDkDbContext() : base("MySenesaiDB")
        {
        }

        static MySensaiDkDbContext()
        {
            Database.SetInitializer<MySensaiDkDbContext>(new IdentityTestDbInit());
        }

        public static MySensaiDkDbContext Create()
        {
            return new MySensaiDkDbContext();
        }

        public IEnumerable AppUsers { get; internal set; }
        public IEnumerable AppRoles { get; internal set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<CourseTag> CoursesTags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().Property(p => p.FirstName).IsRequired();
            modelBuilder.Entity<AppUser>().Property(p => p.LastName).IsRequired();
            modelBuilder.Entity<AppUser>().Property(p => p.Email).IsRequired();
            modelBuilder.Entity<AppUser>().Property(p => p.PasswordHash).IsRequired();
            modelBuilder.Entity<AppUser>().Property(p => p.UserName).IsOptional();
            modelBuilder.Entity<AppUser>().Property(p => p.Birthday).IsOptional();

            modelBuilder.Entity<Course>().HasKey(pk => pk.CourseId);
            modelBuilder.Entity<Course>().Property(p => p.Titel).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Course>().Property(p => p.Description).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Course>().HasRequired<AppUser>(c => c.User).WithMany(a => a.Courses).HasForeignKey(c => c.TeacherID);
            modelBuilder.Entity<Course>().HasOptional<Address>(c => c.Address).WithMany(ad => ad.Courses).HasForeignKey(c => c.AddressId);

            modelBuilder.Entity<Enrollment>().HasKey(pk => pk.EnrollmentId);
            modelBuilder.Entity<Enrollment>().HasRequired<AppUser>(e => e.User).WithMany(a => a.UserEnrollments).HasForeignKey(e => e.UserId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Enrollment>().HasRequired<Course>(e => e.Course).WithMany(c => c.CourseEnrollments).HasForeignKey(e => e.CourseId);

            modelBuilder.Entity<Tag>().HasKey(pk => pk.TagId);
            modelBuilder.Entity<Tag>().Property(p => p.TagName).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<CourseTag>().HasKey(pk => pk.CourseTagId);
            modelBuilder.Entity<CourseTag>().HasRequired<Tag>(ct => ct.Tag).WithMany(t => t.courses).HasForeignKey(ct => ct.TagId);
            modelBuilder.Entity<CourseTag>().HasRequired<Course>(ct => ct.Course).WithMany(c => c.Tags).HasForeignKey(ct => ct.CourseId);

            modelBuilder.Entity<City>().HasKey(pk => pk.CityId);
            modelBuilder.Entity<City>().Property(p => p.PostalNumber).HasMaxLength(4).IsRequired();
            modelBuilder.Entity<City>().Property(p => p.CityName).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Address>().HasKey(pk => pk.AddressId);
            modelBuilder.Entity<Address>().Property(p => p.FullAddress).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Address>().Property(p => p.AddressName).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Address>().HasRequired(ad => ad.User).WithMany(a => a.Addresses).HasForeignKey(ad => ad.UserId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Address>().HasRequired(ad => ad.City).WithMany(ci => ci.Addresses).HasForeignKey(ad => ad.CityId);

            modelBuilder.Entity<Address>().MapToStoredProcedures();

            base.OnModelCreating(modelBuilder);
        }
    }

    public class IdentityTestDbInit : NullDatabaseInitializer<MySensaiDkDbContext>
    {
    }

    // https://stackoverflow.com/questions/15820505/dbentityvalidationexception-how-can-i-easily-tell-what-caused-the-error
    public partial class MySensaiDkDbContext
    {
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
    }
}
