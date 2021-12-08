#nullable disable

[Table("Genre")]
public partial class Genre
{
    [ExplicitKey]
    public int GenreId { get; set; }
    public string Name { get; set; }
}
