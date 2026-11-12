namespace BikeShopManager.Domain.Entities.Stock;

public class VatRate : Audit
{
    public int Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public decimal Rate { get; set; }
}
