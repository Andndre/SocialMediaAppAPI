﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocialMediaAppAPI.Data;

#nullable disable

namespace SocialMediaAppAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240615121707_NotNull")]
    partial class NotNull
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SocialMediaAppAPI.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("SocialMediaAppAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SocialMediaAppAPI.Models.UserLikedPost", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PostId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserLikedPosts");
                });

            modelBuilder.Entity("SocialMediaAppAPI.Models.Post", b =>
                {
                    b.HasOne("SocialMediaAppAPI.Models.User", "Owner")
                        .WithMany("Posts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SocialMediaAppAPI.Models.UserLikedPost", b =>
                {
                    b.HasOne("SocialMediaAppAPI.Models.Post", "Post")
                        .WithMany("UserLikedPosts")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaAppAPI.Models.User", "User")
                        .WithMany("UserLikedPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SocialMediaAppAPI.Models.Post", b =>
                {
                    b.Navigation("UserLikedPosts");
                });

            modelBuilder.Entity("SocialMediaAppAPI.Models.User", b =>
                {
                    b.Navigation("Posts");

                    b.Navigation("UserLikedPosts");
                });
#pragma warning restore 612, 618
        }
    }
}
