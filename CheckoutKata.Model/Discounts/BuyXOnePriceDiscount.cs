using MediatR;

namespace CheckoutKata.Model.Discounts;

public class BuyXOnePriceDiscount : Discount
{
    public int NumberOfItems { get; set; }
    
    public decimal CombinedPrice { get; set; }
}