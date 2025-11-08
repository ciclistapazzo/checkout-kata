using CheckoutKata.Model.Discounts;
using MediatR;

namespace CheckoutKata.Model.Handlers;

public class BuyXOnePriceDiscountHandler : IRequestHandler<BuyXOnePriceDiscount, DiscountResponse>
{
    
    public Task<DiscountResponse> Handle(BuyXOnePriceDiscount request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}