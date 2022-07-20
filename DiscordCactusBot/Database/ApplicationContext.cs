using DiscordCactusBot.Core;
using DiscordCactusBot.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscordCactusBot.Database;

public sealed class ApplicationContext: DbContext
{
    public DbSet<ChannelModel> Channels { get; set; }
    public DbSet<SettingsChannelModel> Settings { get; set; }
    public DbSet<TrackHistoryModel> TrackHistory { get; set; }
    public DbSet<TrackModel> Tracks { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Utils.GetInfoForConnectToDatabase();
        optionsBuilder.UseNpgsql(connectionString);
    }
}