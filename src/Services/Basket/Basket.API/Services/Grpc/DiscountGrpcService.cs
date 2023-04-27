using Discount.Grpc.Protos;

namespace Basket.API.Services.Grpc
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _grpcClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        public async Task<CouponModel> GetDiscountAsync(string productId)
        {
            var discountRequest = new GetDiscountRequest
            {
                ProductId = productId
            };

            return await _grpcClient.GetDiscountAsync(discountRequest);
        }
    }
}
