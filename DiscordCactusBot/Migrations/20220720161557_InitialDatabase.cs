using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiscordCactusBot.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Channel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChannelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettingsChannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DefaultNameTextChannel = table.Column<string>(type: "text", nullable: false),
                    ChannelModelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingsChannel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettingsChannel_Channel_ChannelModelId",
                        column: x => x.ChannelModelId,
                        principalTable: "Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrackHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChannelModelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackHistory_Channel_ChannelModelId",
                        column: x => x.ChannelModelId,
                        principalTable: "Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Track",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Album = table.Column<string>(type: "text", nullable: false),
                    Performers = table.Column<string>(type: "text", nullable: false),
                    Source = table.Column<byte>(type: "smallint", nullable: false),
                    LastPlayback = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TrackHistoryModelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Track", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Track_TrackHistory_TrackHistoryModelId",
                        column: x => x.TrackHistoryModelId,
                        principalTable: "TrackHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SettingsChannel_ChannelModelId",
                table: "SettingsChannel",
                column: "ChannelModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Track_TrackHistoryModelId",
                table: "Track",
                column: "TrackHistoryModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackHistory_ChannelModelId",
                table: "TrackHistory",
                column: "ChannelModelId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SettingsChannel");

            migrationBuilder.DropTable(
                name: "Track");

            migrationBuilder.DropTable(
                name: "TrackHistory");

            migrationBuilder.DropTable(
                name: "Channel");
        }
    }
}
