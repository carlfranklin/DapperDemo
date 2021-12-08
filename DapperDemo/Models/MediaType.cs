#nullable disable
[Table("MediaType")]
public partial class MediaType
{
    [ExplicitKey]
    public int MediaTypeId { get; set; }
    public string Name { get; set; }

}
