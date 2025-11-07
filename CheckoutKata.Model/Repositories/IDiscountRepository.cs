using CheckoutKata.Model.Discounts;
using MediatR;

namespace CheckoutKata.Model.Repositories;

/// <summary>
///     Responsible for retrieval of discounts
/// </summary>
public interface IDiscountRepository
{
    /// <summary>
    ///     Gets the discounts
    /// </summary>
    /// <returns>List of discounts</returns>
    Task<List<Discount>?> GetDiscountsAsync();
}