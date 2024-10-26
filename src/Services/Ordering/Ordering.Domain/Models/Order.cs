

using Ordering.Domain.Events;

namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _orderItems = new();

        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public CustomerId CustomerId { get; private set; }
        public OrderName OrderName { get; private set; }
        public Address ShippingAddress { get; set; }
        public Address BillingAddress { get; set; }
        public Payment Payment { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrive
        {
            get => OrderItems.Sum(x => x.Price * x.Quantity);
            private set { }
        }

        public static Order Create(OrderId id,
            IReadOnlyList<OrderItem> orderItems,
            CustomerId customerId,
            OrderName orderName,
            Address shippingAddress,
            Payment payment)
        {
            var order = new Order
            {
                Id = id,
                CustomerId = customerId,
                BillingAddress = shippingAddress,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                Payment = payment,
                Status = OrderStatus.Pending
            };
            order.AddDomainEvent(new OrderCreatedEvent(order));
            return order;
        }

        public static Order Update(OrderId id,
           IReadOnlyList<OrderItem> orderItems,
           CustomerId customerId,
           OrderName orderName,
           Address shippingAddress,
           Payment payment,
           OrderStatus status)
        {
            var order = new Order
            {
                Id = id,
                CustomerId = customerId,
                BillingAddress = shippingAddress,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                Payment = payment,
                Status = status
            };
            order.AddDomainEvent(new OrderUpdatedEvent(this));
            return order;
        }
        public void Add(ProductId productId,int quantity,decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(quantity);
            ArgumentOutOfRangeException.ThrowIfNegative(price);

            var orderItem = new OrderItem(Id, productId, quantity, price);
            _orderItems.Add(orderItem);

        }
        public void Remove(ProductId productId)
        {
            var orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);
            if (orderItem is not null)
            {
                _orderItems.Remove(orderItem);
            }
        }
    }
}
