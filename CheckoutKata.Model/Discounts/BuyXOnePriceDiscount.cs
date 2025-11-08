namespace CheckoutKata.Model.Discounts;

/// <summary>
///     A discount that will sell fixed quantity of product items with the same SKU for a combined price
/// </summary>
public class BuyXOnePriceDiscount : Discount
{
    /// <summary>
    ///     Sku of product item
    /// </summary>
    public string Sku { get; set; } = null!;

    /// <summary>
    ///     Number of items needed for the fixed price
    /// </summary>
    public int NumberOfItems { get; set; }

    /// <summary>
    ///     The combined price
    /// </summary>
    public decimal CombinedPrice { get; set; }
}