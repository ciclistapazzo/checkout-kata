# Checkout Kata

## Purpose 

This is a demonstration of unit testing of a DotNet class library.  

## Specification / requirements

In a normal supermarket, things are identified using Stock Keeping Units, or SKUs. In our shop, we’ll use individual letters of the alphabet (A, B, C, and so on). Our goods are priced individually. In addition, some items are multi-priced: buy n of them, and they’ll cost you y pounds. For example, item ‘A’ might cost 50 pounds individually, but this week we have a special offer: buy three ‘A’s and they’ll cost you 130. The current pricing and offers are as follows:

|SKU| Unit Price |Special Price|
|-:|------------:|:------------|
| A|          50 |    3 for 130|
| B|          30 |     2 for 45|
| C|          20 |             |
| D|          15 |             |

Our checkout accepts items in any order, so that if we scan a B, an A, and another B, we’ll recognize the two B’s and price them at 45 (for a total price so far of 95). Because the pricing changes frequently, we need to be able to pass in a set of pricing rules each time we start handling a checkout transaction.

## My Solution

I have loosely followed a domain driven design.  I have a model class library.  It contains a checkout service.  The checkout service 
has a method which scans an SKU.  The checkout service gets the product item from a product item repository.  On the first scan it also gets
a list of discount objects from a discount repository.
The checkout service has another method to get the total price.  This enumerates through the discounts and sends each discount along with the basket to a MediatR mediator.
Each MediatR handler would apply a transformation to the given basket so as to perform the discount.  It would return the ajusted basket.
Each discount would apply a transformation to the data returned from the previous discount.  The order of the discounts would therefore be important.

## Unit tests

The purpose of this project is to demonstrate unit testing.  I have done unit tests for the checkout service and the handler that process the "Buy X, one price" discount.