﻿// <auto-generated />
using FrenchBulldogs.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FrenchBulldogs.Migrations
{
    [DbContext(typeof(FrenchBulldogDbContext))]
    partial class FrenchBulldogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FrenchBulldogs.Data.Models.FrenchBulldog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("FrenchBulldogs");
                });

            modelBuilder.Entity("FrenchBulldogs.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FrenchBulldogs.Data.Models.UserFrenchBulldog", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("FrenchBulldogId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("FrenchBulldogId");

                    b.HasIndex("UserId");

                    b.ToTable("UserFrenchBulldogs");
                });

            modelBuilder.Entity("FrenchBulldogs.Data.Models.UserFrenchBulldog", b =>
                {
                    b.HasOne("FrenchBulldogs.Data.Models.FrenchBulldog", "FrenchBulldog")
                        .WithMany("UserFrenchBulldog")
                        .HasForeignKey("FrenchBulldogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FrenchBulldogs.Data.Models.User", "User")
                        .WithMany("UserFrenchBulldog")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FrenchBulldog");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FrenchBulldogs.Data.Models.FrenchBulldog", b =>
                {
                    b.Navigation("UserFrenchBulldog");
                });

            modelBuilder.Entity("FrenchBulldogs.Data.Models.User", b =>
                {
                    b.Navigation("UserFrenchBulldog");
                });
#pragma warning restore 612, 618
        }
    }
}
