﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

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
    public DbSet<Reservation> Reservations { get; set; }

/*    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-05KKIM1;Database=booking_db;Trusted_Connection=True;Trust Server Certificate=True");*/

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

            entity.HasOne(e => e.Admin).WithOne(e => e.User).HasForeignKey<Admin>(e => e.UserName);
            entity.HasOne(e => e.ContentAdmin).WithOne(e => e.User).HasForeignKey<ContentAdmin>(e => e.UserName);
            entity.HasOne(e => e.Customer).WithOne(e => e.User).HasForeignKey<Customer>(e => e.UserName);
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
            entity.Property(e => e.ShowDateTime).HasPrecision(0);

            entity.HasOne(e => e.ContentAdmin).WithMany(e => e.Provoles);

            entity.HasOne(e => e.Cinema).WithMany(e => e.Provoles);

            entity
            .HasOne(e => e.Movie)
            .WithMany(e => e.Provoles)
            .HasForeignKey(e => new { e.MoviesId, e.MoviesName })
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasKey(e => new { e.MoviesId, e.MoviesName, e.ShowDateTime, e.CinemasID, e.ContentAdminId });
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity
            .HasOne(e => e.Provoli)
            .WithMany(e => e.Reservations)
            .HasForeignKey(e => new {e.ProvolesMoviesId, e.ProvolesMoviesName, e.ProvolesDateTime, e.ProvolesCinemasId, e.ProvolesContentAdminId})
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Customer).WithMany(e => e.Reservations).HasForeignKey(e => e.CustomersId);

            entity.HasKey(e => new { e.ProvolesMoviesId, e.ProvolesMoviesName, e.ProvolesCinemasId, e.ProvolesDateTime, e.CustomersId });
        });


        // TODO Testing Data DELETE AFTER TESTING
        modelBuilder.Entity<User>().HasData(
            new User { UserName = "al", Email = "al@testmail.com", Password = "123456", CreateTime = DateTime.Now, Role = "ContentAdmin", Salt = "123" }
            //,new User { UserName = "kon", Email = "kon@testmail.com", Password = "123456", CreateTime = DateTime.Now, Role = "Customer", Salt = "123" }
            );
        modelBuilder.Entity<ContentAdmin>().HasData(
            new ContentAdmin { Id = 0, Name = "alex", UserName = "al" }
            );
        /*modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = 0, Name = "konstantinos", UserName = "kon" }
            );*/
        modelBuilder.Entity<Movie>().HasData(
            new Movie {
                MovieId = 0,
                MovieName = "The Shawshank Redemption",
                MovieContent = "Content",
                MovieDirector= "Frank Darabont",
                MovieSummary = "Over the course of several years, two convicts form a friendship, seeking consolation and, eventually, redemption through basic compassion.",
                MovieLength = 142,
                MovieType = "Drama",
                ContentAdminId = 0
            },
            new Movie {
                MovieId = 1,
                MovieName = "The Godfather",
                MovieContent = "Content",
                MovieDirector = "Francis Ford Coppola",
                MovieSummary = "Don Vito Corleone, head of a mafia family, decides to hand over his empire to his youngest son Michael. However, his decision unintentionally puts the lives of his loved ones in grave danger.",
                MovieLength = 175,
                MovieType = "Crime, Drama",
                ContentAdminId = 0
            },
            new Movie
            {
                MovieId = 2,
                MovieName = "The Dark Knight",
                MovieContent = "Content",
                MovieDirector = "Christopher Nolan",
                MovieSummary = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                MovieLength = 152,
                MovieType = "Action, Crime, Drama",
                ContentAdminId = 0
            }
            );

        modelBuilder.Entity<Cinema>().HasData(
            new Cinema { Id = 0, Name = "Village Cinemas Thessaloniki", Seats = 300, I3D = "Yes" },
            new Cinema { Id = 1, Name = "Options Cinemas Glyfada", Seats = 200, I3D = "No" },
            new Cinema { Id = 2, Name = "Άστορ", Seats = 150, I3D = "No" }
            );

        Provoli provoli;
        modelBuilder.Entity<Provoli>().HasData(
            provoli = new Provoli { MoviesId = 0, MoviesName = "The Shawshank Redemption", CinemasID = 0, Id = 0, ContentAdminId = 0, ShowDateTime = DateTime.Now.AddDays(5) },
            new Provoli { MoviesId = 0, MoviesName = "The Shawshank Redemption", CinemasID = 0, Id = 1, ContentAdminId = 0, ShowDateTime = DateTime.Now.AddDays(6).AddHours(7) },
            new Provoli { MoviesId = 0, MoviesName = "The Shawshank Redemption", CinemasID = 1, Id = 2, ContentAdminId = 0, ShowDateTime = DateTime.Now.AddDays(4).AddHours(7) },
            new Provoli { MoviesId = 1, MoviesName = "The Godfather", CinemasID = 2, Id = 3, ContentAdminId = 0, ShowDateTime = DateTime.Now.AddDays(5).AddHours(7) }
            );

        /*modelBuilder.Entity<Reservation>().HasData(
            new Reservation {
                ProvolesMoviesId = 0,
                ProvolesMoviesName = "The Shawshank Redemption",
                ProvolesCinemasId = 0,
                ProvolesDateTime = (DateTime)provoli.ShowDateTime,
                CustomersId = 0,
                Seats = "201",
                NumberOfSeats = 1}
            );*/
        // TODO Testing Data DELETE AFTER TESTING


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
