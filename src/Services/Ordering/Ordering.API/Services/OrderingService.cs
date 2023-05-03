using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ordering.API.ViewModels;
using Ordering.Domain.Interfaces;

namespace Ordering.API.Services
{
    public class OrderingService
    {
        private readonly IOrderRepository _orderRepo;

        public OrderingService(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task<ActionResult<OrderResponse>> GetOrderAsync(string userName)
        {
            return await _orderRepo.GetQuery(_ => _.UserName == userName)
                .Select(_ => new OrderResponse
                {
                    UserName = _.UserName,
                })
                .FirstOrDefaultAsync();
        }
    }
}
