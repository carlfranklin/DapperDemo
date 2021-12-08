#nullable disable
[Table("Instrument")]
public class Instrument
{
    [Key]
    public int InstrumentId { get; set; }

    public string Name { get; set; }
}
