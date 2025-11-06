namespace CheckoutKata.Model.DomainServices;

/// <summary>
///     Manages checkout functionality
/// </summary>
public interface ICheckoutService
{
    /// <summary>
    ///     Scans a product item
    /// </summary>
    /// <param name="sku">Stock Keeping Unit</param>
    /// <returns>A task</returns>
    Task ScanAsync(string sku);

    /// <summary>
    ///     Gets the total price of the basket
    /// </summary>
    /// <returns></returns>
    decimal GetTotalPrice();
}