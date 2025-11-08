using CheckoutKata.Model.Discounts;
using CheckoutKata.Model.POCOs;
using MediatR;

namespace CheckoutKata.Model.Handlers;

/// <summary>
///     Performs the discount logic for the "Buy X, one proce discount"
/// </summary>
public class BuyXOnePriceDiscountHandler : IRequestHandler<BuyXOnePriceDiscount, DiscountResponse>
{
    /// <summary>
    ///     Performs the handle logic
    /// </summary>
    /// <param name="request">Request with criteria to perform the discount</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>The response with original or transformed data</returns>
    public Task<DiscountResponse> Handle(BuyXOnePriceDiscount request, CancellationToken cancellationToken)
    {
        var result = request.ProductItems;
        var rowsThatConformToCriteria = result.Where(p => p.Sku == request.Sku).Take(request.NumberOfItems).ToList();
        while (rowsThatConformToCriteria.Count == request.NumberOfItems)
        {
            result = result.Except(rowsThatConformToCriteria).ToList();
            result.Add(new ProductItem
            {
                Sku = Guid.NewGuid().ToString(),
                UnitPrice = request.CombinedPrice
            });
            rowsThatConformToCriteria = result.Where(p => p.Sku == request.Sku).Take(request.NumberOfItems).ToList();
        }

        return Task.FromResult(new DiscountResponse
        {
            ProductItems = result
        });
    }
}