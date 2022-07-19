using System.ComponentModel;
using Discord;
using Discord.Commands;
using DiscordCactusBot.Core;

namespace DiscordCactusBot.Commands;

public class Music: ModuleBase<SocketCommandContext>
{
    [Command("search", RunMode = RunMode.Async)]
    [Summary("Поиск трека/альбома/исполнителя по названию")]
    public async Task Search([Description("Название песни для поиска")] string search)
    {
        var channel = (Context.User as IGuildUser)?.VoiceChannel;

        if (channel == null)
        {
            // todo нужно вынести в config. Так же лучше сделать событие исключение, чтобы было проще обрабатывать все ошибки
            await Context.Channel.SendMessageAsync("Вы должны находиться в голосовом канале!");
            return;
        }
        
        var audioClient = await channel.ConnectAsync();
        
        var player = new Player();
        await player.PlayTestMusic(audioClient);
    }
}