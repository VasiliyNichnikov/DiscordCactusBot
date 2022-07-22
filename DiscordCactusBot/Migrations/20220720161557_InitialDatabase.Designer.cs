﻿// <auto-generated />
using System;
using DiscordCactusBot.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiscordCactusBot.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220720161557_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DiscordCactusBot.Models.ChannelModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChannelId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Channel");
                });

            modelBuilder.Entity("DiscordCactusBot.Models.SettingsChannelModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChannelModelId")
                        .HasColumnType("integer");

                    b.Property<string>("DefaultNameTextChannel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChannelModelId")
                        .IsUnique();

                    b.ToTable("SettingsChannel");
                });

            modelBuilder.Entity("DiscordCactusBot.Models.TrackHistoryModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChannelModelId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChannelModelId")
                        .IsUnique();

                    b.ToTable("TrackHistory");
                });

            modelBuilder.Entity("DiscordCactusBot.Models.TrackModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Album")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastPlayback")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Performers")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte>("Source")
                        .HasColumnType("smallint");

                    b.Property<int>("TrackHistoryModelId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TrackHistoryModelId");

                    b.ToTable("Track");
                });

            modelBuilder.Entity("DiscordCactusBot.Models.SettingsChannelModel", b =>
                {
                    b.HasOne("DiscordCactusBot.Models.ChannelModel", null)
                        .WithOne("Settings")
                        .HasForeignKey("DiscordCactusBot.Models.SettingsChannelModel", "ChannelModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiscordCactusBot.Models.TrackHistoryModel", b =>
                {
                    b.HasOne("DiscordCactusBot.Models.ChannelModel", null)
                        .WithOne("History")
                        .HasForeignKey("DiscordCactusBot.Models.TrackHistoryModel", "ChannelModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiscordCactusBot.Models.TrackModel", b =>
                {
                    b.HasOne("DiscordCactusBot.Models.TrackHistoryModel", "TrackHistory")
                        .WithMany("Tracks")
                        .HasForeignKey("TrackHistoryModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TrackHistory");
                });

            modelBuilder.Entity("DiscordCactusBot.Models.ChannelModel", b =>
                {
                    b.Navigation("History")
                        .IsRequired();

                    b.Navigation("Settings")
                        .IsRequired();
                });

            modelBuilder.Entity("DiscordCactusBot.Models.TrackHistoryModel", b =>
                {
                    b.Navigation("Tracks");
                });
#pragma warning restore 612, 618
        }
    }
}
