using System;
using System.Collections.Generic;
using System.Text;
using gurleenProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace gurleenProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }


        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<StoreExpense> StoreExpense { get; set; }
        public virtual DbSet<StoreTravel> StoreTravel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-QFUESHD;Database=gurleenProject;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreId)
                    .HasName("PK_store");

                entity.ToTable("store");

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Hours).HasColumnName("hours");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location");

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasColumnName("storeName");
            });

            modelBuilder.Entity<StoreExpense>(entity =>
            {
                entity.HasKey(e => e.ExpenseId)
                    .HasName("PK_storeExpense_1");

                entity.ToTable("storeExpense");

                entity.Property(e => e.ExpenseId).HasColumnName("expenseId");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasColumnName("storeName");

                entity.Property(e => e.TotalExpense)
                    .HasColumnName("totalExpense")
                    .HasColumnType("money");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreExpense)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_storeExpense_store");
            });

            modelBuilder.Entity<StoreTravel>(entity =>
            {
                entity.HasKey(e => e.TravelId);

                entity.ToTable("storeTravel");

                entity.Property(e => e.TravelId).HasColumnName("travelId");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.LocationFrom)
                    .IsRequired()
                    .HasColumnName("locationFrom");

                entity.Property(e => e.LocationTo)
                    .IsRequired()
                    .HasColumnName("locationTo");

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasColumnName("storeName");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreTravel)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_storeTravel_store");
            });
        }
    }
}

