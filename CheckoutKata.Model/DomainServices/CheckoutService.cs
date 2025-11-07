using CheckoutKata.Model.Discounts;
using CheckoutKata.Model.Exceptions;
using CheckoutKata.Model.POCOs;
using CheckoutKata.Model.Repositories;
using MediatR;

namespace CheckoutKata.Model.DomainServices;

/// <inheritdoc />
public class CheckoutService : ICheckoutService
{
    private readonly List<ProductItem> _basket = new();
    private readonly IDiscountRepository _discountRepository;
    private readonly IMediator _mediator;
    private readonly IProductItemRepository _productItemRepository;
    private List<Discount>? _discounts;

    /// <summary>
    ///     Initalizes an instance of a <see cref="CheckoutService" />
    /// </summary>
    /// <param name="mediator">mediator to dispatch requests</param>
    /// <param name="discountRepository">Gets discount information</param>
    /// <param name="productItemRepository">Gets product items</param>
    public CheckoutService(IMediator mediator, IDiscountRepository discountRepository,
        IProductItemRepository productItemRepository)
    {
        _mediator = mediator;
        _discountRepository = discountRepository;
        _productItemRepository = productItemRepository;
    }

    /// <inheritdoc />
    public async Task ScanAsync(string sku)
    {
        ArgumentNullException.ThrowIfNull(sku);
        if (_discounts is null || _basket.Count == 0) _discounts = await _discountRepository.GetDiscountsAsync();
        var productItem = await _productItemRepository.GetProductItemAsync(sku);
        if (productItem is null) throw new ProductNotFoundException { Sku = sku };
        _basket.Add(productItem);
    }

    /// <inheritdoc />
    public async Task<decimal> GetTotalPrice()
    {
        var currentBasket = _basket;
        if (_discounts != null)
            foreach (var discount in _discounts)
            {
                discount.ProductItems = currentBasket;
                var response = await _mediator.Send(discount);
                currentBasket = response.ProductItems;
            }

        return currentBasket.Sum(i => i.UnitPrice);
    }

    /// <inheritdoc />
    public void Clear()
    {
        throw new NotImplementedException();
    }
}