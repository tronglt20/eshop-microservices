using EventBus.Messages.Events;
using MassTransit;
using Ordering.Domain.Entities;
using Ordering.Domain.Interfaces;
using Ordering.Infrastructure;
using Shared.Domain.Interfaces;

namespace Ordering.API.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutIntergrationEvent>
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IUnitOfWorkBase<OrderContext> _unitOfWork;

        public BasketCheckoutConsumer(IOrderRepository orderRepo
            , IUnitOfWorkBase<OrderContext> unitOfWork)
        {
            _orderRepo = orderRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutIntergrationEvent> context)
        {
            var @event = context.Message;
            var newOrder = new Order
            {
                UserName = @event.UserName,
                TotalPrice = @event.TotalPrice,
                FirstName = @event.FirstName,
                LastName = @event.LastName,
                EmailAddress = @event.EmailAddress,
                AddressLine = @event.AddressLine,
                Country = @event.Country,
                State = @event.State,
                ZipCode = @event.ZipCode,
                CardName = @event.CardName,
                CardNumber = @event.CardNumber,
                Expiration = @event.Expiration,
                CVV = @event.CVV,
                PaymentMethod = @event.PaymentMethod,
            };

            await _orderRepo.InsertAsync(newOrder);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
