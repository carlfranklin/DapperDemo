#nullable disable

[Table("Album")]
public partial class Album
{
    [ExplicitKey]
    public int AlbumId { get; set; }
    public string Title { get; set; }
    public int ArtistId { get; set; }
}
