using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookingWebApplication.Models;

public partial class AppDBContext : DbContext
{
    public AppDBContext()
    {
    }

    public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<ContentAdmin> ContentAdmins { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Provoli> Provoles { get; set; }
    //public DbSet<Reservation> Reservations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-05KKIM1;Database=booking_db;Trusted_Connection=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserName).ValueGeneratedNever();
            entity.Property(e => e.UserName).IsFixedLength();
            entity.Property(e => e.Email).IsFixedLength();
            entity.Property(e => e.Password).IsFixedLength();
            entity.Property(e => e.Salt).IsFixedLength();
            entity.Property(e => e.Role).IsFixedLength();

            entity.HasOne(e => e.Admin).WithOne(e => e.User).HasPrincipalKey<Admin>(e => e.UserName);
            entity.HasOne(e => e.ContentAdmin).WithOne(e => e.User).HasPrincipalKey<ContentAdmin>(e => e.UserName);
            entity.HasOne(e => e.Customer).WithOne(e => e.User).HasPrincipalKey<Customer>(e => e.UserName);
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).IsFixedLength();
        });

        modelBuilder.Entity<ContentAdmin>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).IsFixedLength();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).IsFixedLength();
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => new { e.MovieId, e.MovieName });

            entity.Property(e => e.MovieId).ValueGeneratedNever();
            entity.Property(e => e.MovieName).IsFixedLength();
            entity.Property(e => e.MovieContent).IsFixedLength();
            entity.Property(e => e.MovieType).IsFixedLength();
            entity.Property(e => e.MovieSummary).IsFixedLength();
            entity.Property(e => e.MovieDirector).IsFixedLength();

            entity.HasOne(e => e.ContentAdmin).WithMany(e => e.Movies);
        });

        modelBuilder.Entity<Cinema>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).IsFixedLength();
            entity.Property(e => e.Seats).IsFixedLength();
            entity.Property(e => e.I3D).IsFixedLength();
        });

        modelBuilder.Entity<Provoli>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(e => e.ContentAdmin).WithMany(e => e.Provoles);

            entity.HasOne(e => e.Cinema).WithMany(e => e.Provoles);

            entity
            .HasOne(e => e.Movie)
            .WithMany(e => e.Provoles)
            .HasForeignKey(e => new { e.MoviesId, e.MoviesName })
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        });

        /*modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasOne(e => e.Provoli).WithMany(e => e.Reservations);
            entity.HasOne(e => e.Customer).WithMany(e => e.Reservations);
        });*/

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
