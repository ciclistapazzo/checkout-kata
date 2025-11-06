using CheckoutKata.Model.Discounts;
using CheckoutKata.Model.DomainServices;
using CheckoutKata.Model.Repositories;
using MediatR;
using Moq;

namespace CheckoutKata.Model.Test.DomainServices;

/// <summary>
///     Test the CheckoutService
/// </summary>
public class CheckoutServiceTest
{
    /// <summary>
    ///     Confirm that the first time round a scan is called, it gets the discounts
    /// </summary>
    [Fact]
    public async Task CheckoutService_ScanAsync_FirstTimeRound_GetsDiscounts()
    {
        var sku = "A";
        var discountRepositoryMock = new Mock<IDiscountRepository>();

        var checkoutService = new CheckoutService(It.IsAny<IMediator>(), discountRepositoryMock.Object);
        await checkoutService.ScanAsync(sku);
        discountRepositoryMock.Verify(d => d.GetDiscountsAsync(), Times.Once);
    }

    /// <summary>
    ///     Confirm that if there are 2 scans, discounts are only retrieved once it gets the discounts
    /// </summary>
    [Fact]
    public async Task CheckoutService_ScanAsync_CalledTwice_OnlyGetsDiscountsOnce()
    {
        var sku1 = "A";
        var sku2 = "B";
        var discountRepositoryMock = new Mock<IDiscountRepository>();
        discountRepositoryMock.Setup(d => d.GetDiscountsAsync()).ReturnsAsync(new List<IRequest<DiscountRequest>>
            { new BuyXOnePriceDiscount() });
        var checkoutService = new CheckoutService(It.IsAny<IMediator>(), discountRepositoryMock.Object);
        await checkoutService.ScanAsync(sku1);
        await checkoutService.ScanAsync(sku2);
        discountRepositoryMock.Verify(d => d.GetDiscountsAsync(), Times.Once);
    }
}