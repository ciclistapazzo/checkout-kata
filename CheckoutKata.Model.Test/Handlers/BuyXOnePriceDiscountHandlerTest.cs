using CheckoutKata.Model.Discounts;
using CheckoutKata.Model.Handlers;
using CheckoutKata.Model.POCOs;

namespace CheckoutKata.Model.Test.Handlers;

/// <summary>
///     Tests the BuyXOnePriceDiscountHandler
/// </summary>
public class BuyXOnePriceDiscountHandlerTest
{
    /// <summary>
    ///     Confirms that if the provided data does not match the default criteria then don't apply discount transformation
    /// </summary>
    [Fact]
    public async Task BuyXOnePriceDiscountHandler_Handle_CriteriaNotMatched_NothingDone()
    {
        var singleUnitPrice = 50.00m;
        var productItem = new ProductItem
        {
            Sku = "A",
            UnitPrice = singleUnitPrice
        };
        var buyXOnePriceDiscount = new BuyXOnePriceDiscount
        {
            Name = "A 3 for 130",
            CombinedPrice = 130.00m,
            Sku = "A",
            NumberOfItems = 3,
            ProductItems = new List<ProductItem>
            {
                productItem,
                productItem
            }
        };
        var handler = new BuyXOnePriceDiscountHandler();
        var response = await handler.Handle(buyXOnePriceDiscount, CancellationToken.None);
        Assert.Equal(2, response.ProductItems.Count);
        Assert.Equal(100.00m, response.ProductItems.Sum(p => p.UnitPrice));
    }

    /// <summary>
    ///     Confirms that if the provided data matches the discount criteria then the discount transformation is applied
    /// </summary>
    [Fact]
    public async Task BuyXOnePriceDiscountHandler_Handle_CriteriaMatches_DiscountTransformationDone()
    {
        var firstProductItem = new ProductItem
        {
            Sku = "A",
            UnitPrice = 50.00m
        };
        var secondProductItem = new ProductItem
        {
            Sku = "B",
            UnitPrice = 30.00m
        };
        var buyXOnePriceDiscount = new BuyXOnePriceDiscount
        {
            Name = "A 3 for 130",
            CombinedPrice = 130.00m,
            Sku = "A",
            NumberOfItems = 3,
            ProductItems = new List<ProductItem>
            {
                firstProductItem,
                firstProductItem,
                secondProductItem,
                firstProductItem,
                secondProductItem,
                firstProductItem
            }
        };
        var handler = new BuyXOnePriceDiscountHandler();
        var response = await handler.Handle(buyXOnePriceDiscount, CancellationToken.None);
        Assert.Equal(4, response.ProductItems.Count);
        Assert.Equal(240.00m, response.ProductItems.Sum(p => p.UnitPrice));
    }
}