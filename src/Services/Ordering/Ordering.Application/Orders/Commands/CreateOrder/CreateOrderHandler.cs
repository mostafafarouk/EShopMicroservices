using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler(IApplicationDbContext applicationDbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command  , CancellationToken cancellationToken)
        {
            var order = CreateNewOrder(command.Order);
            applicationDbContext.Orders.Add(order);
            await applicationDbContext.SaveChangesAsync(cancellationToken);
            return new CreateOrderResult(order.Id.Value);
        }

        private Order CreateNewOrder(OrderDto orderDto)
        {
            var shippingAddress = Address.of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
            var billingAddress = Address.of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);

            var newOrder = Order.Create(
                    id: OrderId.of(Guid.NewGuid()),
                    customerId: CustomerId.of(orderDto.CustomerId),
                    orderName: OrderName.of(orderDto.OrderName),
                    shippingAddress: shippingAddress,
                    billingAddress: billingAddress,
                    payment: Payment.of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod)
                    );

            foreach (var orderItemDto in orderDto.OrderItems)
            {
                newOrder.Add(ProductId.of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);
            }
            return newOrder;
        }
    }
}
