using CheckoutKata.Model.POCOs;

namespace CheckoutKata.Model.Discounts;

/// <summary>
///     Information required to perform a discount
/// </summary>
public class DiscountResponse
{
    /// <summary>
    ///     Lis of product items to apply discount to
    /// </summary>
    public List<ProductItem> ProductItems { get; set; } = null!;
}