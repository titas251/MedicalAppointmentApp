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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //define relations
            modelBuilder.Entity<InstitutionDoctor>()
                .HasKey(ph => new { ph.DoctorId, ph.InstitutionId });

            modelBuilder.Entity<InstitutionDoctor>()
                .HasOne(p => p.Institution)
                .WithMany(p => p.Doctors)
                .HasForeignKey(p => p.InstitutionId);

            modelBuilder.Entity<InstitutionDoctor>()
                .HasOne(p => p.Doctor)
                .WithMany(p => p.Institutions)
                .HasForeignKey(p => p.DoctorId);

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
