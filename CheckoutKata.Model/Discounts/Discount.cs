using CheckoutKata.Model.POCOs;
using MediatR;

namespace CheckoutKata.Model.Discounts;

public class Discount : IRequest<DiscountResponse>
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