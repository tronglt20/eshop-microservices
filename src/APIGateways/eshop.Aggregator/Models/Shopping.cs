
namespace eshop.Aggregator.Models
{
    public class Shopping
    {
        public string UserName { get; set; }
        public Basket Basket { get; set; }
        public IEnumerable<OrderResponse> Orders { get; set; }
    }
}
