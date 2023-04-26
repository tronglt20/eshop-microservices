using Discount.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.Infrastructure
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
        }

        public virtual DbSet<Coupon> Coupons { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }   
}
