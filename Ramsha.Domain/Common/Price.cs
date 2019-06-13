
using Ramsha.Domain.Products.Enums;

namespace Ramsha.Domain.Common;

public record Price
{
    private Price()
    {

    }
    public decimal Amount { get; }
    public CurrencyCode Currency { get; }


    public Price(decimal amount, CurrencyCode currency = CurrencyCode.USD)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative.", nameof(amount));

        Amount = amount;
        Currency = currency;
    }


    public Price ConvertTo(CurrencyCode targetCurrency, decimal exchangeRate)
    {
        if (exchangeRate <= 0)
            throw new ArgumentException("Exchange rate must be greater than zero.", nameof(exchangeRate));

        decimal convertedAmount = Amount * exchangeRate;
        return new Price(convertedAmount, targetCurrency);
    }


}
