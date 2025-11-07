namespace CheckoutKata.Model.Exceptions;

/// <summary>
///     Exception that occurs if product not found
/// </summary>
public class ProductNotFoundException : Exception
{
    /// <summary>
    ///     Stock keeping unit of product that is not found
    /// </summary>
    public string Sku { get; set; } = null!;
}