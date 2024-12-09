﻿using Microsoft.EntityFrameworkCore;
using HotelBookingDetails.Domain.Entity;

namespace HotelBookingDetails.Domain.Context;

/// <summary>
///     Класс для доступа к базе данных
/// </summary>
public class HotelBookingDbContext(DbContextOptions<HotelBookingDbContext> options) : DbContext(options)
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Passport> Passports { get; set; }
    public DbSet<ReservedRooms> ReservedRooms { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<TypeRoom> TypeRooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>()
             .HasOne(p => p.PassportData)
                .WithMany()
                .HasForeignKey("passport_id")
                .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<ReservedRooms>()
            .HasOne(p => p.Client)
                .WithMany()
                .HasForeignKey("client_id")
                .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<ReservedRooms>()
            .HasOne(p => p.Room)
                .WithMany()
                .HasForeignKey("room_id")
                .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Room>()
           .HasOne(p => p.Type)
               .WithMany()
               .HasForeignKey("type_id")
               .OnDelete(DeleteBehavior.Cascade);
    }
}
