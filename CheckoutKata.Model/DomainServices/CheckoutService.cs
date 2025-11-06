namespace CheckoutKata.Model.DomainServices;

/// <inheritdoc />
public class CheckoutService : ICheckoutService
{
    /// <inheritdoc />
    public Task ScanAsync(string sku)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public decimal GetTotalPrice()
    {
        throw new NotImplementedException();
    }
}