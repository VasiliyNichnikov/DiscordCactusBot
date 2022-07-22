using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordCactusBot.Models;

[Table("Channel")]
public class ChannelModel : BaseModel
{
    [Required] // Устанавливается в том случае, если свойство является обязательным
    public int ChannelId { get; set; }
    public SettingsChannelModel Settings { get; set; }
    public TrackHistoryModel History { get; set; }
}