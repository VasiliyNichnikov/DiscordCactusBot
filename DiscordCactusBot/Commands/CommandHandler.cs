using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordCactusBot.Core;
using IResult = Discord.Commands.IResult;

namespace DiscordCactusBot.Commands;

public class CommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly CommandService _commands;

    public CommandHandler(DiscordSocketClient client, CommandService commands)
    {
        _client = client;
        _commands = commands;
    }

    public async Task InstallCommandsAsync()
    {
        _client.MessageReceived += HandleCommandAsync;

        await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
            services: null);

        _commands.CommandExecuted += ComponentCommandExecuted;
    }
    
    
    // todo вынести в отдельный класс
    private async Task HandleCommandAsync(SocketMessage messageParameter)
    {
        if (messageParameter is not SocketUserMessage message) return;

        var argPosition = 0;
        
        // todo название канала нужно брать из базы данных
        if ((message.HasCharPrefix('!', ref argPosition) ||
             message.HasMentionPrefix(_client.CurrentUser, ref argPosition)) == false ||
            message.Author.IsBot || message.Channel.Name.ToLower() != "музыка") return;

        var context = new SocketCommandContext(_client, message);

        await _commands.ExecuteAsync(
            context: context,
            argPos: argPosition,
            services: null);
    }
    
    
    // todo вынести в отдельный класс
    private async Task ComponentCommandExecuted(Optional<CommandInfo> command, ICommandContext context, IResult result)
    {
        // todo нужно вынести все данные в config
        // Проверка ошибок
        if (string.IsNullOrEmpty(result.ErrorReason) == false)
        {
            var config = Config.GetInstance().Deserialize.Messages!.Errors;
            var textError = "";
            switch (result.Error)
            {
                case CommandError.UnknownCommand:
                    textError = config?.UnknownCommand;
                    break;
                case CommandError.ParseFailed:
                    textError = config?.ParseFailed;
                    break;
                case CommandError.BadArgCount:
                    textError = config?.BadArgCount;
                    break;
                case CommandError.ObjectNotFound:
                    textError = config?.ObjectNotFound;
                    break;
                case CommandError.MultipleMatches:
                    textError = config?.MultipleMatches;
                    break;
                case CommandError.UnmetPrecondition:
                    textError = config?.UnmetPrecondition;
                    break;
                case CommandError.Exception:
                    textError = config?.Exception;
                    break;
                case CommandError.Unsuccessful:
                    textError = config?.Unsuccessful;
                    break;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            await context.Channel.SendMessageAsync(textError);
        }
    }
    
}