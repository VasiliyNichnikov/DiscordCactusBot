using System.Diagnostics;
using Discord.Audio;

namespace DiscordCactusBot.Core;

/// <summary>
/// Проигрыватель музыки для каждого канала
/// </summary>
public class Player
{
    public async Task PlayTestMusic(IAudioClient client)
    {
        var path = Utils.GetRootMusic(nameMusic: "test");
        
        using var ffmpeg = CreateStream(path);
        await using var output = ffmpeg.StandardOutput.BaseStream;
        await using var discord = client.CreatePCMStream(AudioApplication.Mixed);
        
        try
        {
            Console.WriteLine("Play/Next");
            await output.CopyToAsync(discord);
        }
        finally
        {
            await discord.FlushAsync();
        }
    }

    private Process CreateStream(string path)
    {
        Console.WriteLine($"Path: {path}");
        var processInfo = new ProcessStartInfo
        {
            FileName = "ffmpeg",
            Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
            UseShellExecute = false,
            RedirectStandardOutput = true
        };
        return Process.Start(processInfo);
    }
}