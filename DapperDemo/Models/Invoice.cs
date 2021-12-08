#nullable disable

[Table("Invoice")]
public partial class Invoice
{
    [ExplicitKey]
    public int InvoiceId { get; set; }
    public int CustomerId { get; set; }
    public System.DateTime InvoiceDate { get; set; }
    public string BillingAddress { get; set; }
    public string BillingCity { get; set; }
    public string BillingState { get; set; }
    public string BillingCountry { get; set; }
    public string BillingPostalCode { get; set; }
    public decimal Total { get; set; }

}
