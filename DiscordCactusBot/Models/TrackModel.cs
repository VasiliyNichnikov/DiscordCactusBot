using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordCactusBot.Models;

[Table("Track")]
public class TrackModel : BaseModel
{
    public string Name { get; set; }
    public string Album { get; set; }
    public string Performers { get; set; }
    [Required]
    public byte Source { get; set; }
    public DateTime LastPlayback { get; set; }
    
    // Связь один-к-многим
    public int TrackHistoryModelId { get; set; }
    public TrackHistoryModel TrackHistory { get; set; }
}