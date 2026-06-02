
using Ramsha.Common.Domain;
using Ramsha.Domain.Products.Enums;

namespace Ramsha.Domain.Common;

public class Currency : Entity<int>
{
    private Currency() { }
    private Currency(CurrencyCode currency, decimal exchangeRate)
    {
        CurrencyCode = currency;
        ExchangeRate = exchangeRate;
    }

    public static Currency Create(CurrencyCode currency, decimal exchangeRate)
    {
        return new(currency, exchangeRate);
    }

    public void UpdateRate(decimal exchangeRate)
    {
        ExchangeRate = exchangeRate;
        LastUpdate = DateTime.UtcNow;
    }


    public CurrencyCode CurrencyCode { get; private set; }
    public decimal ExchangeRate { get; private set; }
    public DateTime LastUpdate { get; private set; }
}
