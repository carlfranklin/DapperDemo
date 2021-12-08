#nullable disable
[Table("PlayList")]
public partial class Playlist
{
    [ExplicitKey]
    public int PlaylistId { get; set; }
    public string Name { get; set; }
}
