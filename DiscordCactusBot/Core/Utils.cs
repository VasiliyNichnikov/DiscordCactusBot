namespace DiscordCactusBot.Core;

public static class Utils
{
    /// <summary>
    /// Загрузка env для работы со скрытыми данными
    /// </summary>
    public static void LoadEnv()
    {
        var rootProject = GetRootProject();
        DotNetEnv.Env.Load(Path.Combine(rootProject, ".env"));
    }
    
    /// <summary>
    /// Получение DISCORD токена для работы бота
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string GetDiscordToken()
    {
        var discordToken = Environment.GetEnvironmentVariable("DISCORD-TOKEN");
        if (discordToken is null)
            throw new Exception(); // todo добавить фильтр ошибке
        return discordToken;
    }

    public static string GetRootMusic(string nameMusic)
    {
        var rootProject = GetRootProject();
        var combinedPath = Path.Combine(rootProject, @$"static\music\{nameMusic}.mp3");
        return combinedPath;
    }
    
    private static string GetRootProject()
    {
        var path = Directory.GetCurrentDirectory();
        return Directory.GetParent(path).Parent.Parent.FullName;
    }
}