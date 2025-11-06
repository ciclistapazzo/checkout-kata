using CheckoutKata.Model.Discounts;
using CheckoutKata.Model.DomainServices;
using CheckoutKata.Model.Exceptions;
using CheckoutKata.Model.POCOs;
using CheckoutKata.Model.Repositories;
using MediatR;
using Moq;

namespace CheckoutKata.Model.Test.DomainServices;

/// <summary>
///     Test the CheckoutService
/// </summary>
public class CheckoutServiceTest
{
    #region GetTotalPrice

    /// <summary>
    ///     Confirms that when GetTotalPrice is called.  It calls the mediator to processes discounts
    /// </summary>
    [Fact]
    public async Task CheckoutService_GetTotalPrice_CallsMediatorWithTheDiscount()
    {
        var firstProductItem = new ProductItem
        {
            Sku = "A",
            UnitPrice = 50.00m
        };
        var mediatorMock = new Mock<IMediator>();
        var discountRepositoryMock = new Mock<IDiscountRepository>();
        var discountRequest = new BuyXOnePriceDiscount();
        discountRepositoryMock.Setup(d => d.GetDiscountsAsync()).ReturnsAsync(new List<IRequest<DiscountRequest>>
            { discountRequest });
        var productItemRepositoryMock = new Mock<IProductItemRepository>();

        productItemRepositoryMock.Setup(p => p.GetProductItemAsync(It.IsAny<string>())).ReturnsAsync(firstProductItem);
        var checkoutService = new CheckoutService(mediatorMock.Object, discountRepositoryMock.Object,
            productItemRepositoryMock.Object);
        await checkoutService.ScanAsync("A");
        checkoutService.GetTotalPrice();
        mediatorMock.Verify(m => m.Send(It.IsAny<BuyXOnePriceDiscount>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region ScanAsync

    /// <summary>
    ///     Confirm that the first time round a scan is called, it gets the discounts
    /// </summary>
    [Fact]
    public async Task CheckoutService_ScanAsync_FirstTimeRound_GetsDiscounts()
    {
        var sku = "A";
        var discountRepositoryMock = new Mock<IDiscountRepository>();
        var productItemRepositoryMock = new Mock<IProductItemRepository>();
        productItemRepositoryMock.Setup(p => p.GetProductItemAsync(It.IsAny<string>())).ReturnsAsync(new ProductItem());
        var checkoutService = new CheckoutService(It.IsAny<IMediator>(), discountRepositoryMock.Object,
            productItemRepositoryMock.Object);
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
        var productItemRepositoryMock = new Mock<IProductItemRepository>();
        productItemRepositoryMock.Setup(p => p.GetProductItemAsync(It.IsAny<string>())).ReturnsAsync(new ProductItem());
        var checkoutService = new CheckoutService(It.IsAny<IMediator>(), discountRepositoryMock.Object,
            productItemRepositoryMock.Object);
        await checkoutService.ScanAsync(sku1);
        await checkoutService.ScanAsync(sku2);
        discountRepositoryMock.Verify(d => d.GetDiscountsAsync(), Times.Once);
    }

    /// <summary>
    ///     Confirms that when an item is scanned it get the item from the repository
    /// </summary>
    [Fact]
    public async Task CheckoutService_ScanAsync_GetsProductItemFromRepository()
    {
        var productItem = new ProductItem
        {
            Sku = "A",
            UnitPrice = 50.00m
        };

        var productItemRepositoryMock = new Mock<IProductItemRepository>();
        productItemRepositoryMock.Setup(p => p.GetProductItemAsync(It.IsAny<string>())).ReturnsAsync(productItem);
        var checkoutService = new CheckoutService(Mock.Of<IMediator>(), Mock.Of<IDiscountRepository>(),
            productItemRepositoryMock.Object);
        await checkoutService.ScanAsync("A");
        productItemRepositoryMock.Verify(p => p.GetProductItemAsync("A"), Times.Once);
    }

    /// <summary>
    ///     Confirms that if an item is not returned from the repository when scanned then the correct exception is thrown
    /// </summary>
    [Fact]
    public async Task CheckoutService_ScanAsync_ProductNotFound_CorrectExceptionThrown()
    {
        var productItemRepositoryMock = new Mock<IProductItemRepository>();
        var checkoutService = new CheckoutService(Mock.Of<IMediator>(), Mock.Of<IDiscountRepository>(),
            productItemRepositoryMock.Object);
        productItemRepositoryMock.Setup(p => p.GetProductItemAsync(It.IsAny<string>()))
            .ReturnsAsync((ProductItem?)null);
        await Assert.ThrowsAsync<ProductNotFoundException>(async () => await checkoutService.ScanAsync("A"));
    }

    #endregion
}