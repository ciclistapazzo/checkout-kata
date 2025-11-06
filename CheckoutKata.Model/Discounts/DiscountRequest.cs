using CheckoutKata.Model.POCOs;

namespace CheckoutKata.Model.Discounts;

/// <summary>
///     Information required to perform a discount
/// </summary>
public class DiscountRequest
{
    /// <summary>
    ///     Name of discount eg. Buy multiple for one price
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Lis of product items to apply discount to
    /// </summary>
    public List<ProductItem> ProductItems { get; set; } = null!;
}