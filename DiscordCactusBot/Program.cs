using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordCactusBot.Commands;
using DiscordCactusBot.Core;
using DiscordCactusBot.Database;

namespace DiscordCactusBot;

public class Program
{
    private DiscordSocketClient _client;
    private CommandService _commands;
    
    
    public static Task Main(string[] args) => new Program().MainAsync();
    
    private async Task MainAsync()
    {
        var discordConfig = new DiscordSocketConfig { MessageCacheSize = 100 };
        _client = new DiscordSocketClient(discordConfig);
        
        _commands = new CommandService();
        var commandHandler = new CommandHandler(_client, _commands);
        
        _client.Log += Log;
        _commands.Log += Log;

        var token = Utils.GetDiscordToken();
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();
        await commandHandler.InstallCommandsAsync();

        // Utils.GetInfoForConnectToDatabase();
        // await using (var db = new ApplicationContext())
        // {
        //     
        // }
        // {
        //     // var channel1 = new ChannelModel {Name = "Channel1"};
        //     // var channel2 = new ChannelModel {Name = "Channel2"};
        //     //
        //     // db.Channels.Add(channel1);
        //     // db.Channels.Add(channel2);
        //     //
        //     // await db.SaveChangesAsync();
        //     //
        //     // var channels = db.Channels.ToList();
        //     // Console.WriteLine($"Список объектов: {channels.Count}");
        //     // foreach (var channel in channels)
        //     // {
        //     //     Console.WriteLine($"Data. Id: {channel.Id}. Name: {channel.Name}");
        //     // }
        // }
        
        _client.Ready += () =>
        {
            Console.WriteLine("Bot is connected");
            return Task.CompletedTask;
        };

        await Task.Delay(-1);
    }

    private Task Log(LogMessage message)
    {
        Console.WriteLine(message.ToString());
        return Task.CompletedTask;
    }
}