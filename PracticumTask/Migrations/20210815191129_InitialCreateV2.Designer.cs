﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PracticumTask.Models;

namespace PracticumTask.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20210815191129_InitialCreateV2")]
    partial class InitialCreateV2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("BookGenre", b =>
                {
                    b.Property<int>("BooksId")
                        .HasColumnType("integer");

                    b.Property<int>("GenresId")
                        .HasColumnType("integer");

                    b.HasKey("BooksId", "GenresId");

                    b.HasIndex("GenresId");

                    b.ToTable("BookGenre");
                });

            modelBuilder.Entity("BookPerson", b =>
                {
                    b.Property<int>("BooksId")
                        .HasColumnType("integer");

                    b.Property<int>("PeopleId")
                        .HasColumnType("integer");

                    b.HasKey("BooksId", "PeopleId");

                    b.HasIndex("PeopleId");

                    b.ToTable("BookPerson");
                });

            modelBuilder.Entity("PracticumTask.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Иван",
                            LastName = "Иванов",
                            MiddleName = "Иванович"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Пётр",
                            LastName = "Петров"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Тумба",
                            LastName = "Юмба"
                        });
                });

            modelBuilder.Entity("PracticumTask.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            Title = "На волнах галоперидола"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 2,
                            Title = "Плачут ли программисты"
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 3,
                            Title = "Подебажим?"
                        });
                });

            modelBuilder.Entity("PracticumTask.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Приключения"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Фантастика"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Роман"
                        });
                });

            modelBuilder.Entity("PracticumTask.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("Birthdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("People");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Birthdate = new DateTime(1943, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Сергей",
                            LastName = "Драгун",
                            MiddleName = "Автоматов"
                        },
                        new
                        {
                            Id = 2,
                            Birthdate = new DateTime(1985, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Дмитрий",
                            LastName = "Пушкин"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Александр",
                            LastName = "Чехов",
                            MiddleName = "Алексеевич"
                        });
                });

            modelBuilder.Entity("BookGenre", b =>
                {
                    b.HasOne("PracticumTask.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PracticumTask.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookPerson", b =>
                {
                    b.HasOne("PracticumTask.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PracticumTask.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("PeopleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PracticumTask.Models.Book", b =>
                {
                    b.HasOne("PracticumTask.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("PracticumTask.Models.Author", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
