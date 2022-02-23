using MedicalAppointmentApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalAppointmentApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<MedicalSpeciality> MedicalSpecialities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Doctor>()
                .HasOne(p => p.MedicalSpeciality)
                .WithMany(b => b.Doctors)
                .HasForeignKey(p => p.MedicalSpecialityId);

            //define unique indexes
            modelBuilder.Entity<Doctor>()
                .HasIndex(u => new { u.FirstName, u.LastName })
                .IsUnique();

            modelBuilder.Entity<Institution>()
                .HasIndex(u => new { u.Name, u.Address })
                .IsUnique();

            modelBuilder.Entity<MedicalSpeciality>()
                .HasIndex(u => u.Name)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
