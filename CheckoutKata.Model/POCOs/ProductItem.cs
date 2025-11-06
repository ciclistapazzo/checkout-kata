namespace CheckoutKata.Model.POCOs;

/// <summary>
///     Something that you would buy at a checkout
/// </summary>
public class ProductItem
{
    /// <summary>
    ///     Stock control unit code
    /// </summary>
    public string Sku { get; set; } = null!;

    /// <summary>
    ///     The price of the product item
    /// </summary>
    public decimal UnitPrice { get; set; }
}