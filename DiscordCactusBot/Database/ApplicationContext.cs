using DiscordCactusBot.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscordCactusBot.Database;

public sealed class ApplicationContext: DbContext
{
    public DbSet<Channel> Channels { get; set; }
    private string _nameDB = "DiscordDB";

    public async Task CheckDataBase()
    {
        // Проверяет наличие БД и если ее нет, создает
        await Database.EnsureCreatedAsync();
    }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Console.WriteLine("Application context: OnConfiguring");
        // optionsBuilder.UseSqlServer("Server=localhost;Database=master;Trusted_Connection=True;");
        optionsBuilder.UseSqlServer(@$"Server=(localdb)\MSSQLLocalDB;Database={_nameDB};Trusted_Connection=True;");
    }
}