using Discount.Grpc.Interfaces;
using Discount.Grpc.Protos;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepo;
        private readonly ILogger<DiscountService> _logger;
        public DiscountService(ILogger<DiscountService> logger, IDiscountRepository discountRepo)
        {
            _logger = logger;
            _discountRepo = discountRepo;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepo.GetDiscountAsync(request.ProductId);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductId={request.ProductId} is not found."));
            }

            _logger.LogInformation("Discount is retrieved for ProductId : {productName}, Amount : {amount}", coupon.Id, coupon.Amount);

            return new CouponModel
            {
                Id = coupon.Id,
                ProductId = coupon.ProductId,
                Description = coupon.Description,
                Amout = coupon.Amount
            };
        }

    }
}