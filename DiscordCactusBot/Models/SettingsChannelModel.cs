using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordCactusBot.Models;

[Table("SettingsChannel")]
public class SettingsChannelModel : BaseModel
{
    public string DefaultNameTextChannel { get; set; }
    public int ChannelModelId { get; set; }
}