using Discount.Grpc.Data;
using Discount.Grpc.Entities;
using Discount.Grpc.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : BaseRepository<Coupon>, IDiscountRepository
    {
        public DiscountRepository(DiscountContext dbContext) : base(dbContext)
        {
        }

        public async Task CreateDiscountAsync(Coupon coupon)
        {
            await InsertAsync(coupon);
        }

        public async Task<bool> DeleteDiscountAsync(string productId)
        {
            var coupon = await GetDiscountAsync(productId);
            if (coupon == null)
                return false;

            await DeleteAsync(coupon);
            return true;
        }

        public async Task<Coupon> GetDiscountAsync(string productId)
        {
            return await GetQuery(_ => _.ProductId == productId).FirstOrDefaultAsync();
        }
    }
}
