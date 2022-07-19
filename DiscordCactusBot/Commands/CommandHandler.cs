using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
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
            var textError = "";
            switch (result.Error)
            {
                case CommandError.UnknownCommand:
                    textError = "Упс! Команда не определена. Введите **!help**, чтобы посмотреть все команды";
                    break;
                case CommandError.ParseFailed:
                    textError = "Упс! Команда не пронализированна!";
                    break;
                case CommandError.BadArgCount:
                    textError = "Упс! Вы передали не верные аргументы!";
                    break;
                case CommandError.ObjectNotFound:
                    textError = "Упс! Что-то пошло не так (ObjectNotFound)";
                    break;
                case CommandError.MultipleMatches:
                    textError = "Упс! Что-то пошло не так (MultipleMatches)";
                    break;
                case CommandError.UnmetPrecondition:
                    textError = "Упс! Что-то пошло не так (UnmetPrecondition)";
                    break;
                case CommandError.Exception:
                    textError = "Упс! Во время выполнения команды что-то пошло не так(";
                    break;
                case CommandError.Unsuccessful:
                    textError = "Упс! Команда завершилась не верно (";
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