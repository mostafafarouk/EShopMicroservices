

namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string? CardName { get; } = default!;
        public string CardNumber { get; } = default!;
        public string Expiration { get; } = default!;
        public string CVV { get; } = default!;
        public string PaymentMethod { get; } = default!;
        public string PaymentMethod1 { get; }

        protected Payment()
        {

        }

        public Payment(string cardName, string cardNumber, string expiration, string cvv, string paymentMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV = cvv;
            PaymentMethod1 = paymentMethod;
        }

        public static Payment of(string cardName,string cardNumber,string expiration,string cvv,string paymentMethod)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
            ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length,3);
            ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
            return new Payment(cardName,cardNumber,expiration,cvv,paymentMethod);

        }
    }
}
