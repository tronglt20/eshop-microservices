using Discount.Domain.Entities;
using Discount.Domain.Interfaces;
using Discount.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Interfaces;

namespace Discount.API.Services
{
    public class DiscountService
    {
        private readonly IUnitOfWorkBase<DiscountContext> _unitOfWork;
        private readonly IDiscountRepository _discountRepo;

        public DiscountService(IDiscountRepository discountRepo, IUnitOfWorkBase<DiscountContext> unitOfWork)
        {
            _discountRepo = discountRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Coupon> GetDiscountAsync(string productId)
        {
            return await _discountRepo.GetDiscountAsync(productId);
        }
    }
}
  