using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Extentions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            new List<Customer>
            { Customer.Create(CustomerId.of(new Guid("bfc03977-1e87-4917-a7c3-1a8b34e77d7e")),"Mostafa","mostafa.farouk@outlook.com")};

        public static IEnumerable<Product> Products =>
            new List<Product>
            { Product.Create(ProductId.of(new Guid("26415abc-1533-4ca7-8ada-7d6859d789bb")),"Product 1",500)};

        public static IEnumerable<Order> Orders =>
            new List<Order>();
            
    }
}