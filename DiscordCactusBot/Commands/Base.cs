using Discord;
using Discord.Commands;

namespace DiscordCactusBot.Commands;

public class Base : ModuleBase<SocketCommandContext>
{
    private readonly CommandService _service;

    public Base(CommandService service)
    {
        _service = service;
    }
    
    /// <summary>
    /// Описывает все команды, которые есть у бота
    /// </summary>
    [Command("help")]
    public async Task Help()
    {
        var embedBuilder = new EmbedBuilder
        {
            Color = new Color(255, 153, 0)
        };
        
        foreach (var module in _service.Modules)
        {
            foreach (var command in module.Commands)
            {
                var result = await command.CheckPreconditionsAsync(Context);
                if (result.IsSuccess)
                {
                    var descriptionCommand = command.Summary ?? "У команды нет описания\n";

                    embedBuilder.AddField(x =>
                    {
                        x.Name = command.Name;
                        x.Value = descriptionCommand;
                        x.IsInline = false;
                    });
                }
            }
        }

        await ReplyAsync("СПИСОК КОМАНД",false, embedBuilder.Build());
    }
}