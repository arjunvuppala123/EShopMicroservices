namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler(IApplicationDbContext dbContext)
        : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = CreateNewOrder(request.Order);
            
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateOrderResult(order.Id.Value);
        }

        private Order CreateNewOrder(OrderDto orderDto)
        {
            var shippingAddress = Address.Of(
                orderDto.ShippingAddress.FirstName,
                orderDto.ShippingAddress.LastName,
                orderDto.ShippingAddress.EmailAddress,
                orderDto.ShippingAddress.AddressLine,
                orderDto.ShippingAddress.Country,
                orderDto.ShippingAddress.State,
                orderDto.ShippingAddress.ZipCode);

            var bilingAddress = Address.Of(
                orderDto.BillingAddress.FirstName,
                orderDto.BillingAddress.LastName,
                orderDto.BillingAddress.EmailAddress,
                orderDto.BillingAddress.AddressLine,
                orderDto.BillingAddress.Country,
                orderDto.BillingAddress.State,
                orderDto.BillingAddress.ZipCode);

            var newOrder = Order.Create(
                    id: OrderId.Of(Guid.NewGuid()),
                    customerId: CustomerId.Of(orderDto.CustomerId),
                    orderName: OrderName.Of(orderDto.OrderName),
                    shippingAddress: shippingAddress,
                    billingAddress: bilingAddress,
                    payment: Payment.Of(orderDto.payment.CardName!, orderDto.payment.CardNumber, orderDto.payment.Expiration, orderDto.payment.CVV, orderDto.payment.PaymentMethod)
            );

            foreach (var item in orderDto.OrderItems)
            {
                newOrder.Add(
                    productId: ProductId.Of(item.ProductId),
                    quantity: item.Quantity,
                    price: item.Price);
            }

            return newOrder;
        }
    }
}
