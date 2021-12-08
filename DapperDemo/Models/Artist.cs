#nullable disable
[Table("Artist")]
public partial class Artist
{
    [ExplicitKey]
    public int ArtistId { get; set; }
    public string Name { get; set; }
}
