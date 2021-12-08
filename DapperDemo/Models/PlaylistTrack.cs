#nullable disable

[Table("PlayListTrack")]
public partial class PlaylistTrack
{
    [ExplicitKey]
    public int PlaylistId { get; set; }
    public int TrackId { get; set; }
}
