using System.Text;

namespace DiscordCactusBot.Core;

// todo вынести все работы с .env в отдельный класс
public static class Utils
{
    static Utils()
    {
        LoadEnv();
    }
    
    /// <summary>
    /// Получение DISCORD токена для работы бота
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string GetDiscordToken()
    {
        var discordToken = GetValueFromEnvByKey("DISCORD-TOKEN");
        return discordToken;
    }
    
    /// <summary>
    /// Возвращает путь до конфига json
    /// </summary>
    /// <returns></returns>
    public static string GetRootConfig()
    {
        var rootProject = GetRootProject();
        var combinePath = Path.Combine(rootProject, "config.json");
        return combinePath;
    }

    /// <summary>
    /// Возвращает информацию для подключения к базе данных 
    /// </summary>
    /// <returns></returns>
    public static string GetInfoForConnectToDatabase()
    {
        var host = GetValueFromEnvByKey("DATABASE-HOST");
        var user = GetValueFromEnvByKey("DATABASE-USER");
        var name = GetValueFromEnvByKey("DATABASE-NAME");
        var port = GetValueFromEnvByKey("DATABASE-PORT");
        var password = GetValueFromEnvByKey("DATABASE-PASSWORD");
        
        return $"Host={host};Port={port};Database={name};Username={user};Password={password}";
    }
    
    public static string GetRootMusic(string nameMusic)
    {
        var rootProject = GetRootProject();
        var combinedPath = Path.Combine(rootProject, @$"static\music\{nameMusic}.mp3");
        return combinedPath;
    }

    private static string GetValueFromEnvByKey(string key)
    {
        var value = Environment.GetEnvironmentVariable(key);
        if (value is null)
            throw new Exception(); // todo добавить фильтр ошибке
        return value;
    }
    
    /// <summary>
    /// Загрузка env для работы со скрытыми данными
    /// </summary>
    private static void LoadEnv()
    {
        var rootProject = GetRootProject();
        
        DotNetEnv.Env.Load(Path.Combine(rootProject, ".env"));
    }
    
    private static string GetRootProject()
    {
        return Directory.GetCurrentDirectory();
    }
}