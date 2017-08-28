﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using Travels.Data;

namespace Travels.Migrations
{
    [DbContext(typeof(TravelContext))]
    [Migration("20170827114654_CreateToursExcursions")]
    partial class CreateToursExcursions
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Travels.Models.Clients", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientName");

                    b.HasKey("ID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Travels.Models.Excursion_Sights", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ExcursionName");

                    b.HasKey("ID");

                    b.ToTable("Excursion_Sights");
                });

            modelBuilder.Entity("Travels.Models.Tours", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("TourName");

                    b.HasKey("ID");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("Travels.Models.Tours_Clients", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientsID");

                    b.Property<int>("ToursID");

                    b.HasKey("ID");

                    b.HasIndex("ClientsID");

                    b.HasIndex("ToursID");

                    b.ToTable("Tours_Clients");
                });

            modelBuilder.Entity("Travels.Models.Tours_Excursions", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Excursion_SightsID");

                    b.Property<int>("ToursID");

                    b.HasKey("ID");

                    b.HasIndex("Excursion_SightsID");

                    b.HasIndex("ToursID");

                    b.ToTable("Tours_Excursions");
                });

            modelBuilder.Entity("Travels.Models.Tours_Clients", b =>
                {
                    b.HasOne("Travels.Models.Clients", "Clients")
                        .WithMany("Tours_Clients")
                        .HasForeignKey("ClientsID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Travels.Models.Tours", "Tours")
                        .WithMany("Tours_Clients")
                        .HasForeignKey("ToursID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Travels.Models.Tours_Excursions", b =>
                {
                    b.HasOne("Travels.Models.Excursion_Sights", "Excursion_Sights")
                        .WithMany("Tours_Excursions")
                        .HasForeignKey("Excursion_SightsID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Travels.Models.Tours", "Tours")
                        .WithMany("Tours_Excursions")
                        .HasForeignKey("ToursID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
