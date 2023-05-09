using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Ordering.Domain.Interfaces;
using Ordering.Infrastructure;
using Shared.Domain.Interfaces;

namespace Ordering.API.Services
{
    public class OrderingService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IUnitOfWorkBase<OrderContext> _unitOfWork;

        public OrderingService(IOrderRepository orderRepo, IUnitOfWorkBase<OrderContext> unitOfWork)
        {
            _orderRepo = orderRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult<IEnumerable<Order>>> GetOrderAsync(string userName)
        {
            return await _orderRepo.GetQuery(_ => _.UserName == userName)
                .ToListAsync();
        }
    }
}
