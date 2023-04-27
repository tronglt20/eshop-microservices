using Discount.Grpc.Entities;
using Shared.Domain.Interfaces;

namespace Discount.Grpc.Interfaces
{
    public interface IDiscountRepository : IBaseRepository<Coupon>
    {
        Task<Coupon> GetDiscountAsync(string productId);
        Task CreateDiscountAsync(Coupon coupon);
        Task<bool> DeleteDiscountAsync(string productId);
    }
}
