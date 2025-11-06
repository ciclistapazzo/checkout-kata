using CheckoutKata.Model.POCOs;

namespace CheckoutKata.Model.Repositories;

/// <summary>
///     Manages data interaction for product items
/// </summary>
public interface IProductItemRepository
{
    /// <summary>
    ///     Gets a product item for given SKU
    /// </summary>
    /// <param name="sku">SKU for the product item</param>
    /// <returns>The product item or null if not found</returns>
    Task<ProductItem?> GetProductItemAsync(string sku);
}