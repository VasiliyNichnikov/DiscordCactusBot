using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordCactusBot.Models;

[Table("TrackHistory")]
public class TrackHistoryModel : BaseModel
{
    public List<TrackModel> Tracks { get; set; } = new();
    public int ChannelModelId { get; set; }
}