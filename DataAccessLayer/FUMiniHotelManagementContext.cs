using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;

namespace DAO
{
    public class FUMiniHotelManagementContext : DbContext
    {
        public FUMiniHotelManagementContext()
        {
        }

        public FUMiniHotelManagementContext(DbContextOptions<FUMiniHotelManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<RoomInformation> RoomInformations { get; set; }
        public virtual DbSet<BookingReservation> BookingReservations { get; set; }
        public virtual DbSet<BookingDetail> BookingDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Read connection string from appsettings.json
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==================== Customer Configuration ====================
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");
                entity.HasKey(e => e.CustomerID);

                entity.Property(e => e.CustomerID)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(3, 1); // IDENTITY(3,1) - starts from 3

                entity.Property(e => e.CustomerFullName)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(12)
                    .IsUnicode(true);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.Property(e => e.CustomerBirthday)
                    .HasColumnType("date");

                entity.Property(e => e.CustomerStatus)
                    .HasColumnType("tinyint");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                // Unique constraint on EmailAddress
                entity.HasIndex(e => e.EmailAddress)
                    .IsUnique();
            });

            // ==================== RoomType Configuration ====================
            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.ToTable("RoomType");
                entity.HasKey(e => e.RoomTypeID);

                entity.Property(e => e.RoomTypeID)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.RoomTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.Property(e => e.TypeDescription)
                    .HasMaxLength(250)
                    .IsUnicode(true);

                entity.Property(e => e.TypeNote)
                    .HasMaxLength(250)
                    .IsUnicode(true);
            });

            // ==================== RoomInformation Configuration ====================
            modelBuilder.Entity<RoomInformation>(entity =>
            {
                entity.ToTable("RoomInformation");
                entity.HasKey(e => e.RoomID);

                entity.Property(e => e.RoomID)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.RoomNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(true);

                // CRITICAL: Map C# property to correct DB column name
                entity.Property(e => e.RoomDescription)
                    .HasColumnName("RoomDetailDescription")
                    .HasMaxLength(220)
                    .IsUnicode(true);

                entity.Property(e => e.RoomMaxCapacity)
                    .HasColumnType("int");

                entity.Property(e => e.RoomStatus)
                    .HasColumnType("tinyint");

                // CRITICAL: Map C# property to correct DB column name
                entity.Property(e => e.RoomPricePerDate)
                    .HasColumnName("RoomPricePerDay")
                    .HasColumnType("money");

                entity.Property(e => e.RoomTypeID)
                    .IsRequired();

                // Foreign Key relationship
                entity.HasOne(e => e.RoomType)
                    .WithMany()
                    .HasForeignKey(e => e.RoomTypeID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ==================== BookingReservation Configuration ====================
            modelBuilder.Entity<BookingReservation>(entity =>
            {
                entity.ToTable("BookingReservation");
                entity.HasKey(e => e.BookingReservationID);

                // IMPORTANT: BookingReservationID is NOT IDENTITY in database
                entity.Property(e => e.BookingReservationID)
                    .ValueGeneratedNever();

                entity.Property(e => e.BookingDate)
                    .HasColumnType("date");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("money");

                entity.Property(e => e.CustomerID)
                    .IsRequired();

                entity.Property(e => e.BookingStatus)
                    .HasColumnType("tinyint");

                // Foreign Key relationship
                entity.HasOne(e => e.Customer)
                    .WithMany()
                    .HasForeignKey(e => e.CustomerID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ==================== BookingDetail Configuration ====================
            modelBuilder.Entity<BookingDetail>(entity =>
            {
                entity.ToTable("BookingDetail");

                // Composite Primary Key (BookingReservationID, RoomID)
                entity.HasKey(e => new { e.BookingReservationID, e.RoomID });

                entity.Property(e => e.StartDate)
                    .IsRequired()
                    .HasColumnType("date");

                entity.Property(e => e.EndDate)
                    .IsRequired()
                    .HasColumnType("date");

                entity.Property(e => e.ActualPrice)
                    .HasColumnType("money");

                // Foreign Key to BookingReservation
                entity.HasOne(e => e.BookingReservation)
                    .WithMany()
                    .HasForeignKey(e => e.BookingReservationID)
                    .OnDelete(DeleteBehavior.Cascade);

                // Foreign Key to RoomInformation
                // Use NoAction to avoid multiple cascade paths error
                entity.HasOne(e => e.RoomInformation)
                    .WithMany()
                    .HasForeignKey(e => e.RoomID)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}