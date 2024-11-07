﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using P2.API.Data;

#nullable disable

namespace P2.API.Migrations
{
    [DbContext(typeof(BacklogContext))]
    [Migration("20241105143416_changedimageurl")]
    partial class changedimageurl
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("P2.API.Model.Backlog", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<bool>("Completed")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CompletionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "GameId");

                    b.ToTable("Backlogs");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            GameId = 1,
                            Completed = false
                        });
                });

            modelBuilder.Entity("P2.API.Model.Game", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "summary");

                    b.Property<int>("ImageURLId")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "image_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<double>("Rating")
                        .HasColumnType("float")
                        .HasAnnotation("Relational:JsonPropertyName", "total_rating");

                    b.HasKey("GameId");

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            GameId = 1,
                            Description = "",
                            ImageURLId = 0,
                            Name = "Sample Fake game",
                            Rating = 0.0
                        },
                        new
                        {
                            GameId = 2,
                            Description = "Investigating a letter from his late wife, James returns to where they made so many memories - Silent Hill.",
                            ImageURLId = 0,
                            Name = "Sample Fake Game 2",
                            Rating = 0.0
                        });
                });

            modelBuilder.Entity("P2.API.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Password = "Password",
                            UserName = "Alfredo"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
