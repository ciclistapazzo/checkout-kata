namespace CheckoutKata.Model.POCOs;

/// <summary>
///     Something that you would buy at a checkout
/// </summary>
public class ProductItem
{
    /// <summary>
    /// 
    /// </summary>
    public string Sku { get; set; } = null!;
    public decimal UnitPrice { get; set; }

}