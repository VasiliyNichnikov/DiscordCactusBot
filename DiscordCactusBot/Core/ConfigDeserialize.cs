using System.Text.Json;

namespace DiscordCactusBot.Core;

public class Errors
{
    public string UnknownCommand { get; set; }
    public string ParseFailed { get; set; }
    public string BadArgCount { get; set; }
    public string ObjectNotFound { get; set; }
    public string MultipleMatches { get; set; }
    public string UnmetPrecondition { get; set; }
    public string Exception { get; set; }
    public string Unsuccessful { get; set; }
}

public class Messages
{
    public Errors? Errors { get; set; }
}

public class ConfigDeserialize
{
    public Messages? Messages { get; set; }
}

public class Config
{
    public ConfigDeserialize Deserialize { get; }
    
    private static Config _instance;

    private Config(ConfigDeserialize deserialize)
    {
        Deserialize = deserialize;
    }

    public static Config GetInstance()
    {
        if (_instance == null)
        {
            Console.WriteLine("New instance");
            var pathConfig = Utils.GetRootConfig();
            var jsonString = File.ReadAllText(pathConfig);
            var deserialize = JsonSerializer.Deserialize<ConfigDeserialize>(jsonString);
            _instance = new Config(deserialize!);
        }

        return _instance;
    }
}