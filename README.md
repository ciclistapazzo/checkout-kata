# Checkout Kata

cli commands

```
dotnet new sln --name CheckoutKata
dotnet new classlib -n CheckoutKata.Model
dotnet sln add CheckoutKata.Model/CheckoutKata.Model.csproj
dotnet new xunit -n CheckoutKata.Model.Test
dotnet sln add CheckoutKata.Model.Test/CheckoutKata.Model.Test.csproj
```