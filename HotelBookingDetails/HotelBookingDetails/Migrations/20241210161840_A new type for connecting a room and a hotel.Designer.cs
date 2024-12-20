﻿// <auto-generated />
using System;
using HotelBookingDetails.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HotelBookingDetails.Domain.Migrations
{
    [DbContext(typeof(HotelBookingDbContext))]
    [Migration("20241210161840_A new type for connecting a room and a hotel")]
    partial class Anewtypeforconnectingaroomandahotel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HotelBookingDetails.Domain.Entity.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("Birthday")
                        .IsRequired()
                        .HasColumnType("date")
                        .HasColumnName("birthday");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("full_name");

                    b.Property<int>("passport_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("passport_id");

                    b.ToTable("client");
                });

            modelBuilder.Entity("HotelBookingDetails.Domain.Entity.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("hotel");
                });

            modelBuilder.Entity("HotelBookingDetails.Domain.Entity.Passport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.Property<int>("Series")
                        .HasColumnType("integer")
                        .HasColumnName("series");

                    b.HasKey("Id");

                    b.ToTable("passport");
                });

            modelBuilder.Entity("HotelBookingDetails.Domain.Entity.ReservedRooms", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("DateArrival")
                        .HasColumnType("date")
                        .HasColumnName("date_arrival");

                    b.Property<DateOnly?>("DateDeparture")
                        .HasColumnType("date")
                        .HasColumnName("date_departure");

                    b.Property<int>("Period")
                        .HasColumnType("integer")
                        .HasColumnName("period");

                    b.Property<int>("client_id")
                        .HasColumnType("integer");

                    b.Property<int>("room_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("client_id");

                    b.HasIndex("room_id");

                    b.ToTable("reserved_room");
                });

            modelBuilder.Entity("HotelBookingDetails.Domain.Entity.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("integer")
                        .HasColumnName("capacity");

                    b.Property<int>("Cost")
                        .HasColumnType("integer")
                        .HasColumnName("cost");

                    b.Property<int>("hotel_id")
                        .HasColumnType("integer");

                    b.Property<int>("type_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("hotel_id");

                    b.HasIndex("type_id");

                    b.ToTable("room");
                });

            modelBuilder.Entity("HotelBookingDetails.Domain.Entity.TypeRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("type_room");
                });

            modelBuilder.Entity("HotelBookingDetails.Domain.Entity.Client", b =>
                {
                    b.HasOne("HotelBookingDetails.Domain.Entity.Passport", "PassportData")
                        .WithMany()
                        .HasForeignKey("passport_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PassportData");
                });

            modelBuilder.Entity("HotelBookingDetails.Domain.Entity.ReservedRooms", b =>
                {
                    b.HasOne("HotelBookingDetails.Domain.Entity.Client", "Client")
                        .WithMany()
                        .HasForeignKey("client_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelBookingDetails.Domain.Entity.Room", "Room")
                        .WithMany()
                        .HasForeignKey("room_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HotelBookingDetails.Domain.Entity.Room", b =>
                {
                    b.HasOne("HotelBookingDetails.Domain.Entity.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("hotel_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelBookingDetails.Domain.Entity.TypeRoom", "Type")
                        .WithMany()
                        .HasForeignKey("type_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("Type");
                });
#pragma warning restore 612, 618
        }
    }
}
